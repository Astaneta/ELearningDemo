using ELearningDemo.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ELearningDemo.Models.Services.Infrastructure
{
    public class MyCourseDbContext : DbContext
    {
        public MyCourseDbContext(DbContextOptions<MyCourseDbContext> option) :base(option)
        {
            
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            // Mappatura dell'entity Course
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Courses");

                entity.HasKey(course => course.Id);

                entity.OwnsOne(corso => corso.CurrentPrice, builder =>
                {
                    builder.Property(money => money.Currency)
                    .HasConversion<string>()
                    .HasColumnName("CurrentPrice_Currency");
                    builder.Property(money => money.Amount)
                    .HasColumnName("CurrentPrice_Amount")
                    .HasConversion<double>();
                });

                entity.OwnsOne(corso => corso.FullPrice, builder =>
                {
                    builder.Property(money => money.Currency)
                    .HasConversion<string>()
                    .HasColumnName("FullPrice_Currency");
                    builder.Property(money => money.Amount)
                    .HasColumnName("FullPrice_Amount")
                    .HasConversion<double>();
                });

                entity.HasMany(course => course.Lessons)
                    .WithOne(lesson => lesson.Course)
                    .HasForeignKey(lesson => lesson.CourseId);
            });

            //Mappatura dell'entity Lesson
            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.ToTable("Lessons");

                entity.HasKey(lesson => lesson.Id);
            });
        }
    }
}