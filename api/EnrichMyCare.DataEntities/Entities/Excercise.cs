using EnrichMyCare.DataEntities.Entities;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EnrichMyCare.DataEntities
{
    public partial class Excercise : BaseDataEntity
    {
        public Excercise()
        {
            ImageVideo = new List<ImageVideo>();
            InitialProgression = new InitialProgression();
        }

        public int ExcerciseId { get; set; }
        public string ExcerciseName { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string Description { get; set; }
        public int? ProgramId { get; set; }
        public Guid? CreatedById { get; set; }
        public Guid? ModifiedById { get; set; }        
        public virtual Program Program { get; set; }
        public virtual InitialProgression InitialProgression { get; set; }
        public virtual IList<ImageVideo> ImageVideo { get; set; }
    }
}
