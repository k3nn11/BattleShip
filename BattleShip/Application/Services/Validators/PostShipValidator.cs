using System;
using Application.DTO.ShipDTO;
using FluentValidation;

namespace Application.Services.Validators
{
    public class PostShipValidator : AbstractValidator<PostShipDTO>
    {
        public PostShipValidator()
        {
            this.RuleFor(x => x.ShipType)
                .IsInEnum()
                .WithMessage("The range of enum value is 0 - 3");
            this.RuleFor(x => x.Health)
                .ShipNumeralRule();
            this.RuleFor(x => x.Length)
                 .ShipNumeralRule();
            this.RuleFor(x => x.Speed)
                .ShipNumeralRule();
        }
    }
}
