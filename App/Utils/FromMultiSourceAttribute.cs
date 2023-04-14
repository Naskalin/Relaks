using Microsoft.AspNetCore.Mvc.ModelBinding;
using BindingSource = Microsoft.AspNetCore.Mvc.ModelBinding.BindingSource;

namespace App.Utils;

// Используется в ApiEndpoints для сложных реквестов, например put
// https://github.com/ardalis/ApiEndpoints/issues/161#issuecomment-1060857204
public sealed class FromMultiSourceAttribute : Attribute, IBindingSourceMetadata
{
    public BindingSource? BindingSource { get; } = CompositeBindingSource.Create(
        new[] {BindingSource.Path, BindingSource.Query, BindingSource.Form},
        nameof(FromMultiSourceAttribute));
}