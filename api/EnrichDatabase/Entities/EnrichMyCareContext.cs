using System;
using EnrichMyCare.DataEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EnrichMyCare.EnrichDatabase.Entities
{
    public partial class EnrichMyCareContext : DbContext
    {
        public EnrichMyCareContext()
        {
        }

        public EnrichMyCareContext(DbContextOptions<EnrichMyCareContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Excercise> Excercise { get; set; }
        public virtual DbSet<ImageVideo> ImageVideo { get; set; }
        public virtual DbSet<InitialProgression> InitialProgression { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<PatientProgram> PatientProgram { get; set; }
        public virtual DbSet<Program> Program { get; set; }
        public virtual DbSet<Progression> Progression { get; set; }

    }
}
