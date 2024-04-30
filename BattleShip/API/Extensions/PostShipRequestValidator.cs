using API.CustomMiddleware;
using Application.DTO;
using Application.Services.Validators;

namespace API.Extensions
{
    public static class PostShipRequestValidator
    {
        public static IApplicationBuilder UseFieldValidation(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware <ValidationMiddleware<PostPlayingFieldDTO>> (new PlayFieldValidator());
        }
    }
}
