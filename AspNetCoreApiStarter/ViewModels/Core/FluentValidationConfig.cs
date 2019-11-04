using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using FluentValidation;
using Newtonsoft.Json;

namespace AspNetCoreApiStarter.ViewModels.Core
{
    /// <summary>
    /// Classe gérant les config générique de la validation Fluent.
    /// </summary>
    public static class FluentValidationConfig
    {
        /// <summary>
        /// Méthode de initialise la configuration générique.
        /// </summary>
        public static void Config()
        {
            string name = null;

            // Permet de récupérer d'afficher les noms définis dans l'attribut jsonproperty des classes
            // dans les messages renvoyé au client
            // Si pas d'attribut passe en camelCase
            ValidatorOptions.PropertyNameResolver = (type, member, n) =>
            {
                if (member != null)
                {
                    // use attribute
                    JsonPropertyAttribute jsonAttribute = (JsonPropertyAttribute)Attribute.GetCustomAttribute(member, typeof(JsonPropertyAttribute));
                    if (jsonAttribute != null)
                    {
                        name = jsonAttribute.PropertyName;
                    }
                    else
                    {
                        // on force member Name to camelCase
                        name = ToCamelCase(member.Name);
                    }
                }

                return name;
            };
        }

        /// <summary>
        /// Transforme une chaine en CamleCase.
        /// </summary>
        /// <param name="s">Chaine à transformer.</param>
        /// <returns>Chaine transformée.</returns>
        private static string ToCamelCase(string s)
        {
            if (string.IsNullOrEmpty(s) || !char.IsUpper(s[0]))
            {
                return s;
            }

            var chars = s.ToCharArray();

            for (var i = 0; i < chars.Length; i++)
            {
                if (i == 1 && !char.IsUpper(chars[i]))
                {
                    break;
                }

                var hasNext = i + 1 < chars.Length;
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                {
                    break;
                }

                chars[i] = char.ToLower(chars[i], CultureInfo.InvariantCulture);
            }

            return new string(chars);
        }
    }
}
