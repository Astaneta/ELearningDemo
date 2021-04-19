using System;
using System.Data;
using ElearningDemo.Models.Enums;
using ElearningDemo.Models.ValueType;

namespace ElearningDemo.Models.ViewModels
{
    public class CorsiViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Author { get; set; }
        public double Rating { get; set; }
        public Money FullPrice { get; set; }
        public Money CurrentPrice { get; set; }

        public static CorsiViewModel FromDataRow(DataRow item)
        {
            var corsiViewModel = new CorsiViewModel{
                Title = Convert.ToString(item["Title"]),
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
                Id = Convert.ToInt32(item["Id"])
            };
            return corsiViewModel;
        }
    }
}