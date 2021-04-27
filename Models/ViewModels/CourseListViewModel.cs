using System.Collections.Generic;
using ELearningDemo.Models.InputModels;

namespace ElearningDemo.Models.ViewModels
{
    public class CourseListViewModel
    {
        public List<CorsiViewModel> Corsi { get; set; }
        public CorsiListaInputModel Input { get; set; }
    }
}