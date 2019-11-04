using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AspNetCoreApiStarter.Shared.CustomException
{
    public class EntityNotFoundException : GenericException
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="EntityNotFoundException" />.
        /// </summary>
        public EntityNotFoundException()
            : base()
        {
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="EntityNotFoundException" />.
        /// </summary>
        /// <param name="message">Message d'erreur de l'exception.</param>
        public EntityNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="EntityNotFoundException" />.
        /// <param name="serializationInfo">info de serialisation.</param>
        /// <param name="context">contexte de serialisation.</param>
        /// </summary>
        public EntityNotFoundException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="EntityNotFoundException" />.
        /// </summary>
        /// <param name="message">Message d'erreur de l'exception.</param>
        /// <param name="rootEx">Exception racine.</param>
        public EntityNotFoundException(string message, System.Exception rootEx)
            : base(message, rootEx)
        {
        }
    }
}
