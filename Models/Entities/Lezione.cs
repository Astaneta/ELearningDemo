using System;
using System.Collections.Generic;

namespace ELearningFake.Models.Entities
{
    public partial class Lezione
    {
        public long Id { get; set; }
        public long CorsoId { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public string Durata { get; set; }

        public virtual Corso Corso { get; set; }
    }
}
