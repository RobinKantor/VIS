using System;
using System.Collections.Generic;
using System.Text;

namespace vis3
{
    public abstract class DiscountPolicy
    {
        public abstract Money GetDiscountedPrice(Money originalPriced);
    }
}
