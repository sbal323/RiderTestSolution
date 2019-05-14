using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using AiTest.Tests.IrisTest;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Model;
using Microsoft.ML.Trainers;

namespace AiTest.Tests.SalaryTest
{
    public class SalaryAiTest:IAiTest
    {
        private ITransformer _model;
        private readonly MLContext _context = new MLContext(seed: 0);
        private const string RootFolder = "./Tests/SalaryTest(regression)";
        private const string ModelFileName = "model.zip";
        private const string TrainDataFile = "salary-data.txt";
        private const string EvaluateDataFile = "salary-data-test.txt";
        private SdcaRegressionTrainer _trainer;
        void IAiTest.Train()
        {
            Console.WriteLine("=============== Regression task - Salary Prediction ===============");
            IDataView dataView = _context.Data.LoadFromTextFile<SalaryData>($"{RootFolder}/{TrainDataFile}", hasHeader: true, separatorChar: ',');
            var dataProcessPipeline  = _context.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: nameof(SalaryData.Salary))
                .Append(_context.Transforms.Concatenate("Features", nameof(SalaryData.YearsExperience)));
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
            IDataView dataView = _context.Data.LoadFromTextFile<SalaryData>($"{RootFolder}/{EvaluateDataFile}", hasHeader: true, separatorChar: ',');
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
            var predEngine = _context.Model.CreatePredictionEngine<SalaryData, SalaryPrediction>(loadedModel);
            var salarySample = new SalaryData() {YearsExperience = 8};
            var prediction = predEngine.Predict(salarySample);
            Console.WriteLine();
            Console.WriteLine($"**********************************************************************");
            Console.WriteLine($"Predicted salary: {prediction.PredictedSalary:0.####}");
            Console.WriteLine($"**********************************************************************");
            Console.WriteLine();
        }
    }
}