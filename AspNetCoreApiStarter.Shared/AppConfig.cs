using System.Collections.Generic;

namespace AspNetCoreApiStarter.Shared
{
    /// <summary>
    /// AppConfig.
    /// </summary>
    public class AppConfig
    {
        public string AppName { get; set; }

        public string ActiveDb { get; set; }

        public string AllowedOrigins { get; set; }

        /// <summary>
        /// ConnectionStrings
        /// </summary>
        public Dictionary<string, string> ConnectionStrings { get; set; }

        /// <summary>
        /// Swagger
        /// </summary>
        public Swagger Swagger { get; set; }
    }

    /// <summary>
    /// Swagger.
    /// </summary>
    public class Swagger
    {
        /// <summary>
        /// Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// VirtualDirectory.
        /// </summary>
        public string VirtualDirectory { get; set; }

        /// <summary>
        /// ContactName.
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// ContactEmail.
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        /// ContactUrl.
        /// </summary>
        public string ContactUrl { get; set; }
    }
}
