using System;

namespace elearningdemo.Models.Exceptions
{
    public class CourseTitleNotAvalaibleException : Exception
    {
        public CourseTitleNotAvalaibleException(Exception innerException) : base($"Il titolo del corso già esiste", innerException)
        {
            
        }
    }
}