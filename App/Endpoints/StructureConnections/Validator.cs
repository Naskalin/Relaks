namespace App.Endpoints.StructureConnections;

public class StructureConnectionCreateValidator : StructureConnectionFormValidator<StructureConnectionCreateRequest> {}
public class StructureConnectionUpdateValidator : StructureConnectionFormValidator<StructureConnectionPutRequest> {}

public class StructureConnectionFormValidator<T> : Validator<T> where T : StructureConnectionFormRequest
{
    public StructureConnectionFormValidator()
    {
        RuleFor(x => x.Description).NotNull().Length(0, 500);
        RuleFor(x => x.StructureFirstId).NotEmpty();
        RuleFor(x => x.StructureSecondId).NotEmpty();
        RuleFor(x => x.StructureFirstId)
            .Must((model, field) => !field.Equals(model.StructureSecondId))
            .WithMessage("Группа может быть связана только с другой группой.");
        RuleFor(x => x.StartAt).NotEqual(default(DateTime));
        RuleFor(x => x.DeletedAt).NotEqual(default(DateTime));
        RuleFor(x => x.DeletedReason).NotNull().Length(0, 250);
    }
}