using EnrichMyCare.DataEntities.Entities;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EnrichMyCare.DataEntities
{
    public partial class Progression : BaseDataEntity
    {
        public int ProgressionId { get; set; }
        public int? ProgressionReps { get; set; }
        public int? ProgressionPerWeek { get; set; }
        public int? ProgressionTimePeriod { get; set; }
        public int? InitialProgressionId { get; set; }
        public Guid? CreatedById { get; set; }
        public Guid? ModifiedById { get; set; }
        
        public virtual InitialProgression InitialProgression { get; set; }
    }
}
