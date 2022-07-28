namespace App.Endpoints.EntryFiles.Meta;

public class PutMetaValidator : Validator<PutMetaRequest>
{
    public PutMetaValidator()
    {
        RuleFor(x => x.NewValue).NotEmpty().Length(0, 200);
    }
}