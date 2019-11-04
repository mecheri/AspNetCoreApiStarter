using AspNetCoreApiStarter.Shared;
using AspNetCoreApiStarter.Shared.CustomException;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreApiStarter.Dal.Base
{
    /// <summary>
    /// Etend les méthodes d'extension de dapper.
    /// </summary>
    public static class ExtendedSqlMapper
    {
        /// <summary>
        /// Execute a command asynchronously using .NET 4.5 Task and check row count for concurrency.
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static async Task<int> ExecuteOptmisticAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            int rowCount = await cnn.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);

            if (rowCount <= 0)
            {
                throw new ConcurrentAccessException();
            }

            return rowCount;
        }
    }
}
