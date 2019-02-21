namespace ConsoleAppCS.Variance
{
    public class VarianceTester
    {
        public static void CallDo(Base experiment)
        {
            experiment.Do();
        }

        public static Descendant GetInstance()
        {
            var instance = new Descendant();
            return instance;
        }
    }
}