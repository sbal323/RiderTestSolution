using System;
using System.Diagnostics;
using Microsoft.ML;
using Microsoft.ML.Data;


namespace AiTest.Tests.IrisTest
{
    public class IrisAiTest : IAiTest
    {
        private ITransformer _model;
        private readonly MLContext _context = new MLContext(seed: 0);
        private const string RootFolder = "./Tests/IrisTest(clustering)";
        private const string ModelFileName = "model.zip";
        private const string TrainDataFile = "iris-data.txt";

        void IAiTest.Train()
        {
            Console.WriteLine("=============== Clustering task - Iris Prediction ===============");
            // STEP 1: Common data loading configuration
            IDataView fullData = _context.Data.LoadFromTextFile(path: $"{RootFolder}/{TrainDataFile}",
                columns:new[]
                {
                    new TextLoader.Column("Label", DataKind.Single, 0),
                    new TextLoader.Column(nameof(IrisData.SepalLength), DataKind.Single, 1),
                    new TextLoader.Column(nameof(IrisData.SepalWidth), DataKind.Single, 2),
                    new TextLoader.Column(nameof(IrisData.PetalLength), DataKind.Single, 3),
                    new TextLoader.Column(nameof(IrisData.PetalWidth), DataKind.Single, 4),
                },
                hasHeader:true,
                separatorChar:'\t');
                                                
            //Split dataset in two parts: TrainingDataset (80%) and TestDataset (20%)
            DataOperationsCatalog.TrainTestData trainTestData = _context.Data.TrainTestSplit(fullData, testFraction: 0.2);
            var trainingDataView = trainTestData.TrainSet;
            var testingDataView = trainTestData.TestSet;
            //STEP 2: Process data transformations in pipeline
            var dataProcessPipeline = _context.Transforms.Concatenate("Features", nameof(IrisData.SepalLength), nameof(IrisData.SepalWidth), nameof(IrisData.PetalLength), nameof(IrisData.PetalWidth));

            // STEP 3: Create and train the model     
            var trainer = _context.Clustering.Trainers.KMeans(featureColumnName: "Features", numberOfClusters: 3);
            var trainingPipeline = dataProcessPipeline.Append(trainer);
            
            Stopwatch stop = new Stopwatch();
            Console.WriteLine("=============== Create and Train the Model ===============");
            stop.Start();
            _model = trainingPipeline.Fit(trainingDataView);
            stop.Stop();
            Console.WriteLine($" Total {stop.ElapsedMilliseconds} ms");
            Console.WriteLine("=============== End of training ===============");
            Console.WriteLine();
            Utility.SaveModelAsFile(_context, _model, trainingDataView, $"{RootFolder}/{ModelFileName}");
        }

        void IAiTest.Evaluate()
        {
            // No evaluation for clustering
        }

        void IAiTest.Predict()
        {
            var prediction = _context.Model.CreatePredictionEngine<IrisData, IrisPrediction>(_model).Predict(
                new IrisData()
                {
                    SepalLength = 5.1f,
                    SepalWidth = 3.5f,
                    PetalLength = 1.4f,
                    PetalWidth = 0.2f
                });
            Console.WriteLine();
            Console.WriteLine($"**********************************************************************");
            Console.WriteLine($"Cluster: {prediction.PredictedClusterId}");
            Console.WriteLine($"Distances: {string.Join(" ", prediction.Distances)}");
            Console.WriteLine($"**********************************************************************");
            Console.WriteLine();
        }
    }
}