using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AspNetCoreApiStarter.Shared.CustomException
{
    /// <summary>
    /// Exception pour les authorisations
    /// </summary>
    public class AuthorizationException : GenericException
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="AuthorizationException" />.
        /// </summary>
        public AuthorizationException()
            : base()
        {
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="AuthorizationException" />.
        /// </summary>
        /// <param name="message">Message d'erreur de l'exception.</param>
        public AuthorizationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="AuthorizationException" />.
        /// <param name="serializationInfo">info de serialisation.</param>
        /// <param name="context">contexte de serialisation.</param>
        /// </summary>
        public AuthorizationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="AuthorizationException" />.
        /// </summary>
        /// <param name="message">Message d'erreur de l'exception.</param>
        /// <param name="rootEx">Exception racine.</param>
        public AuthorizationException(string message, System.Exception rootEx)
            : base(message, rootEx)
        {
        }
    }
}
