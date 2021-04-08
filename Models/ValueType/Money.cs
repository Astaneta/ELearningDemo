using System;
using Elearningfake.Models.Enums;

namespace Elearningfake.Models.ValueType
{
    public class Money
    {   
        /*This richiama un costruttore della stessa classe*/
        public Money() : this(Valuta.EUR, 0.00m) 
        {

        }

        public Money(Valuta valuta, decimal cifra)
        {
            Cifra = cifra;
        }
        private decimal cifra = 0;
        public decimal Cifra 
        { 
            get
            {
                return cifra;
            }
            set
            {
                if (value < 0)
                {
                    throw new InvalidOperationException("L'importo non può essere negativo");
                }
                cifra = value;
            }
        }

        public Valuta Valuta {get; set;}

        public override bool Equals(object obj)
        {
            var money = obj as Money;
            return  money != null &&
                    Cifra == money.Cifra &&
                    Valuta == money.Valuta;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Cifra, Valuta);
        }

        public override string ToString()
        {
            return $"{Valuta} {Cifra:#.00}";
        }
    }
}