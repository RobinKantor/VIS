using System;
using System.Collections.Generic;
using System.Text;

namespace vis3
{
    public class Money
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Money(decimal amount, string currency)
        { 
            if (amount < 0)
            {
                throw new ArgumentException("Amount cannot be negative.");
            }

            this.Amount = amount;
            this.Currency = currency;
        }

        public Money Add(decimal amount)
        {
            return new Money(this.Amount + amount, Currency);
        }

        public Money Multiply(int pieces)
        {
            if (pieces < 0)
            {
                throw new ArgumentException("Pieces cannot be negative.");
            }

            return new Money(this.Amount * pieces, Currency);
        }

        public Money Multiply(decimal multiplier)
        {
            if (multiplier < 0)
            {
                throw new ArgumentException("Multiplier cannot be negative.");
            }

            return new Money(this.Amount * multiplier, Currency);
        }
    }
}
