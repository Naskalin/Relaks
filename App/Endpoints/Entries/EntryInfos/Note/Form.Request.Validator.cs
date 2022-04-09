using FluentValidation;

namespace App.Endpoints.Entries.EntryInfos.Note;

public class FormRequestValidator : AbstractValidator<RequestNoteDetails>
{
    public FormRequestValidator()
    {
        Include(new FormCommonValidator());
        RuleFor(x => x.Note).NotEmpty().MaximumLength(10000);
    }
}