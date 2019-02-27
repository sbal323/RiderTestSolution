using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Data.DataView;
using Microsoft.ML;
using Microsoft.ML.Core.Data;
using Microsoft.ML.Data;

namespace AiTest.Tests
{
    public class PriceAiTest:IAiTest
    {
        private ITransformer _model;
        private readonly MLContext _context = new MLContext(seed: 0);
        private TextLoader _textLoader;
        private const string RootFolder = "./Tests/PriceTest(regression)";
        private const string ModelFileName = "model.zip";
        private const string TrainDataFile = "taxi-fare-train.csv";
        private const string EvaluateDataFile = "taxi-fare-test.csv";
        void IAiTest.Train()
        {
            Console.WriteLine("=============== Regression task - Price Prediction ===============");
            _textLoader = _context.Data.CreateTextLoader(new TextLoader.Arguments()
                {
                    Separators = new[] { ',' },
                    HasHeader = true,
                    Column = new[]
                    {
                        new TextLoader.Column("VendorId", DataKind.Text, 0),
                        new TextLoader.Column("RateCode", DataKind.Text, 1),
                        new TextLoader.Column("PassengerCount", DataKind.R4, 2),
                        new TextLoader.Column("TripTime", DataKind.R4, 3),
                        new TextLoader.Column("TripDistance", DataKind.R4, 4),
                        new TextLoader.Column("PaymentType", DataKind.Text, 5),
                        new TextLoader.Column("FareAmount", DataKind.R4, 6)
                    }
                }
            );
            IDataView dataView = _textLoader.Read($"{RootFolder}/{TrainDataFile}");
            var pipeline = _context.Transforms.CopyColumns(inputColumnName:"FareAmount", outputColumnName:"Label")
                .Append(_context.Transforms.Categorical.OneHotEncoding("VendorId"))
                .Append(_context.Transforms.Categorical.OneHotEncoding("RateCode"))
                .Append(_context.Transforms.Categorical.OneHotEncoding("PaymentType"))
                .Append(_context.Transforms.Concatenate("Features", "VendorId", "RateCode", "PassengerCount", "TripTime", "TripDistance", "PaymentType"))
                .Append(_context.Regression.Trainers.FastTree());
            Stopwatch stop = new Stopwatch();
            Console.WriteLine("=============== Create and Train the Model ===============");
            stop.Start();
            _model = pipeline.Fit(dataView);
            stop.Stop();
            Console.WriteLine($" Total {stop.ElapsedMilliseconds} ms");
            Console.WriteLine("=============== End of training ===============");
            Console.WriteLine();
        }
        void IAiTest.Evaluate()
        {
            var dataView = _textLoader.Read($"{RootFolder}/{EvaluateDataFile}");
            Console.WriteLine("=============== Evaluating Model accuracy with Test data===============");
            var predictions = _model.Transform(dataView);
            var metrics = _context.Regression.Evaluate(predictions, "Label", "Score");
            Console.WriteLine();
            Console.WriteLine($"*************************************************");
            Console.WriteLine($"*       Model quality metrics evaluation         ");
            Console.WriteLine($"*------------------------------------------------");
            Console.WriteLine($"*       R2 Score:      {metrics.RSquared:0.##}");
            Console.WriteLine($"*       RMS loss:      {metrics.Rms:#.##}");
            Console.WriteLine($"*************************************************");
            Utility.SaveModelAsFile(_context, _model, $"{RootFolder}/{ModelFileName}");
        }
        void IAiTest.Predict()
        {
            ITransformer loadedModel;
            using (var stream = File.OpenRead($"{RootFolder}/{ModelFileName}"))
                loadedModel = _context.Model.Load(stream);
            var predictionFunction = loadedModel.CreatePredictionEngine<TaxiTrip, TaxiTripFarePrediction>(_context);
            var taxiTripSample = new TaxiTrip()
            {
                VendorId = "VTS",
                RateCode = "1",
                PassengerCount = 1,
                TripTime = 1140,
                TripDistance = 3.75f,
                PaymentType = "CRD",
                FareAmount = 0 // To predict. Actual/Observed = 15.5
            };
            var prediction = predictionFunction.Predict(taxiTripSample);
            Console.WriteLine();
            Console.WriteLine($"**********************************************************************");
            Console.WriteLine($"Predicted fare: {prediction.FareAmount:0.####}, actual fare: 15.5");
            Console.WriteLine($"**********************************************************************");
            Console.WriteLine();
        }
    }
}