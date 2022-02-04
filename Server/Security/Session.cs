namespace Mekajiki.Server.Security;

public struct Session
{
    public string? UserAgent { get; set; }
    
    public string TokenHash { get; set; }
}