using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AspNetCoreApiStarter.Shared.CustomException
{
    /// <summary>
    /// Exception pour les erreurs arithmétiques.
    /// </summary>
    public class ArithmeticOverflowException : GenericException
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ArithmeticOverflowException" />.
        /// </summary>
        public ArithmeticOverflowException()
            : base()
        {
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ArithmeticOverflowException" />.
        /// </summary>
        /// <param name="message">Message d'erreur de l'exception.</param>
        public ArithmeticOverflowException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ArithmeticOverflowException" />.
        /// <param name="serializationInfo">info de serialisation.</param>
        /// <param name="context">contexte de serialisation.</param>
        /// </summary>
        public ArithmeticOverflowException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ArithmeticOverflowException" />.
        /// </summary>
        /// <param name="message">Message d'erreur de l'exception.</param>
        /// <param name="rootEx">Exception racine.</param>
        public ArithmeticOverflowException(string message, System.Exception rootEx)
            : base(message, rootEx)
        {
        }
    }
}
