using System.Collections.Generic;

namespace elearningdemo.Models.ViewModels
{
    public class ListViewModel<T>
    {
        public List<T> Result { get; set; }
        public int TotalCount { get; set; }
    }
}