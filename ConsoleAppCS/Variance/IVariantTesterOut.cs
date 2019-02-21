namespace ConsoleAppCS.Variance
{
    public interface IVariantTesterOut<out T> where T: Descendant
    {
        T GetInstance();
    }
}