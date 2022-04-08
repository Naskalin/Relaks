using FluentValidation;

namespace App.Endpoints.Entries.EntryInfos.Date;

public class PutRequestValidator : AbstractValidator<EntryDatePutRequestDetails>
{
    public PutRequestValidator()
    {
        Include(new CreateRequestValidator());
        Include(new SoftDeleteRequestValidator());
    }
}