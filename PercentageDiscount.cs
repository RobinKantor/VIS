using System;
using System.Collections.Generic;
using System.Text;

namespace vis3
{
    public class PercentageDiscount : DiscountPolicy
    {
        public int Percentage { get; set; }

        public override Money GetDiscountedPrice(Money originalPrice)
        {
            return originalPrice.Multiply((100 - Percentage) / 100m);
        }
    }
}
