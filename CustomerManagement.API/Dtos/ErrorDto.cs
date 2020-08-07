using System.Runtime.Serialization;

namespace CustomerManagement.API.Dtos
{
    [DataContract]
    public class ErrorDto
    {
        public ErrorDto()
        {
            
        }

        public ErrorDto(string message)
        {
            this.Message = message;

        }
        
        [DataMember(EmitDefaultValue = false)]
        public string Message { get; set; }
    }
}