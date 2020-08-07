using CustomerManagement.API.Dtos.Interfaces;

namespace CustomerManagement.API.Dtos.Title
{
    public class TitleUpsertDto : IUpsertDto
    {
        public string Value { get; set; }
        
        public string Description { get; set; }
    }
}