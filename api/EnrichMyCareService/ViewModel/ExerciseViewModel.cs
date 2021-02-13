using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace EnrichMyCareService.ViewModel
{
    public class ExerciseViewModel
    {
        public string ExerciseName { get; set; }
        public DateTime? ReviewDate { get; set; } 
        public string Description { get; set; }
        public InitialProgressionViewModel InitialProgression { get; set; }
        public List<ProgressionViewModel> Progression { get; set; }
        public List<IFormFile> files { get; set; }
                           
    }
}