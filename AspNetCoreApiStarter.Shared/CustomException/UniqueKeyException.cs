using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AspNetCoreApiStarter.Shared.CustomException
{
    /// <summary>
    /// Exception pour des contraintes de clé type: unique key ou unique index
    /// </summary>
    public class UniqueKeyException : GenericException
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="UniqueKeyException" />.
        /// </summary>
        public UniqueKeyException()
            : base()
        {
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="UniqueKeyException" />.
        /// </summary>
        /// <param name="message">Message d'erreur de l'exception.</param>
        public UniqueKeyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="UniqueKeyException" />.
        /// <param name="serializationInfo">info de serialisation.</param>
        /// <param name="context">contexte de serialisation.</param>
        /// </summary>
        public UniqueKeyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="UniqueKeyException" />.
        /// </summary>
        /// <param name="message">Message d'erreur de l'exception.</param>
        /// <param name="rootEx">Exception racine.</param>
        public UniqueKeyException(string message, System.Exception rootEx)
            : base(message, rootEx)
        {
        }
    }
}
