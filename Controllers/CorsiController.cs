using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElearningDemo.Models.InputModels;
using ElearningDemo.Models.Services.Application;
using ElearningDemo.Models.ViewModels;
using ELearningDemo.Models.InputModels;
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
        
        public async Task<IActionResult> Index(CoursesListInputModel inputModel)
        {
            ListViewModel<CorsiViewModel> corsi = await corsiService.GetCorsiAsync(inputModel);
            CourseListViewModel courseListViewModel = new CourseListViewModel
            {
                Corsi = corsi,
                Input = inputModel
            };
            ViewData["Title"] = "Catalogo corsi";
            return View(courseListViewModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            CorsoDetailViewModel corso = await corsiService.GetCorsoAsync(id);
            ViewData["Title"] = corso.Title;
            return View(corso);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Nuovo Corso";
            var inputModel = new CourseCreateInputModel();
            return View(inputModel);
        }
        
        [HttpPost]
        public IActionResult Create(CourseCreateInputModel inputModel)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}