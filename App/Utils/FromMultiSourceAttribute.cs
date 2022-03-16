using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace App.Utils;

// https://github.com/ardalis/ApiEndpoints/issues/161#issuecomment-1060857204
public sealed class FromMultiSourceAttribute : Attribute, IBindingSourceMetadata
{
    public BindingSource BindingSource { get; } = CompositeBindingSource.Create(
        new[] { BindingSource.Path, BindingSource.Query },
        nameof(FromMultiSourceAttribute));
}