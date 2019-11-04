using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using AspNetCoreApiStarter.Bll.Itf.Dal;
using AspNetCoreApiStarter.Shared;
using AspNetCoreApiStarter.Shared.Logger;
using Microsoft.Extensions.Options;

namespace AspNetCoreApiStarter.Dal.Base
{
    /// <summary>
    /// Factory des connexions à la base de données.
    /// </summary>
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly ILoggerHelper logger;
        private readonly IOptions<AppConfig> config;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ConnectionFactory"/>.
        /// </summary>
        /// <param name="logger">factory logger injecté.</param>
        public ConnectionFactory(IOptions<AppConfig> config, ILoggerHelper<ConnectionFactory> logger)
        {
            this.logger = logger;
            this.config = config;
        }

        /// <summary>
        /// Obtenir la chaine de connexion principale.
        /// </summary>
        /// <returns>Chaine de connexion.</returns>
        public string GetMainDbConnectionstring()
        {
            return this.config.Value.ConnectionStrings[this.config.Value.ActiveDb];
        }

        /// <summary>
        /// Obtenir la chaine de connexion principale.
        /// </summary>
        /// <returns>Nom du provider.</returns>
        public string GetMainDbProviderName()
        {
            return "System.Data.SqlClient"; // TODO à paramétrer
        }

        /// <summary>
        /// Obtenir la connexion par défaut.
        /// La factory renvoi une <see cref="ExtendedDbConnection"/> pour gestion exception, logs...
        /// </summary>
        /// <returns>Objet connexion.</returns>
        public IDbConnection CreateDefaultConnection()
        {
            DbConnection conn = this.GetConnection(this.GetMainDbConnectionstring(), this.GetMainDbProviderName());

            // la factory renvoi une connexion étendu pour gestion exception, logs ect...
            return new ExtendedDbConnection(conn, this.logger);
        }

        /// <summary>
        /// Permet d'enregister les factorys.
        /// </summary>
        /// <param name="providerName">Nom du provider.</param>
        private static void RegisterFactory(string providerName)
        {
            // dot net core, no gac, no machine config , register manually
            if (providerName == "System.Data.SqlClient")
            {
                DbProviderFactories.RegisterFactory("System.Data.SqlClient", typeof(SqlClientFactory));
            }
            else
            {
                throw new Exception($"{providerName} n'est pas géré");
            }
        }

        /// <summary>
        /// Obtient une connexion depuis la factory.
        /// </summary>
        /// <param name="connectionString">Chaine de connexion.</param>
        /// <param name="providerName">Nom du provider.</param>
        /// <returns>Instance de la connexion.</returns>
        private DbConnection GetConnection(string connectionString, string providerName)
        {
            DbConnection connexion = null;
            DbProviderFactory dbFactory = null;

            // DbProviderFactories n'existe pas en .Net Standard 2.0
            bool ok = DbProviderFactories.TryGetFactory(providerName, out dbFactory);

            if (!ok)
            {
                RegisterFactory(providerName);
                bool retryOk = DbProviderFactories.TryGetFactory(providerName, out dbFactory);

                if (!retryOk)
                {
                    throw new Exception($"Impossible d'obtenir la factory à partir du provider {providerName}");
                }
            }

            // Recovery of the connection in the factory
            connexion = dbFactory.CreateConnection();
            connexion.ConnectionString = connectionString;

            // It returns the connection instance
            return connexion;
        }
    }
}
