using FluentValidation;
using Relaks.Interfaces;

namespace Relaks.Validators;

public class SoftDeletedValidator : AbstractValidator<ISoftDeletedReason>
{
    public SoftDeletedValidator()
    {
        RuleFor(x => x.DeletedAt).NotEqual(default(DateTime));
        When(x => !string.IsNullOrEmpty(x.DeletedReason), () =>
        {
            RuleFor(x => x.DeletedReason).Length(2, 500);
        });
    }
}