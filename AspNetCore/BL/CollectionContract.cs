using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators.Internal;

namespace AspNetCore.BL
{
    public class SecontBonusCalculator : IBonusCalculator
    {
        private dynamic _bonusProcessor;
        
        public decimal CalculateBonus(decimal baseAmount, string uniqueId)
        {
            if(string.IsNullOrWhiteSpace(uniqueId))
            {
                throw new ArgumentException("A client must provide unique Id for calculation");
            }
            return  _bonusProcessor.CalculateAmount(baseAmount, uniqueId);
        }
    }
}