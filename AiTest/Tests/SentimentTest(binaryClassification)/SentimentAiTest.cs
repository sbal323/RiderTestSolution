using System;
using System.Diagnostics;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;

namespace AiTest.Tests
{
    public class SentimentAiTest:IAiTest
    {
        private ITransformer _model;
        private readonly MLContext _context = new MLContext(seed: 0);
        private const string RootFolder = "./Tests/SentimentTest(binaryClassification)";
        private const string ModelFileName = "model.zip";
        private const string TrainDataFile = "wikipedia-detox-250-line-data.tsv";
        private const string EvaluateDataFile = "wikipedia-detox-250-line-test.tsv";
        private SdcaLogisticRegressionBinaryTrainer _trainer;
        void IAiTest.Train()
        {
            Console.WriteLine("=============== Binary Classification - TextSentiment Prediction ===============");
            IDataView dataView = _context.Data.LoadFromTextFile<SentimentData>($"{RootFolder}/{TrainDataFile}", hasHeader: true);
            var dataProcessPipeline  = _context.Transforms.Text.FeaturizeText(outputColumnName: "Features", inputColumnName:nameof(SentimentData.Text));

            _trainer = _context.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Label", featureColumnName: "Features");
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
            IDataView dataView = _context.Data.LoadFromTextFile<SentimentData>($"{RootFolder}/{EvaluateDataFile}", hasHeader: true);
            Console.WriteLine("=============== Evaluating Model accuracy with Test data===============");
            var predictions = _model.Transform(dataView);
            var metrics = _context.BinaryClassification.Evaluate(predictions, "Label", "Score");
            Common.ConsoleHelper.PrintBinaryClassificationMetrics(_trainer.ToString(), metrics);
            Utility.SaveModelAsFile(_context,_model,  dataView, $"{RootFolder}/{ModelFileName}");
        }
        void IAiTest.Predict()
        {
            // Create prediction engine related to the loaded trained model
            var predEngine= _context.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(_model);
            SentimentData sampleStatement = new SentimentData
            {
                Text = "Why are you threatening me? I'm not being disruptive, its you who is being disruptive."
            };
            //Score
            var resultPrediction = predEngine.Predict(sampleStatement);
            Console.WriteLine();
            Console.WriteLine("=============== Prediction Test of model with a single sample and test dataset ===============");
            Console.WriteLine();
            Console.WriteLine($"Sentiment: {sampleStatement.Text} | Prediction: {(Convert.ToBoolean(resultPrediction.Prediction) ? "Toxic" : "Not Toxic")} | Probability: {resultPrediction.Probability} ");
            Console.WriteLine("=============== End of Predictions ===============");
            Console.WriteLine();
        }
    }
}