using System;
using System.IO;
using AiTest.Tests.IrisTest;
using Microsoft.ML;
using Microsoft.ML.Core.Data;
using Microsoft.ML.Data;
using Microsoft.ML.Model;
using Microsoft.ML.Transforms.Conversions;

namespace AiTest.Tests.SalaryTest
{
    public class SalaryAiTest:IAiTest
    {
        private ITransformer _model;
        private MLContext _context;
        private const string ModelPath = ".\\Tests\\SalaryTest(regression)\\model.zip";
        private const float PredictionYears = 8;
        void IAiTest.Train()
        {
            var dataset = ".\\Tests\\SalaryTest(regression)\\salary-data.txt";
            _context = new MLContext();
            var reader = _context.Data.CreateTextLoader<SalaryData>(separatorChar: ',', hasHeader: true);
            var trainingDataView = reader.Read(dataset);
            //When the model is trained and evaluated, by default, the values in the Label column are considered as correct values to be predicted. 
            var pipeline = _context.Transforms.CopyColumns(inputColumnName:"Salary", outputColumnName:"Label")
                .Append(_context.Transforms.Concatenate("Features", "YearsExperience"))
                .Append(_context.Regression.Trainers.FastTree());
            Console.WriteLine("--------------Training----------------");
            _model = pipeline.Fit(trainingDataView);
            using (var stream = File.Create(ModelPath))
            {
                _context.Model.Save(_model, stream);
            }
        }
        void IAiTest.Evaluate()
        {
            // When you load the model, it's a transformer.
            ITransformer loadedModel;
            using (var stream = File.OpenRead(ModelPath))
                loadedModel = _context.Model.Load(stream);
            Console.WriteLine("--------------Evaluating----------------");
            var reader = _context.Data.CreateTextLoader<SalaryData>(separatorChar: ',', hasHeader: true);
            var testDataset = ".\\Tests\\SalaryTest(regression)\\salary-data-test.txt";
            var trainingDataView = reader.Read(testDataset);
            var predictions = loadedModel.Transform(trainingDataView);
            var metrics = _context.Regression.Evaluate(predictions, "Label", "Score");
            Console.WriteLine();
            Console.WriteLine($"*************************************************");
            Console.WriteLine($"*       Model quality metrics evaluation         ");
            Console.WriteLine($"*------------------------------------------------");
            Console.WriteLine($"Root Mean Squared: {metrics.Rms}");
            Console.WriteLine($"R^2: {metrics.RSquared}");
            Console.WriteLine(Environment.NewLine);
        }
        void IAiTest.Predict()
        {
            // When you load the model, it's a transformer.
            ITransformer loadedModel;
            using (var stream = File.OpenRead(ModelPath))
                loadedModel = _context.Model.Load(stream);
            Console.WriteLine("--------------Predicting----------------");
            var predictionFunction = loadedModel.CreatePredictionEngine<SalaryData, SalaryPrediction>(_context);
            var prediction = predictionFunction.Predict(new SalaryData() {YearsExperience = PredictionYears});
            Console.WriteLine($"**********************************************************************");
            Console.WriteLine($"After {PredictionYears} years you would earn around {String.Format("{0:C}", prediction.PredictedSalary)}");
            Console.WriteLine($"**********************************************************************");
        }
    }
}