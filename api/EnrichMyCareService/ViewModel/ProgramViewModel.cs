using System;
using System.Collections.Generic;

namespace EnrichMyCareService.ViewModel
{
    public class ProgramViewModel
    {
        public string PatientId { get; set; }
        public string ProgramName { get; set; }
        public DateTime? ProgramDate { get; set; }
        public IList<ExerciseViewModel> Exercises { get; set; }
        public string GoalObjective { get; set; }
    }
}