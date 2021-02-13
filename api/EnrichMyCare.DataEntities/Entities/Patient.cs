using EnrichMyCare.DataEntities.Entities;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EnrichMyCare.DataEntities
{
    public partial class Patient : BaseDataEntity
    {
        public Patient()
        {
            PatientProgram = new List<PatientProgram>();
        }

        public int PatientId { get; set; }
        public string PatientNumber { get; set; }
        public string PatientName { get; set; }
        public Guid? CreatedById { get; set; }
        public Guid? ModifiedById { get; set; }
        
        public virtual IList<PatientProgram> PatientProgram { get; set; }
    }
}
