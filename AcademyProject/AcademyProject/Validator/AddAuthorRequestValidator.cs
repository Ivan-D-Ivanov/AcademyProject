using AcademyProjectModels.Request;
using FluentValidation;

namespace AcademyProject.Validator
{
    public class AddAuthorRequestValidator : AbstractValidator<AddAuthorRequest>
    {
        public AddAuthorRequestValidator()
        {
            RuleFor(x => x.Age).InclusiveBetween(18, 80);
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(25);
            When(x => !string.IsNullOrEmpty(x.NickName), () =>
            {
                RuleFor(x => x.NickName).MinimumLength(2).MaximumLength(15);
            });
        }
    }
}
