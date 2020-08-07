using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerManagement.Entities
{
    [Table("tbl_titles")]
    public class Title :EntityBase
    {
        [Column("value")]
        [MaxLength(100)]
        public string Value { get; set; }
        
        [Column("description")]
        [MaxLength(1000)]
        public string Description { get; set; }
        
        public List<Customer> Customers { get; set; }

    }
}