using API.CustomMiddleware;
using Application.DTO.UserDTO;
using Application.Services.Validators;

namespace API.Extensions
{
    public static class UserRequestValidator
    {
        public static IApplicationBuilder UseUserValidation(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware <ValidationMiddleware<PostUserDTO>>(new UserValidator());
        }
    }
}
