using AspNetCoreApiStarter.Bll.Itf.Dal;
using AspNetCoreApiStarter.Security.Auth;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreApiStarter.Tests.Integration.Helper
{
    public class TestServerHelper
    {
        public IConnectionFactory ConnectionFactory { get; private set; }

        public IJwtFactory JwtFactory { get; private set; }

        public TestServer Server { get; private set; }

        public TestServerHelper InitServer()
        {
            var server = new TestServer(new WebHostBuilder()
                 .UseEnvironment("Development")
                 .UseContentRoot(Directory.GetCurrentDirectory() + @"\..\..\..\..\AspNetCoreApiStarter")
                 .UseStartup<Startup>());

            this.Server = server;
            return this;
        }

        public TestServerHelper InitJwtFactory()
        {
            if (this.Server == null)
            {
                throw new Exception("server must be initialized");
            }

            this.JwtFactory = this.Server.Host.Services.GetService<IJwtFactory>();
            return this;
        }

        public TestServerHelper InitConnectionFactory()
        {
            if (this.Server == null)
            {
                throw new Exception("server must be initialized");
            }

            this.ConnectionFactory = this.Server.Host.Services.GetService<IConnectionFactory>();
            return this;
        }

        public HttpClient CreateClient()
        {
            return this.Server.CreateClient();
        }

        public string GenerateEncodedToken(string userName, ClaimsIdentity identity)
        {
            string jwtToken = this.JwtFactory.GenerateEncodedToken(userName, identity).Result;
            return jwtToken;
        }

        public ClaimsIdentity GenerateClaimsIdentity(int id, string userName, string email, string firstName, string lastName)
        {
            ClaimsIdentity identity = this.JwtFactory.GenerateClaimsIdentity(id, userName, email, firstName, lastName);
            return identity;
        }
    }
}
