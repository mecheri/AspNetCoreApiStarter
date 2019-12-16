using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreApiStarter.Bll.Itf.Bll;
using AspNetCoreApiStarter.Model;
using AspNetCoreApiStarter.Security.Auth;
using AspNetCoreApiStarter.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AspNetCoreApiStarter.Controllers
{
    /// <summary>
    /// Authtication Controller.
    /// </summary>
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserBll userBll;
        private readonly IJwtFactory jwtFactory;
        private readonly JwtIssuerOptions jwtOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="userBll">User service.</param>
        /// <param name="jwtFactory">Jwt tokens factory.</param>
        /// <param name="jwtOptions">Jwt tokens options.</param>
        public AuthController(IUserBll userBll, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            this.userBll = userBll;
            this.jwtFactory = jwtFactory;
            this.jwtOptions = jwtOptions.Value;

        }

        /// <summary>
        /// Login to the app.
        /// </summary>
        /// <param name="vm">Authentication VM.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. Http 200 if Ok.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]AuthVm vm)
        {
            var identity = await this.GetClaimsIdentity(vm.UserName, vm.Password);
            if (identity == null)
            {
                return this.Unauthorized();
            }

            var jwt = await JwtGenerator.GenerateJwt(identity, this.jwtFactory, vm.UserName, this.jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        /// <summary>
        /// Get claims identity.
        /// </summary>
        /// <param name="username">User Name.</param>
        /// <param name="password">Password.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. Http 200 if Ok.</returns>
        private async Task<ClaimsIdentity> GetClaimsIdentity(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            // get the user to verifty
            User user = await this.userBll.Check(username, password);

            // check the credentials
            if (user == null)
            {
                // Credentials are invalid, or account doesn't exist
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            return await Task.FromResult(this.jwtFactory.GenerateClaimsIdentity(user.Id, user.UserName, user.Email, user.FirstName, user.LastName));
        }
    }
}
