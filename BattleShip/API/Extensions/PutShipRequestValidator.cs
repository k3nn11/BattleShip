using API.CustomMiddleware;
using Application.DTO.ShipDTO;
using Application.Services.Validators;

namespace API.Extensions
{
    public static class PutShipRequestValidator
    {
        public static IApplicationBuilder UsePutShipvalidator(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidationMiddleware<PutShipDTO>>(new PutShipValidator());
        }
    }
}
