using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AspNetCoreApiStarter.Shared.CustomException
{
    /// <summary>
    /// Applicative Exception of the projet.
    /// </summary>
    [Serializable]
    public class GenericException : Exception
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="GenericException" />.
        /// </summary>
        public GenericException()
            : base()
        {
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="GenericException" />.
        /// </summary>
        /// <param name="message">Message d'erreur de l'exception.</param>
        public GenericException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="GenericException" />.
        /// <param name="serializationInfo">info de serialisation.</param>
        /// <param name="context">contexte de serialisation.</param>
        /// </summary>
        public GenericException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="GenericException" />.
        /// </summary>
        /// <param name="message">Message d'erreur de l'exception.</param>
        /// <param name="rootEx">Exception racine.</param>
        public GenericException(string message, System.Exception rootEx)
            : base(message, rootEx)
        {
        }
    }
}