using elearningfake.Models.ValueType;

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
    }
}