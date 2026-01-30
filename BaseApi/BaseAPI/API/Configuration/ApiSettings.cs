namespace BaseAPI.API.Configuration;

public class ApiSettings
{
    public string ApiName { get; set; } = "Base API";
    public string ApiVersion { get; set; } = "1.0.0";
    public string ApiDescription { get; set; } = string.Empty;
    public ContactInfo Contact { get; set; } = new();
    public LicenseInfo License { get; set; } = new();
}

public class ContactInfo
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}

public class LicenseInfo
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}
