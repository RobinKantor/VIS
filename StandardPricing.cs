using System;
using System.Collections.Generic;
using System.Text;

namespace vis3
{
    public class StandardPricing : IPricingStrategy
    {
        public Money GetPrice(IEnumerable<Money> itemPrices)
        {
            Money total = new Money(0, "CZK");

            foreach (Money price in itemPrices)
            {
                total = total.Add(price.Amount);
            }

            return total;
        }
    }
}
