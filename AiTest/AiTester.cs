namespace AiTest
{
    public class AiTester
    {
        private readonly IAiTest _test;
        
        public AiTester(IAiTest test)
        {
            _test = test;
        }

        public AiTester()
        {
            _test = new StubAiTest();
        }

        public void Execute()
        {
            _test.Train();
            _test.Evaluate();
            _test.Predict();
        }

        public AiTester SetTest(IAiTest test)
        {
            return new AiTester(test);
        }
    }
}