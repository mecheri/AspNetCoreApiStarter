using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AspNetCoreApiStarter.Shared.CustomException
{
    /// <summary>
    /// Exception pour des contraintes de clé étrangères
    /// </summary>
    public class ConcurrentAccessException : GenericException
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ConcurrentAccessException" />.
        /// </summary>
        public ConcurrentAccessException()
            : base()
        {
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ConcurrentAccessException" />.
        /// </summary>
        /// <param name="message">Message d'erreur de l'exception.</param>
        public ConcurrentAccessException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ConcurrentAccessException" />.
        /// <param name="serializationInfo">info de serialisation.</param>
        /// <param name="context">contexte de serialisation.</param>
        /// </summary>
        public ConcurrentAccessException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ArithmeticOverflowException" />.
        /// </summary>
        /// <param name="message">Message d'erreur de l'exception.</param>
        /// <param name="rootEx">Exception racine.</param>
        public ConcurrentAccessException(string message, System.Exception rootEx)
            : base(message, rootEx)
        {
        }
    }
}
