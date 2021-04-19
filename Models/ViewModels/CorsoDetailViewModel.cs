using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ElearningDemo.Models.Enums;
using ElearningDemo.Models.ValueType;

namespace ElearningDemo.Models.ViewModels
{
    public class CorsoDetailViewModel : CorsiViewModel
    {
        public string Description { get; set; }
        public List<LezioneViewModel> Lessons {get; set;}

        public TimeSpan DurataTotaleCorso 
        { 
            get => TimeSpan.FromSeconds(Lessons?.Sum (l => l.Duration.TotalSeconds) ?? 0 );
        }

        public static new CorsoDetailViewModel FromDataRow(DataRow item)
        {
            var corsiViewModel = new CorsoDetailViewModel{
                Title = Convert.ToString(item["Title"]),
                Description = Convert.ToString(item["Description"]),
                ImagePath = Convert.ToString(item["ImagePath"]),
                Author = Convert.ToString(item["Author"]),
                Rating = Convert.ToDouble(item["Rating"]),
                FullPrice = new Money(
                    Enum.Parse<Currency>(Convert.ToString(item["FullPrice_Currency"])),
                    Convert.ToDecimal(item["FullPrice_Amount"])
                ),
                CurrentPrice = new Money(
                    Enum.Parse<Currency>(Convert.ToString(item["CurrentPrice_Currency"])),
                    Convert.ToDecimal(item["CurrentPrice_Amount"])
                ),
                Id = Convert.ToInt32(item["Id"]),
                Lessons = new List<LezioneViewModel>()
            };
            return corsiViewModel;
        }
    }
}