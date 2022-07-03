using FluentValidation;

namespace App.Endpoints.InfoTemplates;

public class FormRequestDetailsValidator : AbstractValidator<FormRequestDetails>
{
    public FormRequestDetailsValidator()
    {
        RuleFor(x => x.Title).NotEmpty().Length(2, 250);
        RuleFor(x => x.Template).NotEmpty();
        RuleForEach(x => x.Template.Groups)
            .ChildRules(groups =>
            {
                groups.RuleFor(x => x.Title).NotNull().Length(0, 250);
                groups.RuleForEach(x => x.Items).NotEmpty();
                groups.RuleForEach(x => x.Items).ChildRules(items =>
                {
                    items.RuleFor(x => x.Key).NotNull().Length(0, 250);
                    items.RuleFor(x => x.Value).NotNull().Length(1, 1000);
                });
            });
    }
}