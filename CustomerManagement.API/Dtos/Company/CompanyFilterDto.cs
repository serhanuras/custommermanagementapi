using CustomerManagement.API.Dtos.Interfaces;

namespace CustomerManagement.API.Dtos.Company
{
    public class CompanyFilterDto:IFilterDto
    {
        public string Name { get; set; }
    }
}