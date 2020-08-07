using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerManagement.Entities
{
    [Table("tbl_customers")]
    public class Customer:EntityBase
    {
        [Column("name")]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Column("surname")]
        [MaxLength(50)]
        public string Surname { get; set; }
        
        public Company Company { get; set; }
        
        [Column("company_id")]
        public long CompanyId { get; set; }

        public Title Title { get; set; }
        
        [Column("title_id")]
        public long TitleId { get; set; }


    }
}