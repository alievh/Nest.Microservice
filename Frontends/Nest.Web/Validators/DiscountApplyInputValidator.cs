using FluentValidation;
using Nest.Web.Models.Discount;

namespace FreeCourse.Web.Validators;

public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
{
    public DiscountApplyInputValidator()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Code can't be empty");
    }
}
