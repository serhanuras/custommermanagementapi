using CustomerManagement.Data.Interfaces;
using CustomerManagement.Entities;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.API.Services
{
    public class TitleService:ServiceBase<Title>
    {
        public TitleService(ILogger logger, IUnitOfWork unitOfWork, string includeProperties)
            : base(logger, unitOfWork, includeProperties)
        {
        }
    }
}