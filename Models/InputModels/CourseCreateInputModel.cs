using System.ComponentModel.DataAnnotations;

namespace ElearningDemo.Models.InputModels
{
    public class CourseCreateInputModel
    {
        [Required (ErrorMessage = "Il titolo è obbligatorio"),
         MinLength(10, ErrorMessage = "Il titolo deve contenere almeno {1} caratteri"), 
         MaxLength(100, ErrorMessage = "Il titolo può contenere al massimo {1} caratteri"), 
         RegularExpression(@"^[\w\s\.]+$", ErrorMessage = "Il titolo può essere formato solo da caratteri alfanumerici, numeri e punti")]
        public string Title { get; set; }
    }
}