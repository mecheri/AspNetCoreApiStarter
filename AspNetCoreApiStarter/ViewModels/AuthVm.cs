using AspNetCoreApiStarter.Model;
using AspNetCoreApiStarter.ViewModels.Core;
using AspNetCoreApiStarter.ViewModels.Validators;
using Newtonsoft.Json;

namespace AspNetCoreApiStarter.ViewModels
{
    /// <summary>
    /// Authentication view model.
    /// </summary>
    public class AuthVm
    {
        /// <summary>
        /// Gets or sets the username of account.
        /// </summary>
        [JsonProperty("username")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password of account.
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
