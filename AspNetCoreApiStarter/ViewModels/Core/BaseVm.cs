using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace AspNetCoreApiStarter.ViewModels.Core
{
    /// <summary>
    /// Classe de base des vues modèles.
    /// </summary>
    public abstract class BaseVm
    {
        /////// <summary>
        /////// Gets or sets la liste des erreurs de validation.
        /////// </summary>
        ////[JsonIgnore]
        ////protected ValidationResult ValidationResult { get; set; }

        /////// <summary>
        /////// Gets des erreurs de validation.
        /////// </summary>
        ////[JsonIgnore]
        ////protected IList<ValidationFailure> Errors
        ////{
        ////    get
        ////    {
        ////        return this.ValidationResult.Errors;
        ////    }
        ////}

        /////// <summary>
        /////// Méthode de validation à implémenter.
        /////// </summary>
        ////// public abstract void Validate();

        /////// <summary>
        /////// Méthode qui permet de savoir si le modèle est valide.
        /////// </summary>
        /////// <returns>
        /////// Booléen indiquant la validité.
        /////// </returns>
        ////public bool IsValid()
        ////{
        ////    return this.ValidationResult == null || this.ValidationResult.IsValid;
        ////}

        /////// <summary>
        /////// Chargement d'un dictionnaire simple pour renvoyer les informations de validation serveur
        /////// "property => [ "error1", "error2", ... "errorN" ]".
        /////// </summary>
        /////// <returns>Dictionnaire des erreurs de validation.</returns>
        ////public Dictionary<string, string[]> ProcessVmErrors()
        ////{
        ////    return this.Errors
        ////    .GroupBy(i => char.ToLowerInvariant(i.PropertyName[0]) + i.PropertyName.Substring(1), i => i.ErrorMessage)
        ////    .ToDictionary(e => e.Key, e => e.ToArray());
        ////}

        /////// <summary>
        /////// Chargement d'un dictionnaire simple pour renvoyer les informations de validation serveur
        /////// "property => [ "error1", "error2", ... "errorN" ]".
        /////// </summary>
        /////// <returns>Dictionnaire des erreurs de validation.</returns>
        ////public List<ValidationFailure> ProcessVmListErrors()
        ////{
        ////    return this.Errors as List<ValidationFailure>;
        ////}

        /////// <summary>
        /////// Chargement d'un dictionnaire complexe pour renvoyer les informations de validation serveur
        /////// "vmName => [ property : [ "error1", "error2", ... "errorN" ] ].
        /////// </summary>
        /////// <returns>Dictionnaire des erreurs de validation.</returns>
        ////public Dictionary<string, Dictionary<string, string[]>> ProcessMultipleVmErrors()
        ////{
        ////    return this.Errors.Select(m => new
        ////    {
        ////        vm = m.PropertyName.Split('.')[0],
        ////        property = m.PropertyName.Split('.')[1],
        ////        errorMsg = m.ErrorMessage
        ////    })
        ////    .GroupBy(e => e.vm)
        ////    .ToDictionary(
        ////        g1 => g1.Key,
        ////        g1 => g1
        ////            .GroupBy(e => e.property)
        ////            .ToDictionary(
        ////            g2 => g2.Key,
        ////            g2 => g2.Select(e => e.errorMsg).ToArray()));
        ////}

        /// <summary>
        /// Charge un dictionnaire des critères de filtre et de leur valeur associée.
        /// </summary>
        /// <typeparam name="C">Type des critères.</typeparam>
        /// <typeparam name="V">Type de la vue modèle contenant les valeurs.</typeparam>
        /// <param name="mapVm">Map associant les critères de filtre définit sur le modèle aux propriétés de la vue modèle.</param>
        /// <returns>Dictionnaire contenant uniquement les critères actifs et leur valeur.</returns>
        public Dictionary<C, object> GetDicoFilter<C, V>(Dictionary<C, Func<V, object>> mapVm)
            where V : BaseVm
        {
            Dictionary<C, object> dicoFilter = new Dictionary<C, object>();

            foreach (KeyValuePair<C, Func<V, object>> kv in mapVm)
            {
                var value = kv.Value((V)this);
                if (value != null && !string.IsNullOrEmpty(value.ToString()))
                {
                    dicoFilter.Add(kv.Key, value);
                }
            }

            return dicoFilter;
        }

        /// <summary>
        /// Debug une view model en bouclant sur l'ensemble de ses propriétés publique et d'instance.
        /// </summary>
        /// <returns>une chaine avec les couples propriété, valeur.</returns>
        public string DebugVm()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                foreach (PropertyInfo descriptor in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(this);
                    sb.AppendFormat("{0}={1}{2}", name, value, Environment.NewLine);
                }
            }
            catch (Exception)
            {
            }

            return sb.ToString();
        }
    }
}