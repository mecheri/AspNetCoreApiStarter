using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreApiStarter.Security.Auth
{
    /// <summary>
    /// JWT factory interface
    /// </summary>
    public interface IJwtFactory
    {
        /// <summary>
        /// Generate encoded token
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="identity">Claims identity</param>
        /// <returns></returns>
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);

        /// <summary>
        /// Generate claims identity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        ClaimsIdentity GenerateClaimsIdentity(int id, string userName, string email, string firstName, string lastName);
    }
}