using System;
using System.Collections.Generic;
using System.Text;

namespace vis3
{
    public interface IPricingStrategy
    {
        Money GetPrice (IEnumerable<Money> itemPrices);
    }
}
