using System;

namespace ELearningFake.Models.Exceptions
{
    public class CorsoNonTrovatoException : Exception
    {
        public CorsoNonTrovatoException(int corsoId) : base ($"Corso {corsoId} non trovato")
        {
            
        }
    }
}