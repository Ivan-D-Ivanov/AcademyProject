using FluentValidation;
using AcademyProjectModels.Request;

namespace AcademyProject.Validator
{
    public class AddBookRequestValidator : AbstractValidator<AddBookRequest>
    {
        public AddBookRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MinimumLength(5).MaximumLength(20);
        }
    }
}
