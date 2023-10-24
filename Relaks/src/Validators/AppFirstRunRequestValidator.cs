using FluentValidation;
using Relaks.Models.Misc;

namespace Relaks.Validators;

public class AppFirstRunRequestValidator : AbstractValidator<AppFirstRunRequest>
{
    public AppFirstRunRequestValidator()
    {
        RuleFor(x => x.StoreDirPath)
            .NotEmpty()
            .Must(Directory.Exists)
            ;
    }
}