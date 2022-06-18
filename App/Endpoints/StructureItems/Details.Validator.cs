using App.Repository;
using FluentValidation;
using FluentValidation.Results;

namespace App.Endpoints.StructureItems;

public class DetailsValidator : AbstractValidator<StructureItemFormDetails>
{
    public DetailsValidator()
    {
        RuleFor(x => x.Comment).NotNull().Length(0, 250);
        RuleFor(x => x.EntryId).NotEmpty();
        RuleFor(x => x.StructureId).NotEmpty();
        RuleFor(x => x.StartAt).NotEqual(default(DateTime));
            
        RuleFor(x => x.DeletedAt).NotEqual(default(DateTime));
        RuleFor(x => x.DeletedReason).NotNull().Length(0, 250);
    }
}

public class StructureItemDbValidate
{
    private readonly StructureRepository _structureRepository;
    private readonly EntryRepository _entryRepository;

    public StructureItemDbValidate(StructureRepository structureRepository, EntryRepository entryRepository)
    {
        _structureRepository = structureRepository;
        _entryRepository = entryRepository;
    }

    public async Task<List<ValidationFailure>> ValidateAsync(StructureItemFormDetails details, CancellationToken cancellationToken)
    {
        var errors = new List<ValidationFailure>();
        var entry = await _entryRepository.FindByIdAsync(details.EntryId, cancellationToken);
        if (entry == null) errors.Add(new ValidationFailure("EntryId", "EntryId not found"));
        
        var structure = await _structureRepository.FindByIdAsync(details.StructureId, cancellationToken);
        if (structure == null) errors.Add(new ValidationFailure("StructureId", "StructureId not found"));

        return errors;
    }
}