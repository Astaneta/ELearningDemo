using System;

namespace ELearningDemo.Models.Exceptions
{
    public class CourseNonTrovatoException : Exception
    {
        public CourseNonTrovatoException(int corsoId) : base ($"Course {corsoId} non trovato")
        {
            
        }
    }
}