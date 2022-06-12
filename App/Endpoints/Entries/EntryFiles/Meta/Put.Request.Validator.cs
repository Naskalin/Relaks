using FluentValidation;

namespace App.Endpoints.Entries.EntryFiles.Meta;

public class PutRequestValidator : AbstractValidator<PutDetails>
{
    public PutRequestValidator()
    {
        RuleFor(x => x.NewValue).NotEmpty().Length(0, 200);
    }
}