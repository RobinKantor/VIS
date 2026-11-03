using System;
using System.Collections.Generic;
using System.Text;

namespace vis3
{
    public class NoDiscount : DiscountPolicy
    {
        public override Money GetDiscountedPrice(Money originalPrice)
        {
            return originalPrice;
        }
    }
}
