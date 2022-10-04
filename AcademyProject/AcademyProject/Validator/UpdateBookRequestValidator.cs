using FluentValidation;
using AcademyProjectModels.Request;

namespace AcademyProject.Validator
{
    public class UpdateBookRequestValidator : AbstractValidator<UpdateBookRequest>
    {
        public UpdateBookRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MinimumLength(5).MaximumLength(20);
        }
    }
}
