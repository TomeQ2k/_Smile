using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Smile.Core.Application.Exceptions;
using Smile.Core.Application.Extensions;
using Smile.Core.Application.Logging;
using Smile.Core.Application.Logic.Responses;
using Smile.Core.Application.Models.Error;
using Smile.Core.Common.Helpers;

namespace Smile.Core.Application.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            var errorMessage = context.Exception.Message;
            var (statusCode, errorCode) = (HttpStatusCode.InternalServerError, ErrorCodes.ServerError);

            (statusCode, errorCode) = context.Exception switch
            {
                ServerException _ => (HttpStatusCode.InternalServerError, GetErrorCode(context.Exception)),
                CrudException _ => (HttpStatusCode.InternalServerError, GetErrorCode(context.Exception)),
                ResetPasswordException _ => (HttpStatusCode.InternalServerError, GetErrorCode(context.Exception)),
                TokenException _ => (HttpStatusCode.InternalServerError, GetErrorCode(context.Exception)),
                ProfileUpdateException _ => (HttpStatusCode.InternalServerError, GetErrorCode(context.Exception)),
                ChangePasswordException _ => (HttpStatusCode.InternalServerError, GetErrorCode(context.Exception)),
                UploadFileException _ => (HttpStatusCode.InternalServerError, GetErrorCode(context.Exception)),
                DeleteFileException _ => (HttpStatusCode.InternalServerError, GetErrorCode(context.Exception)),
                AdminActionException _ => (HttpStatusCode.InternalServerError, GetErrorCode(context.Exception)),
                HubConnectionException _ => (HttpStatusCode.InternalServerError, GetErrorCode(context.Exception)),

                AuthException _ => (HttpStatusCode.Unauthorized, GetErrorCode(context.Exception)),
                InvalidCredentialsException _ => (HttpStatusCode.Unauthorized, GetErrorCode(context.Exception)),
                AccountNotConfirmedException _ => (HttpStatusCode.Unauthorized, GetErrorCode(context.Exception)),

                EntityNotFoundException _ => (HttpStatusCode.NotFound, GetErrorCode(context.Exception)),

                ServiceException _ => (HttpStatusCode.ServiceUnavailable, GetErrorCode(context.Exception)),

                NoPermissionsException _ => (HttpStatusCode.Forbidden, GetErrorCode(context.Exception)),
                AccountBlockedException _ => (HttpStatusCode.Forbidden, GetErrorCode(context.Exception)),

                DuplicateException _ => (HttpStatusCode.Conflict, GetErrorCode(context.Exception)),

                OldPasswordInvalidException _ => (HttpStatusCode.BadRequest, GetErrorCode(context.Exception)),

                _ => (HttpStatusCode.InternalServerError, ErrorCodes.ServerError)
            };

            var jsonResponse = (new BaseResponse(Error.Build(errorCode, errorMessage, statusCode: statusCode))).ToJSON();

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.HttpContext.Response.ContentLength = Encoding.UTF8.GetBytes(jsonResponse).Length;

            context.HttpContext.Response.AddApplicationError(errorMessage);

            await context.HttpContext.Response.WriteAsync(jsonResponse);

            var logger = context.HttpContext.RequestServices.GetService<INLogger>();
            logger.Error(errorMessage, context.Exception);

            await base.OnExceptionAsync(context);
        }

        #region private

        private string GetErrorCode(Exception exception) => (string)exception.GetType().GetProperty("ErrorCode").GetValue(exception, null);

        #endregion
    }
}