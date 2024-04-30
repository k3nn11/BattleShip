using FluentValidation;
using System;

namespace Application.Services
{
    public static class EntityRuleExtension
    {
        public static IRuleBuilderOptions<T, string> Name<T>(this IRuleBuilder<T, string> rulebuilder)
        {
            return rulebuilder.NotNull().NotEmpty();
        }

        public static IRuleBuilderOptions<T, int> FieldMeasurementRule<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder.GreaterThanOrEqualTo(1).Must(x => x % 2 != 0).WithMessage("Should be greater than zero and an odd number");
        }

        public static IRuleBuilderOptions<T, int> ShipNumeralRule<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder.NotEmpty().GreaterThanOrEqualTo(1);
        }
    }
}
