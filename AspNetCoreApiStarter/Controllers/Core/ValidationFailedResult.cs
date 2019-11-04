using AspNetCoreApiStarter.ViewModels.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AspNetCoreApiStarter.ViewModels.Core.ErrorVm;

namespace AspNetCoreApiStarter.Controllers.Core
{
    public class ValidationFailedResult : ActionResult
    {
        private readonly ModelStateDictionary modelState;

        public ValidationFailedResult(ModelStateDictionary modelState)
        {
            this.modelState = modelState;
        }

        public async override Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var errorVm = new ErrorVm()
            {
                Code = ErrorCode.ValidationFailed,
                ValidationDictionnay = ToValidation(modelState)
            };

            var jsonResult = new JsonResult(errorVm);
            jsonResult.StatusCode = StatusCodes.Status400BadRequest;

            await jsonResult.ExecuteResultAsync(context);
        }

        /// <summary>
        /// Chargement d'un dictionnaire simple pour renvoyer les informations de validation serveur
        /// "property => [ "error1", "error2", ... "errorN" ]".
        /// </summary>
        /// <param name="modelState">Informations de validation.</param>
        /// <returns>Dictionnaire des erreurs de validation.</returns>
        public static object ToValidation(ModelStateDictionary modelState)
        {
            return modelState.ToDictionary(kv => kv.Key, kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToList());
        }
    }
}
