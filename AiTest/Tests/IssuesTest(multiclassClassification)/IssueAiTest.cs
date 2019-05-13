//using System;
//using System.Diagnostics;
//using System.Linq;
//using Microsoft.Data.DataView;
//using Microsoft.ML;
//using Microsoft.ML.Core.Data;
//using Microsoft.ML.Data;
//
//namespace AiTest.Tests
//{
//    public class IssueAiTest:IAiTest
//    {
//        private ITransformer _model;
//        private readonly MLContext _context = new MLContext(seed: 0);
//        private TextLoader _textLoader;
//        private const string RootFolder = "./Tests/IssuesTest(multiclassClassification)";
//        private const string ModelFileName = "model.zip";
//        private const string TrainDataFile = "issues_train.tsv";
//        private const string EvaluateDataFile = "issues-test.tsv";
//        void IAiTest.Train()
//        {
//            Console.WriteLine("=============== Multiclass Classification - Issue Area Prediction ===============");
//            _textLoader = _context.Data.CreateTextLoader<GitHubIssue>(hasHeader: true);
//            IDataView dataView =_textLoader.Read($"{RootFolder}/{TrainDataFile}");
//            var pipeline =
//                _context.Transforms.Conversion.MapValueToKey(inputColumnName: "Area", outputColumnName: "Label")
//                    .Append(_context.Transforms.Text.FeaturizeText(inputColumnName: "Title",
//                        outputColumnName: "TitleFeaturized"))
//                    .Append(_context.Transforms.Text.FeaturizeText(inputColumnName: "Description",
//                        outputColumnName: "DescriptionFeaturized"))
//                    .Append(_context.Transforms.Concatenate("Features", "TitleFeaturized", "DescriptionFeaturized"))
//                    .AppendCacheCheckpoint(_context)
//                    .Append(_context.MulticlassClassification.Trainers.StochasticDualCoordinateAscent(DefaultColumnNames.Label, DefaultColumnNames.Features))
//                    .Append(_context.Transforms.Conversion.MapKeyToValue("PredictedLabel"));
//            Stopwatch stop = new Stopwatch();
//            Console.WriteLine("=============== Create and Train the Model ===============");
//            stop.Start();
//            _model = pipeline.Fit(dataView);
//            stop.Stop();
//            Console.WriteLine($" Total {stop.ElapsedMilliseconds} ms");
//            Console.WriteLine("=============== End of training ===============");
//            Console.WriteLine();
//        }
//
//        void IAiTest.Evaluate()
//        {
//            var dataView = _textLoader.Read($"{RootFolder}/{EvaluateDataFile}");
//            Console.WriteLine("=============== Evaluating Model accuracy with Test data===============");
//            var predictions = _model.Transform(dataView);
//            var metrics = _context.MulticlassClassification.Evaluate(predictions, "Label");
//            Console.WriteLine();
//            Console.WriteLine($"*************************************************************************************************************");
//            Console.WriteLine($"*       Metrics for Multi-class Classification model - Test Data     ");
//            Console.WriteLine($"*------------------------------------------------------------------------------------------------------------");
//            Console.WriteLine($"*       MicroAccuracy:    {metrics.AccuracyMicro:0.###}");
//            Console.WriteLine($"*       MacroAccuracy:    {metrics.AccuracyMacro:0.###}");
//            Console.WriteLine($"*       LogLoss:          {metrics.LogLoss:#.###}");
//            Console.WriteLine($"*       LogLossReduction: {metrics.LogLossReduction:#.###}");
//            Console.WriteLine($"*************************************************************************************************************");
//            Utility.SaveModelAsFile(_context, _model, $"{RootFolder}/{ModelFileName}");
//        }
//
//        void IAiTest.Predict()
//        {
//            var predictionFunction = _model.CreatePredictionEngine<GitHubIssue, IssuePrediction>(_context);
//            GitHubIssue issue = new GitHubIssue() {
//                Title = "WebSockets communication is slow in my machine",
//                Description = "The WebSockets communication used under the covers by SignalR looks like is going slow in my development machine.."
//            };
//            var resultPrediction = predictionFunction.Predict(issue);
//            Console.WriteLine();
//            Console.WriteLine("=============== Prediction Test of model with a single sample and test dataset ===============");
//            Console.WriteLine();
//            Console.WriteLine($"Issue: {issue.Title} | Prediction: {resultPrediction.Area} | Probability: {resultPrediction.Score.Max()} ");
//            Console.WriteLine("=============== End of Predictions ===============");
//            Console.WriteLine();
//        }
//    }
//}