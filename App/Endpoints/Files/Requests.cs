namespace App.Endpoints.Files;

public class DownloadFilesRequest
{
    public Guid FileId { get; set; }
    public string? ImageFilter { get; set; }
}

public class ExplorerFilesRequest
{
    public Guid FileId { get; set; }
}