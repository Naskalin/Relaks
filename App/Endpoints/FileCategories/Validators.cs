namespace App.Endpoints.FileCategories;

public class FileCategoryCreateValidator : FileCategoryFormValidator<FileCategoryCreateRequest> {}
public class FileCategoryUpdateValidator : FileCategoryFormValidator<FileCategoryPutRequest> {}

public class FileCategoryFormValidator<T> : Validator<T> where T : FileCategoryFormRequest
{
    public FileCategoryFormValidator()
    {
        RuleFor(x => x.Title).NotEmpty().Length(1, 250);
        RuleFor(x => x.EntryId).NotEmpty();
        
        RuleFor(x => x.DeletedAt).NotEqual(default(DateTime));
        RuleFor(x => x.DeletedReason).NotNull().Length(0, 250);
    }
}