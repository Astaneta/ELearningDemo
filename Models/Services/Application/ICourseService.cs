using System.Collections.Generic;
using System.Threading.Tasks;
using ElearningDemo.Models.InputModels;
using ElearningDemo.Models.ViewModels;
using ELearningDemo.Models.InputModels;

namespace ElearningDemo.Models.Services.Application
{
    public interface ICourseService
    {
         Task<ListViewModel<CoursesViewModel>> GetCoursesAsync(CoursesListInputModel model);
         Task<List<CoursesViewModel>> GetBestCourseAsync();
         Task<List<CoursesViewModel>> GetMostRecentCourseAsync();
         Task<CourseDetailViewModel> GetCourseAsync(int id);
         Task<CourseDetailViewModel> CreateCourseAsync(CourseCreateInputModel inputModel);
         Task<bool> IsTitleAvailableAsync(string title);
    }
}