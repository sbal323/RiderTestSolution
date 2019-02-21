namespace ConsoleAppCS.Variance
{
    public class GenericVarianceTester: IVariantTesterIn<Base>, IVariantTesterOut<Descendant>
    {
        public void CallDo(Base experiment)
        {
            experiment.Do();
        }

        public Descendant GetInstance()
        {
            var instance = new Descendant();
            return instance;
        }
    }
}