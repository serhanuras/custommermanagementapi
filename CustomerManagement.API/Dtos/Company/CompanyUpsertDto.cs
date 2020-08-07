using CustomerManagement.API.Dtos.Interfaces;

namespace CustomerManagement.API.Dtos.Company
{
    public class CompanyCreationDto:ICreationDto
    {
        public string Name { get; set; }
        
        public string Address { get; set; }
    }
}