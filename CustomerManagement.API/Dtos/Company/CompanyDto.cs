using CustomerManagement.API.Dtos.Interfaces;

namespace CustomerManagement.API.Dtos.Company
{
    public class CompanyDto:IDto
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Address { get; set; }
    }
}