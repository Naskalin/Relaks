using App.Models;
using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Resources;
using Microsoft.EntityFrameworkCore;

namespace App.ResourceDefinitions;

public class EntryDefinition : JsonApiResourceDefinition<Entry, Guid>
{
    public EntryDefinition(IResourceGraph resourceGraph) : base(resourceGraph)
    {
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