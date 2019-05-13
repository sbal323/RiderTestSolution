//using System;
//using System.Diagnostics;
//using System.IO;
//using AiTest.Tests.IrisTest;
//using Microsoft.Data.DataView;
//using Microsoft.ML;
//using Microsoft.ML.Core.Data;
//using Microsoft.ML.Data;
//using Microsoft.ML.Model;
//using Microsoft.ML.Transforms.Conversions;
//
//namespace AiTest.Tests.SalaryTest
//{
//    public class SalaryAiTest:IAiTest
//    {
//        private ITransformer _model;
//        private readonly MLContext _context = new MLContext(seed: 0);
//        private TextLoader _textLoader;
//        private const string RootFolder = "./Tests/SalaryTest(regression)";
//        private const string ModelFileName = "model.zip";
//        private const string TrainDataFile = "salary-data.txt";
//        private const string EvaluateDataFile = "salary-data-test.txt";
//        void IAiTest.Train()
//        {
//            Console.WriteLine("=============== Regression task - Salary Prediction ===============");
//            _textLoader = _context.Data.CreateTextLoader<SalaryData>(separatorChar: ',', hasHeader: true);
//            IDataView dataView = _textLoader.Read($"{RootFolder}/{TrainDataFile}");
//            var pipeline = _context.Transforms.CopyColumns(inputColumnName:"Salary", outputColumnName:"Label")
//                .Append(_context.Transforms.Concatenate("Features", "YearsExperience"))
//                .Append(_context.Regression.Trainers.FastTree());
//            Stopwatch stop = new Stopwatch();
//            Console.WriteLine("=============== Create and Train the Model ===============");
//            stop.Start();
//            _model = pipeline.Fit(dataView);
//            stop.Stop();
//            Console.WriteLine($" Total {stop.ElapsedMilliseconds} ms");
//            Console.WriteLine("=============== End of training ===============");
//            Console.WriteLine();
//        }
//        void IAiTest.Evaluate()
//        {
//            var dataView = _textLoader.Read($"{RootFolder}/{EvaluateDataFile}");
//            Console.WriteLine("=============== Evaluating Model accuracy with Test data===============");
//            var predictions = _model.Transform(dataView);
//            var metrics = _context.Regression.Evaluate(predictions, "Label", "Score");
//            Console.WriteLine();
//            Console.WriteLine($"*************************************************");
//            Console.WriteLine($"*       Model quality metrics evaluation         ");
//            Console.WriteLine($"*------------------------------------------------");
//            Console.WriteLine($"*       R2 Score:      {metrics.RSquared:0.##}");
//            Console.WriteLine($"*       RMS loss:      {metrics.Rms:#.##}");
//            Console.WriteLine($"*************************************************");
//            Utility.SaveModelAsFile(_context, _model, $"{RootFolder}/{ModelFileName}");
//        }
//        void IAiTest.Predict()
//        {
//            ITransformer loadedModel;
//            using (var stream = File.OpenRead($"{RootFolder}/{ModelFileName}"))
//                loadedModel = _context.Model.Load(stream);
//            var predictionFunction = loadedModel.CreatePredictionEngine<SalaryData, SalaryPrediction>(_context);
//            var salarySample = new SalaryData() {YearsExperience = 8};
//            var prediction = predictionFunction.Predict(salarySample);
//            Console.WriteLine();
//            Console.WriteLine($"**********************************************************************");
//            Console.WriteLine($"Predicted salary: {prediction.PredictedSalary:0.####}");
//            Console.WriteLine($"**********************************************************************");
//            Console.WriteLine();
//        }
//    }
//}