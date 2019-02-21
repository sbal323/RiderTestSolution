using Microsoft.ML.Data;

namespace AiTest.Tests
{
    public class IssuePrediction
    {
        [ColumnName("PredictedLabel")]
        public string Area;
        [ColumnName("Score")]
        public float[] Score { get; set; }
    }
}