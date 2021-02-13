using System;

namespace EnrichMyCare.DataEntities.Entities
{
    public class BaseDataEntity
    {
        public DateTime? CreatedOn
        {
            get { return DateTime.Now; }
        }

        public DateTime? ModifiedOn
        {
            get { return DateTime.Now; }
        }

        public bool? IsActive { get; set; }        
    }
}
