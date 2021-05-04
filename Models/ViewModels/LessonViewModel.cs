using System;
using System.Data;
using ELearningDemo.Models.Entities;

namespace ElearningDemo.Models.ViewModels
{
    public class LessonViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }

        public static LessonViewModel FromDataRow(DataRow item)
        {
            LessonViewModel lezioneVM = new LessonViewModel{
                Id = Convert.ToInt32(item["Id"]),
                Title = Convert.ToString(item["Title"]),
                Description = Convert.ToString(item["Description"]),
                Duration = TimeSpan.Parse(Convert.ToString(item["Duration"]))
            };
            return lezioneVM;
        }

        public static LessonViewModel FromEntity(Lesson lesson)
        {
            return new LessonViewModel
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Duration = lesson.Duration,
                Description = lesson.Description
            };
        }
    }
}