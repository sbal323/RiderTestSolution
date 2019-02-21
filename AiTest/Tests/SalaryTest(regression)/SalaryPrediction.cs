using Microsoft.ML.Data;

namespace AiTest.Tests.SalaryTest
{
    public class SalaryPrediction
    {
        [ColumnName("Score")]
        public float PredictedSalary;
    }
}