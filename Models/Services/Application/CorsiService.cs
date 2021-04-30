using System;
using System.Collections.Generic;
using Elearningfake.Models.Enums;
using Elearningfake.Models.Services.Application;
using Elearningfake.Models.ValueType;
using Elearningfake.Models.ViewModels;

namespace Elearningfake.Models.Services.Application
{
    public class CorsiService : ICorsoService
    {
        public List<CorsiViewModel> GetCorsi()
        {
            var CoursesList = new List<CorsiViewModel>();
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
                CoursesList.Add(corsi);
            }
            return CoursesList;
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