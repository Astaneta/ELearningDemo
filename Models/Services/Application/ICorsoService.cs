using System.Collections.Generic;
using System.Threading.Tasks;
using elearningdemo.Models.ViewModels;
using ElearningDemo.Models.ViewModels;
using ELearningDemo.Models.InputModels;

namespace ElearningDemo.Models.Services.Application
{
    public interface ICorsoService
    {
         Task<ListViewModel<CorsiViewModel>> GetCorsiAsync(CorsiListaInputModel model);
         Task<CorsoDetailViewModel> GetCorsoAsync(int id);
    }
}