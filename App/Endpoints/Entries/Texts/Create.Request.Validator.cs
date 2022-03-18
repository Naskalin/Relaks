using App.Models;
using App.Utils;
using FluentValidation;

namespace App.Endpoints.Entries.Texts;

public class CreateRequestValidator : AbstractValidator<CreateRequestDetails>
{
    public CreateRequestValidator()
    {
        RuleFor(x => x.About).NotNull().Length(0, 255);
        RuleFor(entryText => entryText.Val).NotEmpty().MaximumLength(5000);
        RuleFor(x => x.TextType).IsEnumName(typeof(EntryTextTypeEnum), false);

        RuleFor(x => x.ActualStartAt).NotEmpty().NotEqual(default(DateTime));
        RuleFor(x => x.ActualEndAt).NotEqual(default(DateTime));
        RuleFor(x => x.ActualStartAtReason).NotNull().Length(0, 500);
        RuleFor(x => x.ActualEndAtReason).NotNull().Length(0, 500);
    }

    public bool isPhoneValid(string numberWithRegion)
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
    // private bool BeValidValue(string textType, string val)
    // {
    //     Enum.TryParse(textType, true, out EntryTextTypeEnum textTypeEnum);
    //     
    //     switch (textTypeEnum)
    //     {
    //         
    //     }
    // }
}