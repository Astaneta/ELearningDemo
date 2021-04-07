using System.Collections.Generic;
using System.Threading.Tasks;
using Elearningfake.Models.ViewModels;

namespace Elearningfake.Models.Services.Application
{
    public interface ICorsoService
    {
         Task<List<CorsiViewModel>> GetCorsiAsync();
         Task<CorsoDetailViewModel> GetCorsoAsync(int id);
    }
}