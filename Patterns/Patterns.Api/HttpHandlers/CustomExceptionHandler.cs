using System.IO;
using System.Management.Instrumentation;
using System.Net;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using Patterns.Api.Exceptions;

namespace Patterns.Api.HttpHandlers
{
    public class CustomExceptionHandler : ExceptionHandler
    {

        public override void Handle(ExceptionHandlerContext context)
        {

            if (context.Exception is InvalidDataException || context.Exception is CustomValidationException)
                context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.BadRequest,
                    new {context.Exception.Message}));

            else if (context.Exception is InstanceNotFoundException)

                context.Result = new NotFoundResult(context.Request);
            else
            {
                context.Result =
                    new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.InternalServerError,
                        new ErrorInformation
                        {
                            Message = context.Exception.Message,
                            StackTrace = context.Exception.StackTrace
                        }
                        ));
            }
        }
    }

    internal class ErrorInformation
    {
        public string Message { get; set; }

        public string StackTrace { get; set; }
    }

}
