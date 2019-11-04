using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreApiStarter.Model;
using AspNetCoreApiStarter.ViewModels.Core;
using AspNetCoreApiStarter.ViewModels.Validators;
using Newtonsoft.Json;

namespace AspNetCoreApiStarter.ViewModels
{
    /// <summary>
    /// Defines user view model.
    /// </summary>
    public class UserVm : BaseVm
    {
        /// <summary>
        /// Gets or sets the email Address of account.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user name of account.
        /// </summary>
        [JsonProperty("username")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the email Address of account.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Password of account.
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the FirstName of account.
        /// </summary>
        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the LastName of account.
        /// </summary>
        [JsonProperty("lastname")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets timesatmap as ulong.
        /// </summary>
        [JsonProperty("ts")]
        public ulong Ts { get; set; }

        #region Load

        /// <summary>
        /// From model to DTO.
        /// </summary>
        /// <param name="user">User model.</param>
        /// <returns>User DTO.</returns>
        public static UserVm Load(User user)
        {
            return new UserVm
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Ts = user.Ts
            };
        }
        #endregion

        #region Get

        /// <summary>
        /// From vm to business object.
        /// </summary>
        /// <param name="vm">User vm.</param>
        /// <returns>User business object.</returns>
        public static User Get(UserVm vm)
        {
            return new User
            {
                Id = vm.Id,
                UserName = vm.UserName,
                Email = vm.Email,
                Password = vm.Password,
                LastName = vm.LastName,
                FirstName = vm.FirstName,
                Ts = (TimeStamp)vm.Ts
            };
        }
        #endregion
    }
}
