using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;

namespace AiTest.Tests
{
    public class IssueAiTest:IAiTest
    {
        private ITransformer _model;
        private readonly MLContext _context = new MLContext(seed: 0);
        private const string RootFolder = "./Tests/IssuesTest(multiclassClassification)";
        private const string ModelFileName = "model.zip";
        private const string TrainDataFile = "issues_train.tsv";
        private const string EvaluateDataFile = "issues-test.tsv";
        private EstimatorChain<KeyToValueMappingTransformer> _trainer;
        void IAiTest.Train()
        {
            Console.WriteLine("=============== Multiclass Classification - Issue Area Prediction ===============");
             IDataView dataView = _context.Data.LoadFromTextFile<GitHubIssue>($"{RootFolder}/{TrainDataFile}", hasHeader: true);

             var dataProcessPipeline =  _context.Transforms.Conversion.MapValueToKey(inputColumnName: nameof(GitHubIssue.Area), outputColumnName: "Area")
                                        .Append(_context.Transforms.Text.FeaturizeText(inputColumnName: "Title", outputColumnName: "TitleFeaturized"))
                                         .Append(_context.Transforms.Text.FeaturizeText(inputColumnName: "Description", outputColumnName: "DescriptionFeaturized"))
                                         .Append(_context.Transforms.Concatenate("Features", "TitleFeaturized", "DescriptionFeaturized"))
                                         .AppendCacheCheckpoint(_context); 

             _trainer = _context.MulticlassClassification.Trainers.SdcaMaximumEntropy(labelColumnName: "Area", featureColumnName: "Features")
                 .Append(_context.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

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
            IDataView dataView = _context.Data.LoadFromTextFile<GitHubIssue>($"{RootFolder}/{EvaluateDataFile}", hasHeader: true);

            Console.WriteLine("=============== Evaluating Model accuracy with Test data===============");
            var predictions = _model.Transform(dataView);
            var metrics = _context.MulticlassClassification.Evaluate(predictions, "Area", "Score");

            Common.ConsoleHelper.PrintMultiClassClassificationMetrics(_trainer.ToString(), metrics);
            Utility.SaveModelAsFile(_context, _model, dataView,$"{RootFolder}/{ModelFileName}");
        }

        void IAiTest.Predict()
        {
            var predictionFunction = _context.Model.CreatePredictionEngine<GitHubIssue, IssuePrediction>(_model);
            GitHubIssue issue = new GitHubIssue() {
                Title = "WebSockets communication is slow in my machine",
                Description = "The WebSockets communication used under the covers by SignalR looks like is going slow in my development machine.."
            };
            var resultPrediction = predictionFunction.Predict(issue);
            Console.WriteLine();
            Console.WriteLine("=============== Prediction Test of model with a single sample and test dataset ===============");
            Console.WriteLine();
            Console.WriteLine($"Issue: {issue.Title} | Prediction: {resultPrediction.Area} | Probability: {resultPrediction.Score.Max()} ");
            Console.WriteLine("=============== End of Predictions ===============");
            Console.WriteLine();
        }
    }
}