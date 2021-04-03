using System.Collections.Generic;
using Elearningfake.Models.ViewModels;

namespace Elearningfake.Models.Services.Application
{
    public interface ICorsoService
    {
         List<CorsiViewModel> GetCorsi();
         CorsoDetailViewModel GetCorso(int id);
    }
}