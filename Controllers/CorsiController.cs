using System;
using System.Collections.Generic;
using Elearningfake.Models.Application;
using Elearningfake.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ELearningFake.Controllers
{
    public class CorsiController : Controller
    {
        public IActionResult Index()
        {
            var corsiService = new CorsiService();
            List<CorsiViewModel> corsi = corsiService.GetCorsi();
            return View(corsi);
        }

        public IActionResult Detail(int id)
        {
            var corsiService = new CorsiService();
            CorsoDetailViewModel corsi = corsiService.GetCorso(id);
            return View(corsi);
        }
    }
}