namespace AspNetCore.BL
{
    public interface IBonusCalculator
    {
        decimal CalculateBonus(decimal baseAmount, string uniqueId);
    }
}