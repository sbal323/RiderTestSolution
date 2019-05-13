//using System;
//using System.Diagnostics;
//using Microsoft.Data.DataView;
//using Microsoft.ML;
//using Microsoft.ML.Core.Data;
//using Microsoft.ML.Data;
//using Microsoft.ML.Transforms.Conversions;
//
//namespace AiTest.Tests.IrisTest
//{
//    public class IrisAiTest : IAiTest
//    {
//        private ITransformer _model;
//        private readonly MLContext _context = new MLContext(seed: 0);
//        private TextLoader _textLoader;
//        private const string RootFolder = "./Tests/IrisTest(clustering)";
//        private const string ModelFileName = "model.zip";
//        private const string TrainDataFile = "iris-data.txt";
//
//        void IAiTest.Train()
//        {
//            Console.WriteLine("=============== Clustering task - Iris Prediction ===============");
//            _textLoader = _context.Data.CreateTextLoader<IrisData>(separatorChar: ',', hasHeader: false);
//            IDataView dataView = _textLoader.Read($"{RootFolder}/{TrainDataFile}");
//            var pipeline = _context.Transforms.Concatenate("Features", "SepalLength", "SepalWidth", "PetalLength",
//                    "PetalWidth")
//                .Append(_context.Clustering.Trainers.KMeans(featureColumn: "Features", clustersCount:3));
//            Stopwatch stop = new Stopwatch();
//            Console.WriteLine("=============== Create and Train the Model ===============");
//            stop.Start();
//            _model = pipeline.Fit(dataView);
//            stop.Stop();
//            Console.WriteLine($" Total {stop.ElapsedMilliseconds} ms");
//            Console.WriteLine("=============== End of training ===============");
//            Console.WriteLine();
//            Utility.SaveModelAsFile(_context, _model, $"{RootFolder}/{ModelFileName}");
//        }
//
//        void IAiTest.Evaluate()
//        {
//            // No evaluation for clustering
//        }
//
//        void IAiTest.Predict()
//        {
//            var prediction = _model.CreatePredictionEngine<IrisData, IrisPrediction>(_context).Predict(
//                new IrisData()
//                {
//                    SepalLength = 5.1f,
//                    SepalWidth = 3.5f,
//                    PetalLength = 1.4f,
//                    PetalWidth = 0.2f
//                });
//            Console.WriteLine();
//            Console.WriteLine($"**********************************************************************");
//            Console.WriteLine($"Cluster: {prediction.PredictedClusterId}");
//            Console.WriteLine($"Distances: {string.Join(" ", prediction.Distances)}");
//            Console.WriteLine($"**********************************************************************");
//            Console.WriteLine();
//        }
//    }
//}