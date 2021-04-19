using System.Collections.Generic;
using System.Threading.Tasks;
using ElearningDemo.Models.ViewModels;

namespace ElearningDemo.Models.Services.Application
{
    public interface ICorsoService
    {
         Task<List<CorsiViewModel>> GetCorsiAsync(string search, int page, string orderBy, bool ascending);
         Task<CorsoDetailViewModel> GetCorsoAsync(int id);
    }
}