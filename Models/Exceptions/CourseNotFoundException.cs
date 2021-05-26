using System;

namespace ELearningDemo.Models.Exceptions
{
    public class CourseNotFoundException : Exception
    {
        public CourseNotFoundException(int corsoId) : base ($"Course {corsoId} non trovato")
        {
            
        }
    }
}