using CustomerManagement.Data.Interfaces;
using CustomerManagement.Entities;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.API.Services
{
    public class CompanyService:ServiceBase<Company>
    {
        public CompanyService(ILogger logger, IUnitOfWork unitOfWork, string includeProperties)
            : base(logger, unitOfWork, includeProperties)
        {
        }
    }
}