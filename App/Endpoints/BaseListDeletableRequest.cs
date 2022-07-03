using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints;

public class BaseListDeletableRequest : BaseListRequest
{
    [FromQuery]
    public bool? IsDeleted { get; set; }
}