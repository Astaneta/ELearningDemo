using System.Collections.Generic;
using ElearningDemo.Models.ViewModels;

namespace ElearningDemo.Models.ViewModels
{
    public class HomeListViewModel
    {
        public List<CoursesViewModel> BestCourses { get; set; }
        public List<CoursesViewModel> MostRecentCourses { get; set; }
    }
}