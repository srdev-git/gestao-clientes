using FluentValidation;
using SRDev.GestaoClientes.Domain.Exceptions;
using System.Net;

namespace SRDev.GestaoClientes.API.Middlewares
{
    public static class ProblemDetailsFactory
    {
        public static ProblemDetails CreateProblemDetails(HttpContext context, Exception exception)
        {
            var statusCode = exception switch
            {
                EmailJaCadastradoException => HttpStatusCode.BadRequest,
                DomainException => HttpStatusCode.UnprocessableEntity,
                UnauthorizedAccessException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            var detail = exception switch
            {
                EmailJaCadastradoException => exception.Message,
                DomainException => exception.Message,
                UnauthorizedAccessException => exception.Message,
                _ => "Internal Server Error"
            };

            return new ProblemDetails
            {
                Type = GetProblemType((int)statusCode),
                Title = GetProblemTitle(exception),
                Status = (int)statusCode,
                Detail = detail,
                Instance = context.Request.Path,
                Errors = GetErrors(exception)
            };
        }

        private static string GetProblemType(int statusCode) => $"https://httpstatuses.com/{statusCode}";

        private static string GetProblemTitle(Exception exception) => exception switch
        {
            EmailJaCadastradoException => "E-mail já cadastrado",
            DomainException => "Erro de domínio",
            _ => "Erro interno do servidor"
        };

        private static Dictionary<string, string[]> GetErrors(Exception exception)
        {
            if (exception is ValidationException validationException)
            {
                return validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );
            }

            return new Dictionary<string, string[]>();
        }
    }
}
