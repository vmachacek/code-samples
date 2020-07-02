using System;

namespace ProcessingPipeline.Domain.ValueObjects
{
    public class Money : IEquatable<Money>
    {
        public Money(string currency, decimal amount)
        {
            this.Currency = currency;
            this.Amount = amount;
        }

        public string Currency { get; private set; }
        public decimal Amount { get; private set; }

        public bool Equals(Money other)
        {
            if (object.ReferenceEquals(other, null)) return false;
            if (object.ReferenceEquals(other, this)) return true;
            return this.Currency.Equals(other.Currency) && this.Amount.Equals(other.Amount);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Money);
        }

        public Money Multiply(decimal d)
        {
            return new Money(Currency, Amount * d);
        }

        public Money Add(decimal d)
        {
            return new Money(Currency, Amount + d);
        }

        public Money Round()
        {
            this.Amount = Math.Round(this.Amount, 2);
            return this;
        }

        public static string DefaultCurrency = "EUR";

        public static Money Zero => new Money(DefaultCurrency, 0);

        public override int GetHashCode()
        {
            var a = this.Currency?.GetHashCode() ?? "USD".GetHashCode();
            var b = this.Amount.GetHashCode();

            return a ^ b;
        }
    }
}
