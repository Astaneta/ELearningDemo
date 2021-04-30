using System.Collections.Generic;
using System.Threading.Tasks;
using ElearningDemo.Models.ViewModels;
using ELearningDemo.Models.InputModels;

namespace ElearningDemo.Models.Services.Application
{
    public interface ICorsoService
    {
         Task<ListViewModel<CorsiViewModel>> GetCorsiAsync(CoursesListInputModel model);
         Task<List<CorsiViewModel>> GetBestCourseAsync();
         Task<List<CorsiViewModel>> GetMostRecentCourseAsync();
         Task<CorsoDetailViewModel> GetCorsoAsync(int id);
    }
}