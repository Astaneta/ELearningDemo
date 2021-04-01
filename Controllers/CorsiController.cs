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

        public IActionResult Detail(string id)
        {
            return View();
        }
    }
}