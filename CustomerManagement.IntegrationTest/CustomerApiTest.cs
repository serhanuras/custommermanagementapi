using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CustomerManagement.API;
using CustomerManagement.API.Dtos.Title;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace CustomerManagement.IntegrationTest
{
    public class CustomerApiTest:IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;
        
        public CustomerApiTest(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async Task TitleGetAllTestAsync()
        {
            var request = "/api/titles";
            
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull( response.Headers.GetValues("X-Total-Count"));
        }
        
        [Fact]
        public async Task TitleCheckPostUpdateDeleteAsync()
        {
            //POST TEST
            var postRequest = new
            {
                Url = "/api/titles",
                Body = new
                {
                    value ="test title value",
                    name = "test tile desc"
                }
            };

            var postResponse = await Client.PostAsync(postRequest.Url, GetStringContent(postRequest.Body));
            var postJsonString = await postResponse.Content.ReadAsStringAsync();
            postResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);

            
            //UPDATE TEST
            var titleDto = JsonConvert.DeserializeObject<TitleDto>(postJsonString);
            var updateRequest = new
            {
                Url = "/api/titles",
                Body = new
                {
                    id= titleDto.Id,
                    value = "test title value updated",
                    name = "test tile desc updated"
                }
            };

            var updateResponse = await Client.PutAsync(updateRequest.Url, GetStringContent(updateRequest.Body));
            var updateJsonString = await updateResponse.Content.ReadAsStringAsync();
            updateResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);
            
            //DELETE TEST
            titleDto = JsonConvert.DeserializeObject<TitleDto>(postJsonString);
            var deleteRequest = new
            {
                Url = $"/api/titles/{titleDto.Id}"
            };

            var deleteResponse = await Client.DeleteAsync(deleteRequest.Url);
            updateResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);


        }
        
        private  StringContent GetStringContent(object obj)
            => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");
    }
    
    
}