namespace ConsoleAppCS.Variance
{
    public interface IVariantTesterIn<in T> where T: Base
    {
        void CallDo(T experiment);
    }
}