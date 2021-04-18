using System;
using System.Collections.Generic;
using Elearningfake.Models.ValueType;

namespace ELearningFake.Models.Entities
{
    public partial class Corso
    {
        public Corso(string titolo, string autore)
        {
            if(string.IsNullOrWhiteSpace(titolo))
            {
                throw new ArgumentException("Il titolo deve essere inserito");
            }
            if (string.IsNullOrWhiteSpace(autore))
            {
                throw new ArgumentException("L'autore deve essere inserito");
            }
            Titolo = titolo;
            Autore = autore;
            Lezioni = new HashSet<Lezione>();
        }

        public long Id { get; private set; }
        public string Titolo { get; private set; }
        public string Descrizione { get; private set; }
        public string ImagePath { get; private set; }
        public string Autore { get; private set; }
        public string Email { get; private set; }
        public double Rating { get; private set; }
        public Money PrezzoPieno {get; private set; }
        public Money PrezzoCorrente {get; private set; }

        public virtual ICollection<Lezione> Lezioni { get; set; }

        public void CambioTitolo(string nuovoTitolo)
        {
            if (string.IsNullOrWhiteSpace(nuovoTitolo))
            {
                throw new ArgumentException("Il titolo è obbligatorio");
            }
            Titolo = nuovoTitolo;
        }

        public void CambioPrezzo(Money nuovoPrezzoPieno, Money nuovoPrezzoScontato)
        {
            if (nuovoPrezzoPieno == null || nuovoPrezzoScontato == null)
            {
                throw new ArgumentException("Il prezzo non può essere nullo");
            }
            if (nuovoPrezzoPieno.Currency != nuovoPrezzoScontato.Currency)
            {
                throw new ArgumentException("La valuta non può essere diversa");
            }
            if (nuovoPrezzoPieno.Amount <= nuovoPrezzoScontato.Amount)
            {
                throw new ArgumentException("Il prezzo pieno non può essere inferiore o uguale al prezzo scontato");
            }
            PrezzoPieno = nuovoPrezzoPieno;
            PrezzoCorrente = nuovoPrezzoScontato;
        }
    }
}
