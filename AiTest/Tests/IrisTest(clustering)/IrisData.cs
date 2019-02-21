using Microsoft.ML.Data;

namespace AiTest.Tests.IrisTest
{
    //длину чашелистика, ширину чашелистика, длину лепестка, ширину лепестка и тип цветка ириса
    public class IrisData
    {
        [LoadColumn(0)]
        public float SepalLength;

        [LoadColumn(1)]
        public float SepalWidth;

        [LoadColumn(2)]
        public float PetalLength;

        [LoadColumn(3)]
        public float PetalWidth;

        [LoadColumn(4)]
        public string Label;
    }
}