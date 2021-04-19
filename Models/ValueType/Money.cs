using System;
using ElearningDemo.Models.Enums;

namespace ElearningDemo.Models.ValueType
{
    public class Money
    {   
        /*This richiama un costruttore della stessa classe*/
        public Money() : this(Currency.EUR, 0.00m) 
        {

        }

        public Money(Currency currency, decimal amount)
        {
            Amount = amount;
        }
        
        private decimal amount = 0;
        public decimal Amount 
        { 
            get
            {
                return amount;
            }
            set
            {
                if (value < 0)
                {
                    throw new InvalidOperationException("L'importo non può essere negativo");
                }
                amount = value;
            }
        }

        public Currency Currency {get; set;}

        public override bool Equals(object obj)
        {
            var money = obj as Money;
            return  money != null &&
                    Amount == money.Amount &&
                    Currency == money.Currency;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Amount, Currency);
        }

        public override string ToString()
        {
            return $"{Currency} {Amount:#.00}";
        }
    }
}