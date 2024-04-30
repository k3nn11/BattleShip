using Application.DTO.ShipDTO;
using FluentValidation;
using System;

namespace Application.Services.Validators
{
    public class PutShipValidator :AbstractValidator<PutShipDTO>
    {
        public PutShipValidator() 
        {
            this.RuleFor(x => x.Health)
               .GreaterThanOrEqualTo(1);
            this.RuleFor(x => x.Length)
                 .GreaterThanOrEqualTo(1);
            this.RuleFor(x => x.Speed) 
                .GreaterThanOrEqualTo(1);
        }
    }
}
