using Microsoft.ML.Data;

namespace AiTest.Tests
{
    public class SentimentData
    {
        [Column(ordinal: "0", name: "Label")] 
        public float Sentiment;

        [Column(ordinal: "1")] 
        public string SentimentText;
    }
}