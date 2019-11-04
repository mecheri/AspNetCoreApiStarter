using AspNetCoreApiStarter.Shared;
using AspNetCoreApiStarter.Shared.CustomException;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AspNetCoreApiStarter.Dal.Base
{
    /// <summary>
    /// Gestion des exceptions.
    /// </summary>
    public class ExtendedDbException
    {
        /// <summary>
        /// Définit le délimiteur utilisé dans les messages d'erreur levés par les procédures stockées.
        /// Le délimiteur est le caractère juste après le code erreur.
        /// </summary>
        private const string DelimiterInSqlException = "-";

        /// <summary>
        /// Gestion des exceptions customs en testant les codes erreurs retournés.
        /// </summary>
        /// <param name="ex">Exception à tester.</param>
        public static void ManageException(SqlException ex)
        {
            int errorNumber = ex.Number;

            // Managing error code sent from procedure with the try catch
            // If an error occured in the procedure, a message is sent with all the informations
            // (the error code is at the beginning of the message)
            if (ex.Number == 50000)
            {
                errorNumber = GetNumberException(ex.Message);
            }

            // throw custom exception
            ManageExceptionByErrorNumber(errorNumber, ex);
        }

        /// <summary>
        /// Lance des exceptions spécifiques en fonction du numéro d'erreur.
        /// </summary>
        /// <param name="errorNumber"> Numéro d'erreur. </param>
        /// <param name="ex"> Object Exception. </param>
        private static void ManageExceptionByErrorNumber(int errorNumber, Exception ex)
        {
            switch (errorNumber)
            {
                //// Error code for "Violation of UNIQUE KEY constraint"
                case 2627:
                    throw new UniqueKeyException(ex.Message, ex);

                //// Error code for "Cannot insert duplicate key row with UNIQUE INDEX"
                case 2601:
                    throw new UniqueKeyException(ex.Message, ex);

                //// Error code for "Conflict with the FOREIGN KEY constraint"
                case 547:
                    throw new ForeignKeyException(ex.Message, ex);

                //// Error code for "Arithmetic overflow error converting expression to data type numeric"
                case 8115:
                    throw new ArithmeticOverflowException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Retourne le code erreur de l'exception sql dans le cas d'un raisse error dune procédure stockée..
        /// </summary>
        /// <param name="messageException">Message renvoyé par le raise error de la procédure stockée.</param>
        /// <returns>Numéro d'erreur.</returns>
        private static int GetNumberException(string messageException)
        {
            int errorCode = -1;
            int firstIndexOfDelimiter = messageException.IndexOf(DelimiterInSqlException);

            errorCode = int.Parse(messageException.Substring(0, firstIndexOfDelimiter));

            return errorCode;
        }
    }
}
