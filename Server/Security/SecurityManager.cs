using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using AspNetCore.Totp;

namespace Mekajiki.Server.Security;

public static class SecurityManager
{
    private static List<User> _users = new();
    private static string _key = "";

    public static void Initialize()
    {
        if (File.Exists("users.json"))
        {
            var jsonUtf8Bytes = File.ReadAllBytes("users.json");
            var utf8Reader = new Utf8JsonReader(jsonUtf8Bytes);
            _users = JsonSerializer.Deserialize<List<User>>(ref utf8Reader);
        }
        else
        {
            Save();
        }

        if (File.Exists("key.txt"))
        {
            _key = File.ReadAllText("key.txt");
        }
        else
        {
            _key = Guid.NewGuid().ToString();
            File.WriteAllText("key.txt", _key);
        }
    }

    public static void Save()
    {
        var jsonString = JsonSerializer.Serialize(_users);
        File.WriteAllText("users.json", jsonString);
    }
    
    

    public static string NewUserTotp(string name, string serverToken, string? userAgent, out string manualKey, out string image)
    {
        if (serverToken != _key)
            throw new UnauthorizedAccessException();
        
        if (_users.Any(x => x.Name == name))
            throw new ArgumentException();
        
        var token = Guid.NewGuid().ToString();
        var hash = Encoding.ASCII.GetString(SHA512.HashData(Encoding.ASCII.GetBytes(token)));
        
        var seed = Guid.NewGuid().ToString();
        
        var qrGenerator = new TotpSetupGenerator();
        var qrCode = qrGenerator.Generate(
            "Mekajiki",
            name,
            seed
        );
        
        image = qrCode.QrCodeImage;
        manualKey = qrCode.ManualSetupKey;
        
        _users.Add(new User
        {
            Name = name,
            TotpSeed = seed,
            OpenSessions = new List<Session>()
            {
                new Session
                {
                    UserAgent = userAgent,
                    TokenHash = hash
                }
            }
        });

        Save();
        return token;
    }

    public static bool IsUser(string token)
    {
        var hash = Encoding.ASCII.GetString(SHA512.HashData(Encoding.ASCII.GetBytes(token)));
        
        foreach (var s in _users)
            if (s.OpenSessions != null)
                if (s.OpenSessions.Any(x => x.TokenHash == hash))
                        return true;

        return false;
    }

    public static User? GetUser(string token)
    {
        var hash = Encoding.ASCII.GetString(SHA512.HashData(Encoding.ASCII.GetBytes(token)));
        
        foreach (var s in _users)
            if (s.OpenSessions != null)
                if (s.OpenSessions.Any(x => x.TokenHash == hash))
                    return s;

        return null;
    }

    public static string TotpAuthenticate(string name, int otp, string? userAgent)
    {
        if (otp < 100000 || otp > 999999)
            throw new ArgumentException();
        
        User? user = _users.Find(x => x.Name == name);

        if (user == null)
            throw new ArgumentException();
        
        var gen = new TotpGenerator();
        var val = new TotpValidator(gen);

        if (!val.Validate(user.TotpSeed, otp))
            throw new UnauthorizedAccessException();
        
        var token = Guid.NewGuid().ToString();
        var hash = Encoding.ASCII.GetString(SHA512.HashData(Encoding.ASCII.GetBytes(token)));
        new Session
        {
            UserAgent = userAgent,
            TokenHash = hash
        };
        
        return token;
    }
}