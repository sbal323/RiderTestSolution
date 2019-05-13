using System;
using System.Diagnostics;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;

namespace AiTest.Tests
{
    public class PriceAiTest:IAiTest
    {
        private ITransformer _model;
        private readonly MLContext _context = new MLContext(seed: 0);
        private const string RootFolder = "./Tests/PriceTest(regression)";
        private const string ModelFileName = "model.zip";
        private const string TrainDataFile = "taxi-fare-train.csv";
        private const string EvaluateDataFile = "taxi-fare-test.csv";
        private SdcaRegressionTrainer _trainer;
        void IAiTest.Train()
        {
            Console.WriteLine("=============== Regression task - Price Prediction ===============");
            IDataView dataView = _context.Data.LoadFromTextFile<TaxiTrip>($"{RootFolder}/{TrainDataFile}", hasHeader: true, separatorChar: ',');
            var dataProcessPipeline  = _context.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: nameof(TaxiTrip.FareAmount))
                .Append(_context.Transforms.Categorical.OneHotEncoding(outputColumnName: "VendorIdEncoded", inputColumnName: nameof(TaxiTrip.VendorId)))
                .Append(_context.Transforms.Categorical.OneHotEncoding(outputColumnName: "RateCodeEncoded", inputColumnName: nameof(TaxiTrip.RateCode)))
                .Append(_context.Transforms.Categorical.OneHotEncoding(outputColumnName: "PaymentTypeEncoded",inputColumnName: nameof(TaxiTrip.PaymentType)))
                .Append(_context.Transforms.NormalizeMeanVariance(outputColumnName: nameof(TaxiTrip.PassengerCount)))
                .Append(_context.Transforms.NormalizeMeanVariance(outputColumnName: nameof(TaxiTrip.TripTime)))
                .Append(_context.Transforms.NormalizeMeanVariance(outputColumnName: nameof(TaxiTrip.TripDistance)))
                .Append(_context.Transforms.Concatenate("Features", "VendorIdEncoded", "RateCodeEncoded", "PaymentTypeEncoded", nameof(TaxiTrip.PassengerCount)
                    , nameof(TaxiTrip.TripTime), nameof(TaxiTrip.TripDistance)));
            _trainer = _context.Regression.Trainers.Sdca(labelColumnName: "Label", featureColumnName: "Features");
            var trainingPipeline = dataProcessPipeline.Append(_trainer);
            Stopwatch stop = new Stopwatch();
            Console.WriteLine("=============== Create and Train the Model ===============");
            stop.Start();
            _model = trainingPipeline.Fit(dataView);
            stop.Stop();
            Console.WriteLine($" Total {stop.ElapsedMilliseconds} ms");
            Console.WriteLine("=============== End of training ===============");
            Console.WriteLine();
        }
        void IAiTest.Evaluate()
        {
            IDataView dataView = _context.Data.LoadFromTextFile<TaxiTrip>($"{RootFolder}/{EvaluateDataFile}", hasHeader: true, separatorChar: ',');
            Console.WriteLine("=============== Evaluating Model accuracy with Test data===============");
            var predictions = _model.Transform(dataView);
            var metrics = _context.Regression.Evaluate(predictions, "Label", "Score");
            Common.ConsoleHelper.PrintRegressionMetrics(_trainer.ToString(), metrics);
            Utility.SaveModelAsFile(_context,_model,  dataView, $"{RootFolder}/{ModelFileName}");
        }
        void IAiTest.Predict()
        {
            ITransformer loadedModel;
            using (var stream = new FileStream($"{RootFolder}/{ModelFileName}", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                loadedModel = _context.Model.Load(stream, out var modelInputSchema);
            }
            var predEngine = _context.Model.CreatePredictionEngine<TaxiTrip, TaxiTripFarePrediction>(loadedModel);
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
            var prediction = predEngine.Predict(taxiTripSample);
            Console.WriteLine();
            Console.WriteLine($"**********************************************************************");
            Console.WriteLine($"Predicted fare: {prediction.FareAmount:0.####}, actual fare: 15.5");
            Console.WriteLine($"**********************************************************************");
            Console.WriteLine();
        }
    }
}