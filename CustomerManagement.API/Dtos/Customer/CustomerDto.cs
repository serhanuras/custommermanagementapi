using CustomerManagement.API.Dtos.Interfaces;

namespace CustomerManagement.API.Dtos.Customer
{
    public class CustomerDto : IDto
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string CompanyName { get; set; }
        
        public long CompanyId { get; set; }
        
        public string Title { get; set; }
        
        public long TitleId { get; set; }
    }
}