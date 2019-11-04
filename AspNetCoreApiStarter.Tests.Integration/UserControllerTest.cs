using AspNetCoreApiStarter.Dal;
using AspNetCoreApiStarter.Tests.Integration.Helper;
using AspNetCoreApiStarter.Tests.Integration.Helper.Xunit;
using AspNetCoreApiStarter.Shared;
using AspNetCoreApiStarter.ViewModels;
using AspNetCoreApiStarter.ViewModels.Core;
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static AspNetCoreApiStarter.ViewModels.Core.ErrorVm;

namespace AspNetCoreApiStarter.Tests.Integration
{
    /// <summary>
    /// Test d'intégration controller user.
    /// Comme il s'agit de test d'intégration (et non des tests unitaires), on effectue les tests à partir d'un état donnée et dans un ordre déterminé.
    /// Test auth & user dans la même collection permet d'éviter une exécution en parallele.
    /// </summary>
    [Collection("TestAuth&User")]
    [TestCaseOrderer("AspNetCoreApiStarter.IntegrationTest.Helper.Xunit.PriorityOrderer", "AspNetCoreApiStarter.IntegrationTest")]
    public class UserControllerTest : IClassFixture<UserControllerFixture>
    {
        private readonly UserControllerFixture fixture;

        public UserControllerTest(UserControllerFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [Priority(10)]
        [InlineData("GET", "/api/users")]
        public async Task GetUsers_Ok(string verb, string url)
        {
            var request = new HttpRequestMessage(new HttpMethod(verb), url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.fixture.JwtToken);
            var response = await this.fixture.HttpClient.SendAsync(request);

            // response.EnsureSuccessStatusCode();
            var json = response.Content.ReadAsStringAsync().Result;
            List<UserVm> listVm = JsonConvert.DeserializeObject<List<UserVm>>(json);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode); // http 200
            Assert.Single(listVm); // 1 user dans la liste
        }

        [Theory]
        [Priority(20)]
        [InlineData("GET", "/api/users/{0}")]
        public async Task GetUser_Ok(string verb, string url)
        {
            var request = new HttpRequestMessage(new HttpMethod(verb), string.Format(url, this.fixture.MockAdmin.Id));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.fixture.JwtToken);
            var response = await this.fixture.HttpClient.SendAsync(request);

            // response.EnsureSuccessStatusCode();
            var json = response.Content.ReadAsStringAsync().Result;

            UserVm vm = JsonConvert.DeserializeObject<UserVm>(json);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode); // http 200
            Assert.Equal(this.fixture.MockAdmin.Id, vm.Id); // id reçu est bien celui demandé
            Assert.NotEqual(vm.Ts, (ulong)0); // timestamp doit être renseigné
        }

        [Theory]
        [Priority(30)]
        [InlineData("POST", "/api/users")]
        public async Task CreateUser_Ok(string verb, string url)
        {
            string jsonIn = JsonConvert.SerializeObject(this.fixture.MockUser);

            var request = new HttpRequestMessage(new HttpMethod(verb), url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.fixture.JwtToken);
            request.Content = new StringContent(jsonIn, Encoding.UTF8, "application/json");

            var response = await this.fixture.HttpClient.SendAsync(request);

            var jsonOut = response.Content.ReadAsStringAsync().Result;
            UserVm vm = JsonConvert.DeserializeObject<UserVm>(jsonOut);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode); // http 201
            Assert.NotEqual(0, vm.Id); // user has Id

            // on conserve l'id & ts
            this.fixture.MockUser.Id = vm.Id;
            this.fixture.MockUser.Ts = vm.Ts;
        }

        [Theory]
        [Priority(35)]
        [InlineData("POST", "/api/users")]
        public async Task CreateUser_ValidationFailed(string verb, string url)
        {
            var emptyMock = new UserVm() { };
            string jsonIn = JsonConvert.SerializeObject(emptyMock); 

            var request = new HttpRequestMessage(new HttpMethod(verb), url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.fixture.JwtToken);
            request.Content = new StringContent(jsonIn, Encoding.UTF8, "application/json");

            var response = await this.fixture.HttpClient.SendAsync(request);

            var jsonOut = response.Content.ReadAsStringAsync().Result;
            ErrorVm vm = JsonConvert.DeserializeObject<ErrorVm>(jsonOut);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode); // http 400
            Assert.Equal(ErrorCode.ValidationFailed, vm.Code); // validation failed
        }

        [Theory]
        [Priority(40)]
        [InlineData("PUT", "/api/users/{0}")]
        public async Task UpdateUser_Ok(string verb, string url)
        {
            this.fixture.MockUser.UserName = "mockuserUpdated";
            string jsonIn = JsonConvert.SerializeObject(this.fixture.MockUser);

            var request = new HttpRequestMessage(new HttpMethod(verb), string.Format(url, this.fixture.MockUser.Id));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.fixture.JwtToken);
            request.Content = new StringContent(jsonIn, Encoding.UTF8, "application/json");

            var response = await this.fixture.HttpClient.SendAsync(request);

            var jsonOut = response.Content.ReadAsStringAsync().Result;
            UserVm vm = JsonConvert.DeserializeObject<UserVm>(jsonOut);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode); // http 200
            Assert.NotEqual(this.fixture.MockUser.Ts, vm.Ts); // check ts changed
        }

        [Theory]
        [Priority(50)]
        [InlineData("PUT", "/api/users/{0}")]
        public async Task UpdateUser_Concurrency_Ko(string verb, string url)
        {
            this.fixture.MockUser.Ts = 1234; // mock wrong ts
            string jsonIn = JsonConvert.SerializeObject(this.fixture.MockUser);

            var request = new HttpRequestMessage(new HttpMethod(verb), string.Format(url, this.fixture.MockUser.Id));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.fixture.JwtToken);
            request.Content = new StringContent(jsonIn, Encoding.UTF8, "application/json");

            var response = await this.fixture.HttpClient.SendAsync(request);

            var jsonOut = response.Content.ReadAsStringAsync().Result;
            ErrorVm vm = JsonConvert.DeserializeObject<ErrorVm>(jsonOut);

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode); // http 500
            Assert.Equal(ErrorCode.ConcurrentAccess, vm.Code); // code spécific accès concurrent
        }

        // TODO add test concurrency failed

        [Theory]
        [Priority(60)]
        [InlineData("DELETE", "/api/users/{0}")]
        public async Task DeleteUser_Ok(string verb, string url)
        {
            var request = new HttpRequestMessage(new HttpMethod(verb), string.Format(url, this.fixture.MockUser.Id));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.fixture.JwtToken);

            var response = await this.fixture.HttpClient.SendAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode); // http 200
        }

        // TODO check custome xception.

        // TODO check validation error.
    }

    /// <summary>
    /// Fixture pour gérer un état partagé entre les tests d'une même classe.
    /// cf : https://xunit.github.io/docs/shared-context#class-fixture
    /// when you want to create a single test context and share it among all the tests in the class, 
    /// and have it cleaned up after all the tests in the class have finished
    /// </summary>
    public class UserControllerFixture : IDisposable
    {
        // mock
        public UserVm MockAdmin { get; set; } = new UserVm()
        {
            UserName = "ndelaval",
            Password = "nicolas",
            Email = "nicolas@viveris.fr",
            FirstName = "nicolas",
            LastName = "delaval"
        };

        public UserVm MockUser { get; set; } = new UserVm()
        {
            UserName = "mockuser",
            Email = "mock@mock.com",
            Password = "password",
            FirstName = "firstName",
            LastName = "lastName"
        };

        // technique
        public HttpClient HttpClient { get; private set; }
        public string JwtToken { get; private set; }
        public TestServerHelper ServerHelper { get; private set; }

        public UserControllerFixture()
        {
            // permet d'initialiser 
            // - un server web pour le test à partir du fichier startup de l'appli
            // - un accès à la token factory pour mocker un token
            // - un accès à la connection factory pour accéder si besoin directement à la base
            this.ServerHelper = new TestServerHelper()
            .InitServer()
            .InitJwtFactory()
            .InitConnectionFactory();

            // prepare client http
            this.HttpClient = this.ServerHelper.CreateClient();

            // prepare jwttoken
            var id = this.ServerHelper.GenerateClaimsIdentity(1, "nicolas", "nicolas@viveris.fr", "nicolas", "delaval");
            this.JwtToken = this.ServerHelper.GenerateEncodedToken("nicolas", id);

            // prepare db
            // purge & création d'un user (pour permettre l'accès à l'api un user doit exister, pour gestion du token auth)
            // on init direct via sql
            IDbConnection connection = this.ServerHelper.ConnectionFactory.CreateDefaultConnection();
            string insertQuery = @"TRUNCATE TABLE Users; INSERT INTO Users (UserName, Email, Password, FirstName, LastName) 
                                VALUES(@UserName, @Email, @Password, @FirstName, @LastName);
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            this.MockAdmin.Id = connection.ExecuteScalar<int>(insertQuery, this.MockAdmin);
        }

        public void Dispose()
        {
            // ... clean up test
        }
    }
}
