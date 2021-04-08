using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ELearningFake.Models.Entities
{
    public partial class MioCorsoDbContext : DbContext
    {
        public MioCorsoDbContext()
        {
        }

        public MioCorsoDbContext(DbContextOptions<MioCorsoDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Corso> Corsi { get; set; }
        public virtual DbSet<Lezione> Lezioni { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlite("Data Source=Data/MioCorso.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

           
            modelBuilder.Entity<Corso>(entity =>
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
                
                entity.ToTable("Corsi"); //Opzionale se la tabella si chiama come la proprietà che espone il DbSet

                entity.HasKey(corso => corso.Id); // Superfluo se la proprietà si chiama Id o CorsiId

                //Mapping per gli owntype
                entity.OwnsOne(corso => corso.PrezzoCorrente, builder =>{
                    builder.Property(money => money.Valuta)
                    .HasConversion<string>()
                    .HasColumnName("PrezzoCorrente_Valuta");
                    builder.Property(money => money.Cifra).HasColumnName("PrezzoCorrente_Cifra");
                });
                //Rispettando la convenzione dei nomi, il costruttore andrà a cercare colonne con questo nome
                //PrezzoCorrente_Cifra
                //PrezzoCorrente_Valuta
                entity.OwnsOne(corso => corso.PrezzoPieno, builder =>{
                    builder.Property(money => money.Valuta)
                    .HasConversion<string>()
                    .HasColumnName("PrezzoPieno_Valuta");
                    builder.Property(money => money.Cifra).HasColumnName("PrezzoPieno_Cifra");
                });
                
                //Mapping per le relazioni
                entity.HasMany(corso => corso.Lezioni)
                                    .WithOne(lesson => lesson.Corso)
                                    .HasForeignKey(lesson => lesson.CorsoId); //Superflua se la proprietà si chiama CorsoId

            });

            modelBuilder.Entity<Lezione>(entity =>
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

                entity.HasOne(d => d.Corso)
                    .WithMany(p => p.Lezioni)
                    .HasForeignKey(d => d.CorsoId);
                    */
                    #endregion
            
                /* Facoltativa se già mappata dall'entità corso
                entity.HasOne(lezione => lezione.Corso)
                                .WithMany(corso => corso.Lezioni)
                                .HasPrincipalKey(corso => corso.Id);
                */
            });
        }
    }
}
