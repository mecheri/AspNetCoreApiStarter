using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using AspNetCoreApiStarter.Shared.Logger;

namespace AspNetCoreApiStarter.Dal.Base
{
    /// <summary>
    /// Encapsulation de la connexion à la base pour étendre sa gestion.
    /// </summary>
    public class ExtendedDbConnection : DbConnection
    {
        private readonly ILoggerHelper logger;
        private DbConnection connection;

        /// <summary>
        /// Obtient ou définit la chaine de connexion.
        /// </summary>
        public override string ConnectionString
        {
            get { return this.connection.ConnectionString; }
            set { this.connection.ConnectionString = value; }
        }

        /// <summary>
        /// Obtient la database.
        /// </summary>
        public override string Database => this.connection.Database;

        /// <summary>
        /// Obtient le nom du serveur de base de donnée.
        /// </summary>
        public override string DataSource => this.connection.DataSource;

        /// <summary>
        /// Obtient la version du serveur.
        /// </summary>
        public override string ServerVersion => this.connection.ServerVersion;

        /// <summary>
        /// Obtient l'état du serveur.
        /// </summary>
        public override ConnectionState State => this.connection.State;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ExtendedDbConnection"/>.
        /// </summary>
        /// <param name="connection">Connexion source.</param>
        /// <param name="logger">logger transmis.</param>
        public ExtendedDbConnection(DbConnection connection, ILoggerHelper logger)
        {
            this.connection = connection ?? throw new ArgumentNullException("connection");
            this.logger = logger ?? throw new ArgumentNullException("logger");
        }

        /// <summary>
        /// Obtient la connection source.
        /// </summary>
        public DbConnection WrappedConnection
        {
            get { return this.connection; }
        }

        /// <summary>
        /// Changer de base de données.
        /// </summary>
        /// <param name="databaseName">Nom de la base cible.</param>
        public override void ChangeDatabase(string databaseName)
        {
            this.connection.ChangeDatabase(databaseName);
        }

        /// <summary>
        /// Fermer la connexion.
        /// </summary>
        public override void Close()
        {
            this.logger.LogVerbose("Closing connexion");
            this.connection.Close();
            this.logger.LogVerbose("Closed connexion");
        }

        /// <summary>
        /// Ouvrir la connexion.
        /// </summary>
        public override void Open()
        {
            this.logger.LogVerbose("Opening connexion");
            this.connection.Open();
            this.logger.LogVerbose("Opened connexion");
        }

        /// <summary>
        /// Démarrer une transaction.
        /// </summary>
        /// <param name="isolationLevel">Niveau d'isolation.</param>
        /// <returns><see cref="DbTransaction"/>.</returns>
        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            return this.connection.BeginTransaction(isolationLevel);
        }

        /// <summary>
        /// Créer un objet commande.
        /// </summary>
        /// <returns><see cref="DbCommand"/>.</returns>
        protected override DbCommand CreateDbCommand()
        {
            return new ExtendedDbCommand(this.connection.CreateCommand(), this, this.logger);
        }

        /// <summary>
        /// dispose the underlying connection.
        /// </summary>
        /// <param name="disposing">false if pre-empted from a <c>finalizer</c>.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.connection != null)
            {
                this.connection.Dispose();
            }

            this.connection = null;
            base.Dispose(disposing);

            this.logger.LogVerbose("Disposed connexion");
        }
    }
}
