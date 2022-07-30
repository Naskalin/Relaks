using App.Models;

namespace App.Endpoints.InfoTemplates;

public class CreateRequestValidator : FormRequestValidator<CreateRequest>
{
}

public class UpdateRequestValidator : FormRequestValidator<PutRequest>
{
}

public class FormRequestValidator<T> : Validator<T> where T : InfoTemplateFormRequest
{
    public FormRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().Length(2, 250);
        RuleFor(x => x.Template).NotEmpty();
        RuleForEach(x => x.Template.Groups)
            .ChildRules(groups =>
            {
                groups.RuleFor(x => x.Title).NotNull().Length(0, 250);

                // Позволить создавать шаблоны без строк (пустые строки по прежнему запрещены)
                // groups.RuleForEach(x => x.Items).NotEmpty();
                groups.RuleForEach(x => x.Items).ChildRules(item =>
                {
                    item.RuleFor(x => x.Key).NotNull().Length(0, 250);
                    item.RuleFor(x => x.Value).NotNull().Length(0, 1000);
                    item.RuleFor(x => x).Must((_, row) => IsOneNotEmpty(row))
                        .WithMessage("Вы не можете сохранить пустую строку, ключ или значение должны быть заполнены.");
                });
            });
    }


    // Хотя бы один не пустой, иначе будут просто пустые строки
    private bool IsOneNotEmpty(CustomInfoItem item)
    {
        return !String.IsNullOrEmpty(item.Key) || !String.IsNullOrEmpty(item.Value);
    }
}