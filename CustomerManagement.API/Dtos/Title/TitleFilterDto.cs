using CustomerManagement.API.Dtos.Interfaces;

namespace CustomerManagement.API.Dtos.Title
{
    public class TitleFilterDto:IFilterDto
    {
        public string Value { get; set; }
    }
}