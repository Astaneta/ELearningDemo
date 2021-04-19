using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElearningDemo.Models.Services.Application;
using ElearningDemo.Models.ViewModels;
using ELearningDemo.Models.Services.Application;
using Microsoft.AspNetCore.Mvc;

namespace ELearningDemo.Controllers
{
    public class CorsiController : Controller
    {
        private readonly ICachedCorsoService corsiService;

        public CorsiController(ICachedCorsoService corsiService)
        {
            this.corsiService = corsiService;
        }
        
        public async Task<IActionResult> Index(
                                            string search = null,
                                            int page = 1,
                                            string orderBy = "price",
                                            bool ascending = true)
        {
            List<CorsiViewModel> corsi = await corsiService.GetCorsiAsync(search, page);
            ViewData["Titolo"] = "Catalogo corsi";
            return View(corsi);
        }

        public async Task<IActionResult> Detail(int id)
        {
            CorsoDetailViewModel corso = await corsiService.GetCorsoAsync(id);
            ViewData["Titolo"] = corso.Title;
            return View(corso);
        }
    }
}