using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Data.DataView;
using Microsoft.ML;
using Microsoft.ML.Core.Data;
using Microsoft.ML.Data;

namespace AiTest.Tests
{
    public class SentimentAiTest:IAiTest
    {
        private ITransformer _model;
        private readonly MLContext _context = new MLContext(seed: 0);
        private TextLoader _textLoader;
        private const string RootFolder = "./Tests/SentimentTest(binaryClassification)";
        private const string ModelFileName = "model.zip";
        private const string TrainDataFile = "wikipedia-detox-250-line-data.tsv";
        private const string EvaluateDataFile = "wikipedia-detox-250-line-test.tsv";
        void IAiTest.Train()
        {
            Console.WriteLine("=============== Binary Classification - TextSentiment Prediction ===============");
            _textLoader = _context.Data.CreateTextLoader(
                columns: new TextLoader.Column[] 
                {
                    new TextLoader.Column("Label", DataKind.Bool,0),
                    new TextLoader.Column("SentimentText", DataKind.Text,1)
                },
                separatorChar: '\t',
                hasHeader: true
            );
            IDataView dataView =_textLoader.Read($"{RootFolder}/{TrainDataFile}");
            var pipeline =
                _context.Transforms.Text.FeaturizeText(inputColumnName: "SentimentText", outputColumnName: "Features")
                    .Append(_context.BinaryClassification.Trainers.FastTree(numLeaves: 50, numTrees: 50, minDatapointsInLeaves: 20));
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
            var metrics = _context.BinaryClassification.Evaluate(predictions, "Label");
            Console.WriteLine();
            Console.WriteLine("Model quality metrics evaluation");
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Accuracy: {metrics.Accuracy:P2}");
            Console.WriteLine($"Auc: {metrics.Auc:P2}");
            Console.WriteLine($"F1Score: {metrics.F1Score:P2}");
            Console.WriteLine("=============== End of model evaluation ===============");
            Utility.SaveModelAsFile(_context, _model, $"{RootFolder}/{ModelFileName}");
        }
        void IAiTest.Predict()
        {
            var predictionFunction = _model.CreatePredictionEngine<SentimentData, SentimentPrediction>(_context);
            SentimentData sampleStatement = new SentimentData
            {
                SentimentText = "This is a very rude movie"
            };
            var resultPrediction = predictionFunction.Predict(sampleStatement);
            Console.WriteLine();
            Console.WriteLine("=============== Prediction Test of model with a single sample and test dataset ===============");
            Console.WriteLine();
            Console.WriteLine($"Sentiment: {sampleStatement.SentimentText} | Prediction: {(Convert.ToBoolean(resultPrediction.Prediction) ? "Toxic" : "Not Toxic")} | Probability: {resultPrediction.Probability} ");
            Console.WriteLine("=============== End of Predictions ===============");
            Console.WriteLine();
        }
    }
}