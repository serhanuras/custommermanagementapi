using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerManagement.Entities
{
    public interface IEntityBase
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("created_on")]
        public DateTime CreatedOn { get; set; }

        [Column("updated_on")]
        public DateTime? UpdatedOn { get; set; }

        [Column("last_accessed")]
        public DateTime LastAccessed { get; set; }
        
        
    }
}