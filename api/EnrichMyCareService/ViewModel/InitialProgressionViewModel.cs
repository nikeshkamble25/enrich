using System;
using System.Collections.Generic;

namespace EnrichMyCareService.ViewModel
{
    public class InitialProgressionViewModel
    {
        public int? InitialProgressionReps { get; set; }
        public int? InitialProgressionPerWeek { get; set; }
        public int? InitialProgressionTimePeriod { get; set; }
        public IList<ProgressionViewModel> progressions { get; set; }
    }
}