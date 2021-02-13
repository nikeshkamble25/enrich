using EnrichMyCare.DataEntities.Entities;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EnrichMyCare.DataEntities
{
    public partial class PatientProgram : BaseDataEntity
    {
        public int PatientProgramId { get; set; }
        public int? PatientId { get; set; }
        public int? ProgramId { get; set; }
        public Guid? CreatedById { get; set; }
        public Guid? ModifiedById { get; set; }
        
        public virtual Patient Patient { get; set; }
        public virtual Program Program { get; set; }
    }
}
