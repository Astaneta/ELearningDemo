using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using elearningdemo.Models.Exceptions;
using ElearningDemo.Models.InputModels;
using ElearningDemo.Models.Services.Application;
using ElearningDemo.Models.ViewModels;
using ELearningDemo.Models.InputModels;
using ELearningDemo.Models.Services.Application;
using Microsoft.AspNetCore.Mvc;

namespace ELearningDemo.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICachedCourseService courseService;

        public CoursesController(ICachedCourseService courseService)
        {
            this.courseService = courseService;
        }
        
        public async Task<IActionResult> Index(CoursesListInputModel inputModel)
        {
            ListViewModel<CoursesViewModel> corsi = await courseService.GetCoursesAsync(inputModel);
            CourseListViewModel courseListViewModel = new CourseListViewModel
            {
                Course = corsi,
                Input = inputModel
            };
            ViewData["Title"] = "Catalogo corsi";
            return View(courseListViewModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            CourseDetailViewModel corso = await courseService.GetCourseAsync(id);
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
        public async Task<IActionResult> Create(CourseCreateInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CourseDetailViewModel courseId = await courseService.CreateCourseAsync(inputModel);
                    return RedirectToAction(nameof(Index));
                }
                catch (CourseTitleNotAvalaibleException)
                {
                    ModelState.AddModelError(nameof(CourseDetailViewModel.Title), "Il titolo del corso già esiste");
                }
            }
            
            ViewData["Title"] = "Nuovo Corso";
            return View(inputModel);
        }
    }
}