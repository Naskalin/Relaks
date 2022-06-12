using FluentValidation;

namespace App.Endpoints.Entries.EntryFiles;

public class PutRequestValidator : AbstractValidator<EntryFilePutDetails>
{
    public PutRequestValidator()
    {
        RuleFor(x => x.Name).NotNull().Length(0, 250);
        RuleFor(x => x.DeletedAt).NotEqual(default(DateTime));
        RuleFor(x => x.DeletedReason).NotNull().Length(0, 250);
        RuleFor(x => x.Category).NotNull().Length(0, 200);
        RuleForEach(x => x.Tags).NotNull().Length(0, 200);
    }
}