namespace AspNetCore.BL
{
    public class FirstBonusCalculator : IBonusCalculator
    {
        private dynamic _bonusProcessor;
        
        public decimal CalculateBonus(decimal baseAmount, string uniqueId)
        {
            decimal result = _bonusProcessor.CalculateAmount(baseAmount);
           
            if (result < 0)
            {
                result = 0;
            }

            return result;
        }
    }
}