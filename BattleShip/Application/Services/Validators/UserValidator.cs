using System;
using Application.DTO.UserDTO;
using BattleShip.Models;
using FluentValidation;

namespace Application.Services.Validators
{
    public class UserValidator : AbstractValidator<PostUserDTO>
    {
        public UserValidator()
        {
            this.RuleFor(x => x.FirstName)
                .Name();
            this.RuleFor(x => x.LastName)
                .Name();
            this.RuleFor(x => x.Role)
                .IsInEnum()
                .WithMessage("Role is enum with range 0 - 1");
        }
    }
}
