using FluentValidation;

namespace App.Endpoints.AppDataDir;

public class FormDetailsValidator : AbstractValidator<FormRequestDetails>
{
    public FormDetailsValidator()
    {
        RuleFor(x => x.DataDir)
            .NotEmpty()
            .WithMessage("Введите путь к директории для размещения ваших данных.");

        RuleFor(x => x.DataDir)
            .Must((_, dataDir) => Directory.Exists(dataDir))
            .WithMessage("Введите существующий путь к директории.");

        RuleFor(x => x.DataDir)
            .Must((_, dataDir) => IsDirectoryWritable(dataDir))
            .WithMessage("Директория закрыта для записи (возможно, это директория системная)");
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