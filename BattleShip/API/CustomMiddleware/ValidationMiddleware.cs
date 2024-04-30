using FluentValidation;
//using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace API.CustomMiddleware
{
    public class ValidationMiddleware<T>  where T : class
    {
        private readonly RequestDelegate _next;
        private readonly IValidator<T> _validator;
        public ValidationMiddleware(RequestDelegate next, IValidator<T> validator) 
        {
            _validator = validator;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context == null || context.Request.Body == null || !context.Request.Body.CanRead || context.Request.ContentType == null || !context.Request.ContentType.Contains("application/json"))
            {
                await _next(context);
                return;
            }

            context.Request.EnableBuffering();

            var httpMethodName = context.Request.Method.ToLower();
            var validatorType = typeof(T).ToString().ToLower();
            var pathName = context.Request.Path.ToString().Trim(new char[] { '/' , '"'}).ToLower();
            if (!validatorType.Contains(httpMethodName) || !validatorType.Contains(pathName))
            {
                await _next(context);
                return;
            }
            var requestObject = await context.Request.ReadFromJsonAsync<T>();

            var validationResult = await _validator.ValidateAsync(requestObject);
            if (!validationResult.IsValid)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var errors = validationResult.Errors;
                var errorResponse = JsonSerializer.Serialize(errors);

                await context.Response.WriteAsync(errorResponse);
                return;
            }
            //var newRequestBodyJson = System.Text.Json.JsonSerializer.Serialize(requestObject);
            //var newRequestBodyBytes = Encoding.UTF8.GetBytes(newRequestBodyJson);
            //var newRequestBodyStream = new MemoryStream(newRequestBodyBytes);
            //context.Request.Body = newRequestBodyStream;
            //context.Request.ContentLength = newRequestBodyStream.Length;
            //context.Request.ContentType = "application/json";


            context.Request.Body.Position = 0;
            await _next(context);
            return;
        }
    }
}
