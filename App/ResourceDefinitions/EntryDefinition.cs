using App.Models;
using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Resources;
using Microsoft.EntityFrameworkCore;

namespace App.ResourceDefinitions;

public class EntryDefinition : JsonApiResourceDefinition<BaseEntry, Guid>
{
    public EntryDefinition(IResourceGraph resourceGraph) : base(resourceGraph)
    {
    }
    
    public override QueryStringParameterHandlers<BaseEntry> OnRegisterQueryableHandlersForQueryStringParameters()
    {
        return new QueryStringParameterHandlers<BaseEntry>
        {
            ["like"] = (source, parameterValue) => source
                .Where(item => EF.Functions.Like(item.Name, "%" + parameterValue + "%"))
        };
    }
}