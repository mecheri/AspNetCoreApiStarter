using System;
using System.Threading.Tasks;
using AspNetCoreApiStarter.Resources;
using AspNetCoreApiStarter.Shared.CustomException;
using AspNetCoreApiStarter.Shared.Logger;
using AspNetCoreApiStarter.ViewModels.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using static AspNetCoreApiStarter.ViewModels.Core.ErrorVm;

namespace AspNetCoreApiStarter.Middlewares
{
    /// <summary>
    /// Error handling middleware class.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private static IStringLocalizer<SharedResources> localizer;
        private readonly RequestDelegate next;
        private readonly ILoggerHelper logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The Http Request delegate.</param>
        /// <param name="logger">The Logger instance.</param>
        /// <param name="localizer">The Localizer instance.</param>
        public ErrorHandlingMiddleware(
            RequestDelegate next,
            ILoggerHelper<ErrorHandlingMiddleware> logger,
            IStringLocalizer<SharedResources> localizer)
        {
            this.next = next;
            this.logger = logger;
            ErrorHandlingMiddleware.localizer = localizer;
        }

        /// <summary>
        /// Middleware end point.
        /// </summary>
        /// <param name="context">The Http context.</param>
        /// <returns>Task.</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                await BuildErrorResponse(context, ex);
            }
        }

        /// <summary>
        /// Build Error and write response.
        /// </summary>
        /// <param name="context">Http context.</param>
        /// <param name="exception">Current exception.</param>
        /// <returns>Error response body.</returns>
        private static Task BuildErrorResponse(HttpContext context, Exception exception)
        {
            // default
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            // error wrappée
            ErrorVm errorVm = new ErrorVm();
            errorVm.Code = ErrorCode.GenericServer;
            errorVm.Message = localizer[Constants.Errors.InternalServerError];
#if DEBUG
            errorVm.DebugMessage = exception.Message;
            errorVm.StackTrace = exception.StackTrace;
#endif

            if (exception is ConcurrentAccessException)
            {
                errorVm.Code = ErrorCode.ConcurrentAccess;
                errorVm.Message = "acces concurrent";
            }
            else if (exception is ForeignKeyException)
            {
                errorVm.Code = ErrorCode.ForeignKey;
                errorVm.Message = "error fk";
            }
            else if (exception is UniqueKeyException)
            {
                errorVm.Code = ErrorCode.UniqueKeyConstraint;
                errorVm.Message = "unique key";
            }
            else if (exception is AuthorizationException)
            {
                errorVm.Code = ErrorCode.AccessDenied;
                errorVm.Message = "accès refusé";
                context.Response.StatusCode = context.User.Identity.IsAuthenticated
                    ? (int)StatusCodes.Status403Forbidden
                    : (int)StatusCodes.Status401Unauthorized;
            }
            else if (exception is EntityNotFoundException)
            {
                errorVm.Code = ErrorCode.NotFound;
                errorVm.Message = "entité non trouvée";
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }

            string errorString = JsonConvert.SerializeObject(errorVm);
            return context.Response.WriteAsync(errorString);
        }
    }
}