namespace App.Endpoints.AppDataDir;

public class CreateRequestValidator : FormRequestValidator<CreateRequest>
{
    
}

public class FormRequestValidator<T> : Validator<T> where T : FormRequest
{
    public FormRequestValidator()
    {
        RuleFor(x => x.DataDir)
            .NotEmpty()
            .WithMessage("Введите путь к директории для размещения ваших данных.")
            
            .Must((_, dataDir) => Directory.Exists(dataDir))
            .WithMessage("Введите существующий путь к директории.")
            
            .Must((_, dataDir) => IsDirectoryWritable(dataDir))
            .WithMessage("Директория закрыта для записи (возможно, это директория системная)")
            ;

    }

    // https://stackoverflow.com/a/6371533/5638975
    private bool IsDirectoryWritable(string dirPath)
    {
        try
        {
            using (File.Create(
                       Path.Combine(
                           dirPath,
                           Path.GetRandomFileName()
                       ),
                       1,
                       FileOptions.DeleteOnClose)
                  )
            {
            }

            return true;
        }
        catch
        {
            return false;
        }
    }
}