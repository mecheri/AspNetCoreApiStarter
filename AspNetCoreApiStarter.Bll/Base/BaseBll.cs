using AspNetCoreApiStarter.Resources;
using AspNetCoreApiStarter.Shared;
using AspNetCoreApiStarter.Shared.Logger;
using Microsoft.Extensions.Localization;

namespace AspNetCoreApiStarter.Bll.Base
{
    /// <summary>
    /// Base Bll
    /// </summary>
    public abstract class BaseBll
    {
        /// <summary>
        /// App global configuration
        /// </summary>
        protected AppConfig AppConfig { get; set; }

        /// <summary>
        /// Database connection string
        /// </summary>
        protected string ConnectionString { get; set; }

        /// <summary>
        /// App resources localizer
        /// </summary>
        protected IStringLocalizer<SharedResources> Localizer { get; set; }

        /// <summary>
        /// App logger helper
        /// </summary>
        protected ILoggerHelper Logger { get; set; }
    }
}