using FluentValidation;
using Nest.Web.Models.Catalog;

namespace FreeCourse.Web.Validators;

public class CourseUpdateInputValidator : AbstractValidator<ProductUpdateInput>
{
    public CourseUpdateInputValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name can not be null");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description can not be null");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Price can not be null").ScalePrecision(2, 6).WithMessage("Not a correct format");
    }
}
