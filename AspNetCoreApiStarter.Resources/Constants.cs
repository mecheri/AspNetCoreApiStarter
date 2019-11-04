namespace AspNetCoreApiStarter.Resources
{
    /// <summary>
    /// Classe regroupant les constantes les ressources
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// JwtClaimIdentifiers Class
        /// </summary>
        public static class JwtClaimIdentifiers
        {
            /// <summary>
            /// Rol and id strings.
            /// </summary>
            public const string Id = "id";

            /// <summary>
            /// Rol and id strings
            /// </summary>
            public const string Rol = "rol";

            /// <summary>
            /// Rol and id strings.
            /// </summary>
            public const string Email = "em";

            /// <summary>
            /// First Name.
            /// </summary>
            public const string FirstName = "fname";

            /// <summary>
            /// Last Name.
            /// </summary>
            public const string LastName = "lname";
        }

        /// <summary>
        /// JwtClaims Class
        /// </summary>
        public static class JwtClaims
        {
            /// <summary>
            /// Api access string
            /// </summary>
            public const string ApiAccess = "api_access";
        }

        public static class Cors
        {
            /// <summary>
            /// Cors policy name
            /// </summary>
            public const string PolicyName = "CorsPolicy";

        }
        /// <summary>
        /// Classe regroupant les erreurs
        /// </summary>
        public static class Errors
        {
            /// <summary>
            /// BadRequest
            /// </summary>
            public const string BadRequest = nameof(BadRequest);

            /// <summary>
            /// InternalServerError
            /// </summary>
            public const string InternalServerError = nameof(InternalServerError);

            /// <summary>
            /// NotFound
            /// </summary>
            public const string NotFound = nameof(NotFound);

            /// <summary>
            /// Unauthorized
            /// </summary>
            public const string Unauthorized = nameof(Unauthorized);
        }
    }
}
