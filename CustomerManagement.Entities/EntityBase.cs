using System;

namespace CustomerManagement.Entities
{
    public class EntityBase:IEntityBase
    {
        public long Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime LastAccessed { get; set; }
    }
}