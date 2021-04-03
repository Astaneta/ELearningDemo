using System;
using System.Collections.Generic;
using Elearningfake.Models.Services.Application;
using Elearningfake.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ELearningfake.Controllers
{
    public class CorsiController : Controller
    {
        private readonly ICorsoService corsiService;

        public CorsiController(ICorsoService corsiService)
        {
            this.corsiService = corsiService;

        }
        public IActionResult Index()
        {
            List<CorsiViewModel> corsi = corsiService.GetCorsi();
            ViewData["Titolo"] = "Catalogo corsi";
            return View(corsi);
        }

        public IActionResult Detail(int id)
        {
            CorsoDetailViewModel corso = corsiService.GetCorso(id);
            ViewData["Titolo"] = corso.Title;
            return View(corso);
        }
    }
}