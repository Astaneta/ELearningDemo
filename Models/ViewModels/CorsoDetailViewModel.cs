using System;
using System.Collections.Generic;
using System.Linq;

namespace Elearningfake.Models.ViewModels
{
    public class CorsoDetailViewModel : CorsiViewModel
    {
        public string Descrizione { get; set; }
        public List<LezioneViewModel> Lezioni {get; set;}

        public TimeSpan DurataTotaleCorso 
        { 
            get => TimeSpan.FromSeconds(Lezioni?.Sum (l => l.Durata.TotalSeconds) ?? 0 );
        }
    }
}