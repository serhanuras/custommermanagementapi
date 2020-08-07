using CustomerManagement.API.Dtos.Interfaces;

namespace CustomerManagement.API.Dtos.Title
{
    public class TitleDto:IDto
    {
        public long Id { get; set; }
        
        public string Value { get; set; }
        
        public string Description { get; set; }
    }
}