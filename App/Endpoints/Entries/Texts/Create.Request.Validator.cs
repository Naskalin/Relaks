using App.Models;
using App.Utils;
using App.Utils.Extensions;
using FluentValidation;

namespace App.Endpoints.Entries.Texts;

public class CreateRequestValidator : AbstractValidator<EntryTextDetails>
{
    public CreateRequestValidator()
    {
        RuleFor(x => x.About).NotNull().Length(0, 255);
        RuleFor(x => x.TextType).IsEnumName(typeof(TextTypeEnum), false);

        RuleFor(x => x.ActualStartAt).NotEmpty().NotEqual(default(DateTime));
        RuleFor(x => x.ActualEndAt).NotEqual(default(DateTime));
        RuleFor(x => x.ActualStartAtReason).NotNull().Length(0, 500);
        RuleFor(x => x.ActualEndAtReason).NotNull().Length(0, 500);

        RuleFor(x => x.Val).NotEmpty().MaximumLength(5000);
        RuleFor(x => x.Val).Must(IsPhoneValid)
            .When(x => IsTextType(x.TextType, TextTypeEnum.Phone))
            .WithMessage("Ошибка при разборе телефона. Данный номер не может существовать для выбранного региона.");
        RuleFor(x => x.Val).Must(IsPhoneValid).When(x => x.TextType.Equals<TextTypeEnum>(TextTypeEnum.Phone));
        RuleFor(x => x.Val).EmailAddress().When(x => IsTextType(x.TextType, TextTypeEnum.Email));
        RuleFor(x => x.Val)
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .When(x => IsTextType(x.TextType, TextTypeEnum.Url))
            .WithMessage("Ошибка при разборе ссылки. Требуется абсолютный url.");
    }

    private static bool IsTextType(string str, TextTypeEnum textTypeEnum)
    {
        return Enum.TryParse(str, true, out TextTypeEnum result) && result == textTypeEnum;
    }

    private bool IsPhoneValid(string numberWithRegion)
    {
        try
        {
            PhoneHelper.ToPhone(numberWithRegion);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}