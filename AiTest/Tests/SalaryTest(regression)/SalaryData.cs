using Microsoft.ML.Data;

namespace AiTest.Tests.SalaryTest
{
    public class SalaryData
    {
        [LoadColumn(0)]
        public float YearsExperience;

        [LoadColumn(1)]
        public float Salary;
    }
}