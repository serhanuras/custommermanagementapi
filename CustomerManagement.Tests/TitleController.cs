using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomerManagement.Tests
{
    [TestClass]
    public class TitleControllerTest
    {
        private IConfigurationRoot _configuration;

        public TitleControllerTest()
        {
            var builder = new ConfigurationBuilder()	              
                .SetBasePath(Directory.GetCurrentDirectory())	              .AddJsonFile("appsettings.json");
        }
        
        
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}