using System;
using System.Collections.Generic;

namespace ELearningDemo.Models.Entities
{
    public partial class Lezione
    {
        public long Id { get; set; }
        public long CourseId { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public TimeSpan Durata { get; set; }

        public virtual Course Course { get; set; }
    }
}
