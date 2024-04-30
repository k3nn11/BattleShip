using API.CustomMiddleware;
using Application.DTO.ShipDTO;
using Application.Services.Validators;

namespace API.Extensions
{
    public static class ShipRequestValidator
    {
       public static IApplicationBuilder UsePostShipValidation(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidationMiddleware<PostShipDTO>>(new PostShipValidator());
        }
    }
}
