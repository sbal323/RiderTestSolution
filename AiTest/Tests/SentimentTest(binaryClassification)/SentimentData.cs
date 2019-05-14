using Microsoft.ML.Data;
namespace AiTest.Tests
{
    public class SentimentData
    {
        [LoadColumn(0)] 
        public bool Label;

        [LoadColumn(1)] 
        public string Text;
    }
}