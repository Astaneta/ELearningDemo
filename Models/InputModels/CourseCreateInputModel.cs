using System.ComponentModel.DataAnnotations;
using ELearningDemo.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ElearningDemo.Models.InputModels
{
    public class CourseCreateInputModel
    {
        [Required (ErrorMessage = "Il titolo è obbligatorio"),
         MinLength(10, ErrorMessage = "Il titolo deve contenere almeno {1} caratteri"), 
         MaxLength(100, ErrorMessage = "Il titolo può contenere al massimo {1} caratteri"), 
         RegularExpression(@"^[\w\s\.]+$", ErrorMessage = "Il titolo può essere formato solo da caratteri alfanumerici, numeri e punti"),
         Remote(action: nameof(CoursesController.IsTitleAvailable), controller: "Courses", ErrorMessage = "Il titolo già esiste")]
        public string Title { get; set; }
    }
}