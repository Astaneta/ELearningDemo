using ElearningDemo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ElearningDemo.Customization.ViewComponents
{
    public class PaginationBarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IPaginationInfo viewModel)
        {
            return View(viewModel);
        }
    }
}