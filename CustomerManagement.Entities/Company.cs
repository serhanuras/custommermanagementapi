using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerManagement.Entities
{
    [Table("tbl_companies")]
    public class Company : EntityBase
    {
        [Column("name")]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Column("address")]
        [MaxLength(500)]
        public string Address { get; set; }

        public List<Customer> Customers  { get; set; }
    }
}