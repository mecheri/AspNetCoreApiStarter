using AspNetCoreApiStarter.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApiStarter.Bll.Itf.Bll
{
    /// <summary>
    /// User Bll Interface
    /// </summary>
    public interface IUserBll
    {
        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>Users list</returns>
        Task<IEnumerable<User>> Get();

        /// <summary>
        /// Gets a specific user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The User</returns>
        Task<User> Get(int id);

        /// <summary>
        /// Gets a specific user
        /// </summary>
        /// <param name="email"></param>
        /// <returns>The User</returns>
        Task<User> Get(string email);

        /// <summary>
        /// Gets a specific user
        /// </summary>
        /// <param name="username">User Name</param>
        /// <param name="password">Password</param>
        /// <returns>The User</returns>
        Task<User> Check(string username, string password);

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="user">User to create</param>
        /// <returns>Created user</returns>
        Task<User> Create(User user);

        /// <summary>
        /// Updates a specific user
        /// </summary>
        /// <param name="user">User to update</param>
        /// <returns>Updated user</returns>
        Task<User> Update(User user);

        /// <summary>
        /// Deletes a specific user
        /// </summary>
        /// <param name="id">User id to delete</param>
        Task<bool> Delete(int id);
    }
}