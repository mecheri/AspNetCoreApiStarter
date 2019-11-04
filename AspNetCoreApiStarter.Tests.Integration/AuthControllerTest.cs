using AspNetCoreApiStarter.Tests.Integration.Helper;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreApiStarter.Tests.Integration
{
    /// <summary>
    /// Test d'intégration controller auth.
    /// Comme il s'agit de test d'intégration (et non des tests unitaires), on effectue les tests à partir d'un état donnée et dans un ordre déterminé.
    /// Test auth & user dans la même collection permet d'éviter une exécution en parallele.
    /// </summary>
    [Collection("TestAuth&User")]
    public class AuthControllerTest : IClassFixture<AuthControllerTestFixture>
    {
        private readonly AuthControllerTestFixture fixture;

        public AuthControllerTest(AuthControllerTestFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [InlineData("POST", "/api/auth/login", "ndelaval", "nicolas")]
        public async Task GetAuthToken_Ok(string verb, string url, string userName, string pwd)
        {
            var json = $@"{{ ""username"" : ""{userName}"", ""password"":""{pwd}""}}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(new HttpMethod(verb), url) { Content = content };
            var response = await this.fixture.HttpClient.SendAsync(request);
            
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("POST", "/api/auth/login", "ndelaval", "xxxxx")]
        public async Task GetAuthToken_Unauthorized(string verb, string url, string userName, string pwd)
        {
            var json = $@"{{ ""username"" : ""{userName}"", ""password"":""{pwd}""}}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(new HttpMethod(verb), url) { Content = content };
            var response = await this.fixture.HttpClient.SendAsync(request);

            
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }

    /// <summary>
    /// Fixture pour gérer un état partagé entre les tests d'une même classe.
    /// cf : https://xunit.github.io/docs/shared-context#class-fixture
    /// when you want to create a single test context and share it among all the tests in the class, 
    /// and have it cleaned up after all the tests in the class have finished
    /// </summary>
    public class AuthControllerTestFixture : IDisposable
    {
        // mock
        public Model.User MockAdmin { get; set; } = new Model.User()
        {
            UserName = "ndelaval",
            Password = "nicolas",
            Email = "nicolas@viveris.fr",
            FirstName = "nicolas",
            LastName = "delaval"
        };

        // technique
        public HttpClient HttpClient { get; private set; }

        public TestServerHelper ServerHelper { get; private set; }

        public AuthControllerTestFixture()
        {
            // permet d'initialiser 
            // - un server web pour le test à partir du fichier startup de l'appli
            // - un accès à la token factory pour mocker un token
            // - un accès à la connection factory pour accéder si besoin directement à la base
            this.ServerHelper = new TestServerHelper()
            .InitServer()
            .InitConnectionFactory();

            // prepare client http
            this.HttpClient = this.ServerHelper.CreateClient();

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
