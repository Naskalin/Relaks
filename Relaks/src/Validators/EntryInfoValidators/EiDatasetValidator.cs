using FluentValidation;
using Relaks.Models;

namespace Relaks.Validators.EntryInfoValidators;

public class EiDatasetValidator : AbstractValidator<EiDataset>
{
    public EiDatasetValidator()
    {
        // RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x).SetValidator(new EntryInfoValidator());
        RuleFor(x => x).SetValidator(new SoftDeletedValidator());

        RuleFor(x => x.DatasetValue).NotEmpty();
        RuleFor(x => x.Dataset).NotEmpty().SetValidator(new DatasetModelValidator());
    }
}