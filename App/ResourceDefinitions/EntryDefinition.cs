using System.Net;
using System.Reflection;
using App.Models;
using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Errors;
using JsonApiDotNetCore.Middleware;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Serialization.Objects;
using Microsoft.EntityFrameworkCore;

namespace App.ResourceDefinitions;

public class EntryDefinition : JsonApiResourceDefinition<Entry, Guid>
{
    public EntryDefinition(IResourceGraph resourceGraph) : base(resourceGraph)
    {
    }

    public override Task OnWritingAsync(Entry resource, WriteOperationKind writeOperation, CancellationToken cancellationToken)
    {
        if (resource.Description == "")
        {
            resource.Description = null;
            
            // throw new JsonApiException(new ErrorObject(HttpStatusCode.BadRequest)
            // {
            //     Title = "Target resource was modified by another user.",
            //     Detail = "User resource.",
            //     Source = new ErrorSource()
            //     {
            //         Pointer = "/data/attribute/my-attr"
            //     }
            // });
        }

        return base.OnWritingAsync(resource, writeOperation, cancellationToken);
    }

    public override Task OnPrepareWriteAsync(Entry resource, WriteOperationKind writeOperation, CancellationToken cancellationToken)
    {
        if (writeOperation == WriteOperationKind.CreateResource)
        {
            resource.CreatedAt = DateTime.UtcNow;   
        }
        
        resource.UpdatedAt = DateTime.UtcNow;

        return base.OnPrepareWriteAsync(resource, writeOperation, cancellationToken);
    }

    public override QueryStringParameterHandlers<Entry> OnRegisterQueryableHandlersForQueryStringParameters()
    {
        return new QueryStringParameterHandlers<Entry>
        {
            ["like"] = (source, parameterValue) => source
                .Where(item => 
                    EF.Functions.Like(item.Name, "%" + parameterValue + "%")
                    || item.Description != null && EF.Functions.Like(item.Description, "%" + parameterValue + "%")
                )
        };
    }
}