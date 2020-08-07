using CustomerManagement.API.Services;
using CustomerManagement.Data.Implementations;
using CustomerManagement.Data.Interfaces;
using CustomerManagement.Entities;
using CustomerManagement.Test.FakeImplementations;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace CustomerManagement.Test
{
    public class CompanyServiceTest
    {

        private readonly CustomerService _customerService;
        
        public CompanyServiceTest()
        {
            ILogger logger = new LoggerFake();
            IUnitOfWork unitOfWork = new UnitOfWork<Company>();
            _customerService = new CustomerService(logger);
            
        }
    }
}