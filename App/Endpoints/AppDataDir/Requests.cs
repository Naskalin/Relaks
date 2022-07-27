using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.AppDataDir;

public class FormRequest
{
    public string DataDir { get; set; } = null!;
}

public class CreateRequest : FormRequest
{
}