using System.Collections.Generic;
using ElearningDemo.Models.ViewModels;

namespace ElearningDemo.Models.ViewModels
{
    public class HomeListViewModel
    {
        public List<CorsiViewModel> BestCourses { get; set; }
        public List<CorsiViewModel> MostRecentCourses { get; set; }
    }
}