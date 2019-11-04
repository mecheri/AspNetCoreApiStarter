using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace AspNetCoreApiStarter.Bll.Itf.Dal
{
    /// <summary>
    /// Interface factory de création de connexion à la base.
    /// </summary>
    public interface IConnectionFactory
    {
        /// <summary>
        /// Obtenir la connexion par défaut.
        /// </summary>
        /// <returns>Objet connexion.</returns>
        IDbConnection CreateDefaultConnection();
    }
}
