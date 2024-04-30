using Newtonsoft.Json;
using TestProject2024.Models;
using TestProject2024.Utilities;

namespace TestProject2024
{
    public class Tests
    {
        private HttpClientHelper _handler;
        private string name;
        [SetUp]
        public void Setup()
        {
            _handler = new HttpClientHelper();
            var name = new Guid(Guid.NewGuid().ToString());
        }

        [Test]
        public async Task GetAllUsers()
        {
            var responseFromHelper = await _handler.GETALL("users");

            Assert.AreEqual("OK", responseFromHelper.StatusCode.ToString());
        }

        [Test]

        public async Task CreateUser()
        {
            var requestBody =  _handler.CreateUserRequestBody(name);
            var response = await _handler.POST("users", requestBody);
            
            Assert.AreEqual("Created", response.StatusCode.ToString());
        }     
    }
}