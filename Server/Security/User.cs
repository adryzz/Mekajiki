namespace Mekajiki.Server.Security;

public class User
{
    public string Name { get; set; }
    public string TotpTokenHash { get; set; }
    public string Fido2Token { get; set; }
}