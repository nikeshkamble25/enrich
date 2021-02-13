using EnrichMyCare.DataEntities.Entities;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EnrichMyCare.DataEntities
{
    public partial class Program : BaseDataEntity
    {
        public Program()
        {
            Excercise = new List<Excercise>();
            PatientProgram = new List<PatientProgram>();
        }

        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public DateTime? ProgramDate { get; set; }
        public string GoalsObjectives { get; set; }
        public Guid? CreatedById { get; set; }
        public Guid? ModifiedById { get; set; }
        
        public virtual IList<Excercise> Excercise { get; set; }
        public virtual IList<PatientProgram> PatientProgram { get; set; }
    }
}
