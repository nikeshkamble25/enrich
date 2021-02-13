using EnrichMyCare.DataEntities.Entities;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EnrichMyCare.DataEntities
{
    public partial class ImageVideo : BaseDataEntity
    {
        public int ImageVideoId { get; set; }
        public string ImageVideoPath { get; set; }
        public int? ExcerciseId { get; set; }
        public virtual Excercise Excercise { get; set; }
    }
}
