namespace Mekajiki.Server.Security;

public class User
{
    public string? Name { get; set; }

    public  List<Session>? OpenSessions { get; set; }
    
    public string? TotpSeed { get; set; }
    public string? Fido2Token { get; set; }
}