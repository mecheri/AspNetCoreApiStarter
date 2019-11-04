using AspNetCoreApiStarter.Shared;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreApiStarter.Security.Auth
{
    /// <summary>
    /// Tokens Class
    /// </summary>
    public class JwtGenerator
    {
        /// <summary>
        /// Generate JWT Token
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="jwtFactory"></param>
        /// <param name="userName"></param>
        /// <param name="jwtOptions"></param>
        /// <param name="serializerSettings"></param>
        /// <returns></returns>
        public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                username = userName,
                id = identity.Claims.Single(c => c.Type == "id").Value,
                token = await jwtFactory.GenerateEncodedToken(userName, identity),
                duration = (int)jwtOptions.ValidFor.TotalSeconds
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
    }
}
