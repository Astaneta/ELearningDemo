using System;
using System.Collections.Generic;
using elearningfake.Models.Enums;
using elearningfake.Models.ValueType;
using Elearningfake.Models.ViewModels;

namespace Elearningfake.Models.Application
{
    public class CorsiService
    {
        public List<CorsiViewModel> GetCorsi()
        {
            var corsiLista = new List<CorsiViewModel>();
            var rand = new Random();
            for (int i = 1; i <= 20; i++)
            {
                var price = Convert.ToDecimal(rand.NextDouble() * 10 + 10);
                var corsi = new CorsiViewModel
                {
                    Id = i,
                    Title = $"Corso {i}",
                    CurrentPrice = new Money(Currency.EUR, price),
                    FullPrice = new Money(Currency.EUR, (rand.NextDouble() * 10) > 5 ? price : price +2),
                    Author = "Nome&Cognome",
                    Rating = rand.NextDouble() * 5.0,
                    ImagePath = "~/Free_logo.svg"
                };
                corsiLista.Add(corsi);
            }
            return corsiLista;
        }

        public CorsoDetailViewModel GetCorso(int id)
        {
            var rand = new Random();
            var price = Convert.ToDecimal(rand.NextDouble() * 10 + 10);
            var corso = new CorsoDetailViewModel
            {
                Id = id,
                Title = $"Corso {id}",
                CurrentPrice = new Money(Currency.EUR, price),
                FullPrice = new Money(Currency.EUR, (rand.NextDouble() * 10) > 5 ? price : price +2),
                Author = "Nome&Cognome",
                Rating = rand.NextDouble() * 5,
                ImagePath = "~/Free_logo.svg",
                Descrizione = "Qui ci va la descrizione",
                Lezioni = new List<LezioneViewModel>()
            };

            for (int i = 1; i <= 5; i++)
            {
                var lezioni = new LezioneViewModel{
                    Titolo = $"Lezione {i}",
                    Durata = TimeSpan.FromSeconds(rand.Next(40, 90))
                };
                corso.Lezioni.Add(lezioni);
            }

            return corso;
        }
    }
}