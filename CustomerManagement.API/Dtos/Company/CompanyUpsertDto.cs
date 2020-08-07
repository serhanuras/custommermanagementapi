using CustomerManagement.API.Dtos.Interfaces;

namespace CustomerManagement.API.Dtos.Company
{
    public class CompanyUpsertDto:IUpsertDto
    {
        public string Name { get; set; }
        
        public string Address { get; set; }
    }
}