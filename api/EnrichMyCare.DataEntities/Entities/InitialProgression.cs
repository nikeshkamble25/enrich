using EnrichMyCare.DataEntities.Entities;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EnrichMyCare.DataEntities
{
    public partial class InitialProgression : BaseDataEntity
    {
        public InitialProgression()
        {
            Progression = new List<Progression>();
        }

        public int InitialProgressionId { get; set; }
        public int? InitialProgressionReps { get; set; }
        public int? InitialProgressionPerWeek { get; set; }
        public int? InitialProgressionTimePeriod { get; set; }
        public int? ExcerciseId { get; set; }
        public Guid? CreatedById { get; set; }
        public Guid? ModifiedById { get; set; }
        
        public virtual Excercise Excercise { get; set; }
        public virtual IList<Progression> Progression { get; set; }
    }
}
