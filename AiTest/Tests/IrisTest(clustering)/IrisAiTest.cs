using System;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Conversions;

namespace AiTest.Tests.IrisTest
{
    public class IrisAiTest:IAiTest
    {
        private TransformerChain<KeyToValueMappingTransformer> _model;
        private MLContext _context;
        void IAiTest.Train()
        {
            _context = new MLContext();
            var reader = _context.Data.CreateTextLoader<IrisData>(separatorChar: ',', hasHeader: true);
            var trainingDataView = reader.Read(".\\Tests\\IrisTest(clustering)\\iris-data.txt");
            var pipeline = _context.Transforms.Conversion.MapValueToKey("Label")
                .Append(_context.Transforms.Concatenate("Features", "SepalLength", "SepalWidth", "PetalLength", "PetalWidth"))
                .Append(_context.MulticlassClassification.Trainers.StochasticDualCoordinateAscent(labelColumn: "Label", featureColumn: "Features"))
                .Append(_context.Transforms.Conversion.MapKeyToValue("PredictedLabel"));
            _model = pipeline.Fit(trainingDataView);
        }

        void IAiTest.Evaluate()
        {
            
        }

        void IAiTest.Predict()
        {
            var prediction = _model.CreatePredictionEngine<IrisData, IrisPrediction>(_context).Predict(
                new IrisData()
                {
                    SepalLength = 3.3f,
                    SepalWidth = 1.6f,
                    PetalLength = 0.2f,
                    PetalWidth = 5.1f,
                });
            Console.WriteLine($"Predicted flower type is: {prediction.PredictedLabels}");
        }
    }
}