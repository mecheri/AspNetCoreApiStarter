using AspNetCoreApiStarter.Resources;
using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AspNetCoreApiStarter.Security.Auth
{
    /// <summary>
    /// JWT Factory class
    /// </summary>
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtOptions;

        /// <summary>
        /// Creates an instance of JwtFactory.
        /// </summary>
        /// <param name="jwtOptions"></param>
        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
        }

        /// <summary>
        /// Generate encoded token
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="identity">Claims identity</param>
        /// <returns></returns>
        public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
        {
            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, userName),
                 new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                 new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                 identity.FindFirst(Constants.JwtClaimIdentifiers.Rol),
                 identity.FindFirst(Constants.JwtClaimIdentifiers.Id)
             };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        /// <summary>
        /// Generate claim identity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ClaimsIdentity GenerateClaimsIdentity(int id, string userName, string email, string firstName, string lastName)
        {
            var claimsIdentity = new ClaimsIdentity(new GenericIdentity(userName, "Token"));
            var claims = new[]
            {
                // id user
                new Claim(Constants.JwtClaimIdentifiers.Id, id.ToString()),

                // email user
                new Claim(Constants.JwtClaimIdentifiers.Email, email),

                // first name user
                new Claim(Constants.JwtClaimIdentifiers.FirstName, firstName),

                // last name user
                new Claim(Constants.JwtClaimIdentifiers.LastName, lastName),

                // optionnel : permettra de tester si user possède droit accès à l'api (via une policy sur un middleware par ex)
                // ici comme l'appli fournit et controle le token ce n'est pas requis
                new Claim(Constants.JwtClaimIdentifiers.Rol, Constants.JwtClaims.ApiAccess)
            };

            claimsIdentity.AddClaims(claims);

            return claimsIdentity;
        }

        /// <summary>
        /// Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        /// <summary>
        /// Throw If Invalid Options
        /// </summary>
        /// <param name="options"></param>
        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }
    }
}