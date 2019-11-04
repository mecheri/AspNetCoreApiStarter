using AspNetCoreApiStarter.Bll.Itf.Dal;
using AspNetCoreApiStarter.Dal.Base;
using AspNetCoreApiStarter.Model;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApiStarter.Dal
{
    public class UserDal : BaseDal, IUserDal
    {
        public UserDal()
        {
        }

        public async Task<IEnumerable<User>> Get() => await Connection.QueryAsync<User>("SELECT * FROM Users");

        public async Task<User> Get(int id)
        {
            string sQuery = @"SELECT * FROM Users WHERE Id = @Id";
            return await Connection.QueryFirstOrDefaultAsync<User>(sQuery, new { Id = id });
        }

        public async Task<User> Get(string username)
        {
            string sQuery = @"SELECT * FROM Users WHERE UserName = @UserName";
            return await Connection.QueryFirstOrDefaultAsync<User>(sQuery, new { UserName = username });
        }

        public async Task<User> Check(string username, string password)
        {
            string sQuery = @"SELECT * FROM Users 
                            WHERE UserName = @UserName 
                            AND Password = @Password";
            return await Connection.QueryFirstOrDefaultAsync<User>(sQuery, new { UserName = username, Password = password });
        }

        public async Task<int> Create(User user)
        {
            string insertQuery = @"INSERT INTO Users (UserName, Email, Password, FirstName, LastName) 
                                VALUES(@UserName, @Email, @Password, @FirstName, @LastName);
                                SELECT CAST(SCOPE_IDENTITY() as int)";
            return await Connection.ExecuteScalarAsync<int>(insertQuery, user);
        }

        public async Task Update(User user)
        {
            string updateQuery = @"UPDATE Users SET 
                           Username = @Username,
                           Password = @Password,
                           Email = @Email,
                           FirstName = @FirstName,
                           LastName = @LastName
                           WHERE Id = @Id and Ts = @Ts";
            await Connection.ExecuteOptmisticAsync(updateQuery, user); // check concurrency avec ts
        }

        public async Task<bool> Delete(int id)
        {
            string delQuery = @"DELETE FROM Users WHERE Id = @Id";
            int nb = await Connection.ExecuteAsync(delQuery, new { Id = id });
            return nb > 0;
        }
    }
}
