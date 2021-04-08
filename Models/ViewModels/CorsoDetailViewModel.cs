using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Elearningfake.Models.Enums;
using Elearningfake.Models.ValueType;

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

        public static new CorsoDetailViewModel FromDataRow(DataRow item)
        {
            var corsiViewModel = new CorsoDetailViewModel{
                Title = Convert.ToString(item["Titolo"]),
                Descrizione = Convert.ToString(item["Descrizione"]),
                ImagePath = Convert.ToString(item["ImagePath"]),
                Author = Convert.ToString(item["Autore"]),
                Rating = Convert.ToDouble(item["Rating"]),
                FullPrice = new Money(
                    Enum.Parse<Valuta>(Convert.ToString(item["PrezzoPieno_Valuta"])),
                    Convert.ToDecimal(item["PrezzoPieno_Cifra"])
                ),
                CurrentPrice = new Money(
                    Enum.Parse<Valuta>(Convert.ToString(item["PrezzoCorrente_Valuta"])),
                    Convert.ToDecimal(item["PrezzoCorrente_Cifra"])
                ),
                Id = Convert.ToInt32(item["Id"]),
                Lezioni = new List<LezioneViewModel>()
            };
            return corsiViewModel;
        }
    }
}