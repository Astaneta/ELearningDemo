using System.Collections.Generic;
using System.Threading.Tasks;
using ElearningDemo.Models.ViewModels;

namespace ElearningDemo.Models.Services.Application
{
    public interface ICorsoService
    {
         Task<List<CorsiViewModel>> GetCorsiAsync(string search);
         Task<CorsoDetailViewModel> GetCorsoAsync(int id);
    }
}