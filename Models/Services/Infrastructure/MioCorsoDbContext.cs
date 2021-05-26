using System;
using ELearningDemo.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ELearningDemo.Models.Services.Infrastructure
{
    public partial class MioCourseDbContext : DbContext
    {

        public MioCourseDbContext(DbContextOptions<MioCourseDbContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Lezione> Lezioni { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlite("Data Source=Data/MioCourse.db");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

           
            modelBuilder.Entity<Course>(entity =>
            {   
                #region Mapping generato automaticamente dal tool di reverse engineering
                /*
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Autore)
                    .IsRequired()
                    .HasColumnType("TEXT (100)");

                entity.Property(e => e.Descrizione).HasColumnType("TEXT (10000)");

                entity.Property(e => e.Email).HasColumnType("TEXT (100)");

                entity.Property(e => e.ImagePath).HasColumnType("TEXT (100)");

                entity.Property(e => e.PrezzoCorrenteCifra)
                    .IsRequired()
                    .HasColumnName("PrezzoCorrente_Cifra")
                    .HasColumnType("NUMERIC")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.PrezzoCorrenteValuta)
                    .IsRequired()
                    .HasColumnName("PrezzoCorrente_Valuta")
                    .HasColumnType("TEXT (3)")
                    .HasDefaultValueSql("'EUR'");

                entity.Property(e => e.PrezzoPienoCifra)
                    .IsRequired()
                    .HasColumnName("PrezzoPieno_Cifra")
                    .HasColumnType("NUMERIC")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.PrezzoPienoValuta)
                    .IsRequired()
                    .HasColumnName("PrezzoPieno_Valuta")
                    .HasColumnType("TEXT (3)")
                    .HasDefaultValueSql("'EUR'");

                entity.Property(e => e.Titolo)
                    .IsRequired()
                    .HasColumnType("TEXT (100)");
                    ;*/
                    #endregion
                
                entity.ToTable("Courses"); //Opzionale se la tabella si chiama come la proprietà che espone il DbSet

                entity.HasKey(corso => corso.Id); // Superfluo se la proprietà si chiama Id o CourseId

                //Mapping per gli owntype
                entity.OwnsOne(corso => corso.CurrentPrice, builder =>{
                    builder.Property(money => money.Currency)
                    .HasConversion<string>()
                    .HasColumnName("PrezzoCorrente_Valuta");
                    builder.Property(money => money.Amount).HasColumnName("PrezzoCorrente_Cifra");
                });
                //Rispettando la convenzione dei nomi, il costruttore andrà a cercare colonne con questo nome
                //PrezzoCorrente_Cifra
                //PrezzoCorrente_Valuta
                entity.OwnsOne(corso => corso.FullPrice, builder =>{
                    builder.Property(money => money.Currency)
                    .HasConversion<string>()
                    .HasColumnName("PrezzoPieno_Valuta");
                    builder.Property(money => money.Amount).HasColumnName("PrezzoPieno_Cifra");
                });
                
                //Mapping per le relazioni
                entity.HasMany(corso => corso.Lessons)
                                    .WithOne(lesson => lesson.Course)
                                    .HasForeignKey(lesson => lesson.CourseId); //Superflua se la proprietà si chiama CourseId

            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                #region Mapping generato automaticamente dal tool di reverse engineering
                /*
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Descrizione).HasColumnType("TEXT (10000)");

                entity.Property(e => e.Durata)
                    .IsRequired()
                    .HasColumnType("TEXT (8)")
                    .HasDefaultValueSql("'00:00:00'");

                entity.Property(e => e.Titolo)
                    .IsRequired()
                    .HasColumnType("TEXT (100)");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Lezioni)
                    .HasForeignKey(d => d.CourseId);
                    */
                    #endregion
            
                /* Facoltativa se già mappata dall'entità corso
                entity.HasOne(lezione => lezione.Course)
                                .WithMany(corso => corso.Lezioni)
                                .HasPrincipalKey(corso => corso.Id);
                */
            });
        }
    }
}
