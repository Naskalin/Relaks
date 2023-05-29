using FluentValidation;
using Relaks.Interfaces;
using Relaks.Models;

namespace Relaks.Validators.AppFileValidators;

public class AppFileValidator : AbstractValidator<IAppFile>
{
    public AppFileValidator()
    {
        RuleFor(x => x.DisplayName).NotEmpty().Length(2, 255);
    }
}

public class BaseFileValidator : AbstractValidator<BaseFile>
{
    public BaseFileValidator()
    {
        RuleFor(x => x).NotEmpty().SetValidator(new AppFileValidator());
    }
}

public class EntryFileValidator : AbstractValidator<EntryFile>
{
    public EntryFileValidator()
    {
        RuleFor(x => x).NotEmpty().SetValidator(new AppFileValidator());
    }
}