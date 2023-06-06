using FluentValidation;
using Relaks.Models;

namespace Relaks.Validators;

public class DatasetTemplateRequestValidator : AbstractValidator<DatasetTemplateRequest>
{
    public DatasetTemplateRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Dataset).NotEmpty().SetValidator(new DatasetModelValidator());
    }
}