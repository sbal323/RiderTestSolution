using Microsoft.ML.Data;

namespace AiTest.Tests.IrisTest
{
    public class IrisPrediction
    {
        [ColumnName("PredictedLabel")]
        public uint PredictedClusterId;
       
        [ColumnName("Score")]
        public float[] Distances;
    }
}