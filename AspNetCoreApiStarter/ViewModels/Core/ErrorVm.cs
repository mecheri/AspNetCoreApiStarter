using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AspNetCoreApiStarter.Resources;
using AspNetCoreApiStarter.Shared;
using AspNetCoreApiStarter.Shared.CustomException;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace AspNetCoreApiStarter.ViewModels.Core
{
    /// <summary>
    /// Data transfer class for errors.
    /// </summary>
    public class ErrorVm
    {
        /// <summary>
        /// Enumération des cas d'erreur.
        /// </summary>
        public enum ErrorCode : int
        {
            /// <summary>
            /// Générique erreur (défaut)...
            /// </summary>
            GenericServer = 0,

            /// <summary>
            /// Accès concurrent en base
            /// </summary>
            ConcurrentAccess = 1,

            /// <summary>
            /// Contrainte d'unicité
            /// </summary>
            UniqueKeyConstraint = 2,

            /// <summary>
            /// Contrainte de clé étrangère
            /// </summary>
            ForeignKey = 3,

            /// <summary>
            /// Session timeout
            /// </summary>
            SessionTimeOut = 4,

            /// <summary>
            /// Authentification de l'utilisateur en échec
            /// </summary>
            UnKnownUser = 5,

            /// <summary>
            /// Echec de la validation du message
            /// </summary>
            ValidationFailed = 6,

            /// <summary>
            /// Echec dans les droits d'accès
            /// </summary>
            AccessDenied = 7,

            /// <summary>
            /// Aucune données trouvée dans la base
            /// </summary>
            NotFound = 8,

            /// <summary>
            /// Erreur de dépassement arithmétique lors d'une conversion
            /// </summary>
            ArithmeticOverflow = 9
        }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public ErrorCode Code { get; set; }

        /// <summary>
        /// Gets or sets the friendly message.
        /// </summary>
        [JsonProperty(PropertyName = "message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the original error message.
        /// </summary>
        [JsonProperty(PropertyName = "debugMessage", NullValueHandling = NullValueHandling.Ignore)]
        public string DebugMessage { get; set; }

        /// <summary>
        /// Gets or sets the error stack trace.
        /// </summary>
        [JsonProperty(PropertyName = "stackTrace", NullValueHandling = NullValueHandling.Ignore)]
        public string StackTrace { get; set; }

        /// <summary>
        /// Gets or sets the error stack trace.
        /// </summary>
        [JsonProperty(PropertyName = "validation", NullValueHandling = NullValueHandling.Ignore)]
        public object ValidationDictionnay { get; set; }
    }
}