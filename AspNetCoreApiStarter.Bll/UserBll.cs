using AspNetCoreApiStarter.Bll.Base;
using AspNetCoreApiStarter.Bll.Itf;
using AspNetCoreApiStarter.Bll.Itf.Bll;
using AspNetCoreApiStarter.Bll.Itf.Dal;
using AspNetCoreApiStarter.Model;
using AspNetCoreApiStarter.Resources;
using AspNetCoreApiStarter.Security;
using AspNetCoreApiStarter.Shared;
using AspNetCoreApiStarter.Shared.Logger;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AspNetCoreApiStarter.Bll
{
    /// <summary>
    /// User Bll
    /// </summary>
    public class UserBll : BaseBll, IUserBll
    {
        private readonly IUserDal userDal;
        private readonly IConnectionFactory connectionFactory;
        private readonly UserCtxResolver userCtxResolver;

        #region Constructor
        /// <summary>
        /// Creates an instance of <see cref="UserBll" />.
        /// </summary>
        /// <param name="connConfig"></param>
        /// <param name="logger"></param>
        /// <param name="localizer"></param>
        public UserBll(
            IUserDal userDal,
            IConnectionFactory connectionFactory,
            UserCtxResolver userCtxResolver,
            IOptions<AppConfig> config,
            ILoggerHelper<UserBll> logger,
            IStringLocalizer<SharedResources> localizer
        ) {
            this.userDal = userDal;
            this.connectionFactory = connectionFactory;
            this.userCtxResolver = userCtxResolver;
            this.AppConfig = config.Value;
            this.Localizer = localizer;
            this.Logger = logger;

            this.userCtxResolver.Check();
        }
        #endregion Constructor

        #region Get
        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>Users list</returns>
        public async Task<IEnumerable<User>> Get()
        {
            using (IDbConnection conn = this.connectionFactory.CreateDefaultConnection())
            {
                conn.Open();
                this.userDal.Connection = conn;
                return await this.userDal.Get();
            }
        }

        /// <summary>
        /// Gets a specific user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The User</returns>
        public async Task<User> Get(int id)
        {
            using (IDbConnection conn = this.connectionFactory.CreateDefaultConnection())
            {
                conn.Open();
                this.userDal.Connection = conn;
                return await this.userDal.Get(id);
            }
        }

        /// <summary>
        /// Gets a specific user
        /// </summary>
        /// <param name="username"></param>
        /// <returns>The User</returns>
        public async Task<User> Get(string username)
        {
            using (IDbConnection conn = this.connectionFactory.CreateDefaultConnection())
            {
                conn.Open();
                this.userDal.Connection = conn;
                return await this.userDal.Get(username);
            }
        }

        /// <summary>
        /// Gets a specific user
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>The User</returns>
        public async Task<User> Check(string username, string password)
        {
            using (IDbConnection conn = this.connectionFactory.CreateDefaultConnection())
            {
                conn.Open();
                this.userDal.Connection = conn;
                return await this.userDal.Check(username, password);
            }
        }
        #endregion Get

        #region Create
        /// <summary>
        /// Créer un utilisateur et retourner l'utilisateur créé.
        /// </summary>
        /// <param name="user">Utilisateur à créer.</param>
        /// <returns>Le nouvel utilisateur.</returns>
        public async Task<User> Create(User user)
        {
            using (IDbConnection conn = this.connectionFactory.CreateDefaultConnection())
            {
                conn.Open();
                this.userDal.Connection = conn;
                int newId = await this.userDal.Create(user);
                return await this.userDal.Get(newId);
            }
        }
        #endregion Create

        #region Update
        /// <summary>
        /// Mettre à jour un utilisateur et retourner l'utilisateur mis à jour.
        /// </summary>
        /// <param name="user">Utilisateur à mettre à jour.</param>
        /// <returns>Utilisateur mis à jour.</returns>
        public async Task<User> Update(User user)
        {
            using (IDbConnection conn = this.connectionFactory.CreateDefaultConnection())
            {
                conn.Open();
                this.userDal.Connection = conn;
                await this.userDal.Update(user);
                return await this.userDal.Get(user.Id);
            }
        }
        #endregion Update

        #region Delete
        /// <summary>
        /// Deletes a specific user
        /// </summary>
        /// <param name="id">User id to delete</param>
        public async Task<bool> Delete(int id)
        {
            using (IDbConnection conn = this.connectionFactory.CreateDefaultConnection())
            {
                conn.Open();
                this.userDal.Connection = conn;
                return await this.userDal.Delete(id);
            }
        }
        #endregion Delete
    }
}