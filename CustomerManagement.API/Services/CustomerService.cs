using CustomerManagement.Data.Interfaces;
using CustomerManagement.Entities;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.API.Services
{
    public class CustomerService:ServiceBase<Customer>
    {
        public CustomerService(ILogger logger, IUnitOfWork unitOfWork, string includeProperties)
            : base(logger, unitOfWork, includeProperties)
        {
        }
        
        
    }
}