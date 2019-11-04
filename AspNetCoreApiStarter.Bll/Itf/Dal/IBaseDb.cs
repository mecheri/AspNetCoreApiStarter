using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace AspNetCoreApiStarter.Bll.Itf.Dal
{
    /// <summary>
    /// Interface de base pour les dao.
    /// </summary>
    public interface IBaseDb
    {
        /// <summary>
        /// Obtient ou définit une connexion à la base de données.
        /// </summary>
        IDbConnection Connection { get; set; }
    }
}