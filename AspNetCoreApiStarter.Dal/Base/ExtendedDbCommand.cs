using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using AspNetCoreApiStarter.Shared.Logger;

namespace AspNetCoreApiStarter.Dal.Base
{
    /// <summary>
    /// Encapsulation de <see cref="DbCommand"/>.
    /// L'encapsulation permet de gérer en plus des exceptions custom, des logs...
    /// </summary>
    public class ExtendedDbCommand : DbCommand
    {
        /// <summary>
        /// La commande.
        /// </summary>
        private DbCommand command;

        /// <summary>
        /// La connection.
        /// </summary>
        private DbConnection connection;

        /// <summary>
        /// Le logger.
        /// </summary>
        private ILoggerHelper logger;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ExtendedDbCommand"/>.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="logger">logger transmis.</param>
        public ExtendedDbCommand(DbCommand command, DbConnection connection, ILoggerHelper logger)
        {
            this.command = command ?? throw new ArgumentNullException("command");
            this.connection = connection ?? throw new ArgumentNullException("connection");
            this.logger = logger ?? throw new ArgumentNullException("logger");
        }

        /// <summary>
        /// Obtient ou définit le text de la commande.
        /// </summary>
        public override string CommandText
        {
            get { return this.command.CommandText; }
            set { this.command.CommandText = value; }
        }

        /// <summary>
        /// Obtient ou définit le timeout de la commande.
        /// </summary>
        public override int CommandTimeout
        {
            get { return this.command.CommandTimeout; }
            set { this.command.CommandTimeout = value; }
        }

        /// <summary>
        /// Obtient ou définit le type de la commande.
        /// </summary>
        public override CommandType CommandType
        {
            get { return this.command.CommandType; }
            set { this.command.CommandType = value; }
        }

        /// <summary>
        /// Obtient ou définit une valeur indiquant si l'objet commande doit être visible.
        /// </summary>
        public override bool DesignTimeVisible
        {
            get { return this.command.DesignTimeVisible; }
            set { this.command.DesignTimeVisible = value; }
        }

        /// <summary>
        /// Obtient ou définit la gestion via <see cref="DbDataAdapter"/>.
        /// </summary>
        public override UpdateRowSource UpdatedRowSource
        {
            get { return this.command.UpdatedRowSource; }
            set { this.command.UpdatedRowSource = value; }
        }

        /// <summary>
        /// Obtient the internal command.
        /// </summary>
        public DbCommand InternalCommand => this.command;

        /// <summary>
        /// Obtient ou définit la connexion.
        /// </summary>
        protected override DbConnection DbConnection
        {
            get { return this.connection; }
            set { this.connection = value; }
        }

        /// <summary>
        /// Obtient la collection des paramètres.
        /// </summary>
        protected override DbParameterCollection DbParameterCollection => this.command.Parameters;

        /// <summary>
        /// Obtient ou définit la transaction.
        /// </summary>
        protected override DbTransaction DbTransaction
        {
            get { return this.command.Transaction; }
            set { this.command.Transaction = value; }
        }

        /// <summary>
        /// Tente d'annuler la commande.
        /// </summary>
        public override void Cancel()
        {
            this.command.Cancel();
        }

        /// <summary>
        /// Execute une requête SQL. <see cref="DbCommand.ExecuteNonQuery"/>.
        /// </summary>
        /// <returns>Le nombre de ligne affectées.</returns>
        public override int ExecuteNonQuery()
        {
            int result;

            try
            {
                this.logger.LogVerbose("Before ExecuteNonQuery");
                result = this.command.ExecuteNonQuery();
                this.logger.LogVerbose("After ExecuteNonQuery");
            }
            catch (SqlException e)
            {
                this.logger.LogException(e);
                ExtendedDbException.ManageException(e);
                throw;
            }

            return result;
        }

        /// <summary>
        /// Execute une requête <see cref="DbCommand.ExecuteScalar"/>.
        /// </summary>
        /// <returns>retourne 1ere colonne de la 1ere ligne.</returns>
        public override object ExecuteScalar()
        {
            object result;

            try
            {
                this.logger.LogVerbose("Before ExecuteScalar");
                result = this.command.ExecuteScalar();
                this.logger.LogVerbose("After ExecuteScalar");
            }
            catch (SqlException e)
            {
                this.logger.LogException(e);
                ExtendedDbException.ManageException(e);
                throw;
            }

            return result;
        }

        /// <summary>
        /// Create une version préparée ou (compilée). <see cref="DbCommand.Prepare"/>.
        /// </summary>
        public override void Prepare()
        {
            this.command.Prepare();
        }

        /// <summary>
        /// Création d'un paramètre. <see cref="DbCommand.CreateDbParameter"/>.
        /// </summary>
        /// <returns>Le paramètre.</returns>
        protected override DbParameter CreateDbParameter()
        {
            return this.command.CreateParameter();
        }

        /// <summary>
        /// Lecture des lignes retournées par la commande SQL. <see cref="DbCommand.ExecuteReader(CommandBehavior)"/>.
        /// </summary>
        /// <param name="behavior">Comportement de la requête et de son effet sur la base.</param>
        /// <returns>A <see cref="DbDataReader"/>.</returns>
        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            DbDataReader result = null;

            try
            {
                this.logger.LogVerbose("Before ExecuteDbDataReader");
                result = this.command.ExecuteReader(behavior);
                this.logger.LogVerbose("After ExecuteDbDataReader");
            }
            catch (SqlException e)
            {
                this.logger.LogException(e);
                ExtendedDbException.ManageException(e);
                throw;
            }

            return result;
        }

        /// <summary>
        /// dispose the command.
        /// </summary>
        /// <param name="disposing">false if this is being disposed in a <c>finalizer</c>.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.command != null)
            {
                this.command.Dispose();
            }

            this.command = null;
            base.Dispose(disposing);
        }
    }
}
