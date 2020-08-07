using CustomerManagement.API.Dtos.Interfaces;

namespace CustomerManagement.API.Dtos.Customer
{
    public class CustomerUpsertDto : IUpsertDto
    {
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public long CompanyId { get; set; }
        
        public long TitleId { get; set; }
    }
}