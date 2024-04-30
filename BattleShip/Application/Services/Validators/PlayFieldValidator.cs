using System;
using Application.DTO;
using FluentValidation;

namespace Application.Services.Validators
{
    public class PlayFieldValidator : AbstractValidator<PostPlayingFieldDTO>
    {
        public PlayFieldValidator()
        {
            this.RuleFor(x => x.Width)
                .FieldMeasurementRule();
            this.RuleFor(x => x.Height)
              .FieldMeasurementRule();
            this.RuleFor(x => x.FieldName)
                .Name();
        }
    }
}
