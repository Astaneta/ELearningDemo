using System;
using System.Data;
using Elearningfake.Models.Enums;
using Elearningfake.Models.ValueType;

namespace Elearningfake.Models.ViewModels
{
    public class CorsiViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Author { get; set; }
        public double Rating { get; set; }
        public Money FullPrice { get; set; }
        public Money CurrentPrice { get; set; }

        public static CorsiViewModel FromDataRow(DataRow item)
        {
            var corsiViewModel = new CorsiViewModel{
                Title = Convert.ToString(item["Titolo"]),
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
                Id = Convert.ToInt32(item["Id"])
            };
            return corsiViewModel;
        }
    }
}