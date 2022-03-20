using App.Models;
using FluentValidation;

namespace App.Endpoints.Entries.Texts;

public class ListRequestValidator : AbstractValidator<ListRequest>
{
    public ListRequestValidator()
    {
        RuleFor(x => x.TextType).NotEmpty().IsEnumName(typeof(TextTypeEnum), false);
    }
}