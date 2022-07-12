using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.AppDataDir;

public class CreateRequest
{
    [FromBody]
    public FormRequestDetails Details { get; set; } = null!;
}

public class FormRequestDetails
{
    public string DataDir { get; set; } = null!;
}