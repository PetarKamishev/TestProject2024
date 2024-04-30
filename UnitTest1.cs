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
            name = Guid.NewGuid().ToString();
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

        [Test]

        public async Task GetSpecificUser()
        {
            var requestBody = _handler.CreateUserRequestBody(name);
            var request = await _handler.POST("users", requestBody);
            var responseBody = request.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserResponse>(responseBody.Result);
            var response = await _handler.GET("users", user.Id);

            Assert.AreEqual("OK", response.StatusCode.ToString());
            Assert.AreEqual(name, user.Name);
            Assert.IsNotEmpty(user.Id.ToString());
            Assert.IsNotNull(user.Id);
            
        }

        [Test]

        public async Task UpdateUser()
        {
            var requestBody = _handler.CreateUserRequestBody(name);
            var request = await _handler.POST("users", requestBody);
            var responseBody = request.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserResponse>(responseBody.Result);
            var response = await _handler.UPDATE("users", user.Id, requestBody);

            Assert.AreEqual("OK", response.StatusCode.ToString());
            Assert.AreEqual(name, user.Name);
            Assert.IsNotEmpty(user.Id.ToString());
            Assert.IsNotNull(user.Id);
        }

        [Test]

        public async Task DeleteUser()
        {
            var requestBody = _handler.CreateUserRequestBody(name);
            var request = await _handler.POST("users", requestBody);
            var responseBody = request.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserResponse>(responseBody.Result);
            var response = await _handler.DELETE("users", user.Id);

            Assert.AreEqual("NoContent", response.StatusCode.ToString());           
        }
    }
}