using FluentValidation;
using Nest.Web.Models.Catalog;

namespace Nest.Web.Validators;

public class CourseCreateInputValidator : AbstractValidator<ProductCreateInput>
{
	public CourseCreateInputValidator()
	{
		RuleFor(x => x.Name).NotEmpty().WithMessage("Name can not be null");
		RuleFor(x => x.Description).NotEmpty().WithMessage("Description can not be null");
		RuleFor(x => x.Price).NotEmpty().WithMessage("Price can not be null").ScalePrecision(2, 6).WithMessage("Not a correct format");
		RuleFor(x => x.SubCategoryId).NotEmpty().WithMessage("Category must be choosen");
	}
}
