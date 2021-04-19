using System;

namespace ELearningDemo.Models.Entities
{
    public class Lesson
    {
        public long Id { get; set; }
        public long CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }

        public virtual Course Course { get; set; }
    }
}