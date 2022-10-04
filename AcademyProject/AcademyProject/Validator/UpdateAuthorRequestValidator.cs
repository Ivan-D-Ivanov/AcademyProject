using FluentValidation;
using AcademyProjectModels.Request;

namespace AcademyProject.Validator
{
    public class UpdateAuthorRequestValidator : AbstractValidator<UpdateAuthorRequest>
    {
        public UpdateAuthorRequestValidator()
        {
            RuleFor(x => x.Age).InclusiveBetween(18, 80);
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(25);
            When(x => !string.IsNullOrEmpty(x.NickName), () =>
            {
                RuleFor(x => x.NickName).MinimumLength(2).MaximumLength(15);
            });
            RuleFor(x => x.DateOfBirth).GreaterThan(DateTime.MinValue).LessThan(DateTime.MaxValue);
        }
    }
}
