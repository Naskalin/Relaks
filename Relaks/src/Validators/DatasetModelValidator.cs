using FluentValidation;
using Relaks.Models;

namespace Relaks.Validators;

public class DatasetModelValidator : AbstractValidator<DatasetModel>
{
    public DatasetModelValidator()
    {
        RuleFor(x => x.Groups)
            .NotEmpty()
            .Must((x, _) => x.Groups.Count > 0).WithMessage("Должна быть минимум одна группа");

        RuleForEach(x => x.Groups)
            .Must(g => g.Items.Count > 0).WithMessage("В группе должна быть минимум одна запись")
            .ChildRules(group =>
            {
                group.When(x => !string.IsNullOrEmpty(x.Title), () =>
                {
                    group.RuleFor(x => x.Title).MaximumLength(255);
                });
                group.RuleForEach(x => x.Items).ChildRules(item =>
                {
                    item.RuleFor(x => x.Key).Must((x, _) => !string.IsNullOrEmpty(x.Value) || !string.IsNullOrEmpty(x.Key))
                        .WithMessage("Ключ или значение должны быть заполнены");

                    item.When(x => !string.IsNullOrEmpty(x.Key), () =>
                    {
                        item.RuleFor(x => x.Key).MaximumLength(255);
                    });
                    item.When(x => !string.IsNullOrEmpty(x.Value), () =>
                    {
                        item.RuleFor(x => x.Value).MaximumLength(1000);
                    });
                });
            })
            ;
    }
}