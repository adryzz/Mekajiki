using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using AspNetCore.Totp;

namespace Mekajiki.Server.Security;

public static class SecurityManager
{
    private static List<User> users = new();
    private static string key = "";

    public static void Initialize()
    {
        if (File.Exists("users.json"))
        {
            var jsonUtf8Bytes = File.ReadAllBytes("users.json");
            var utf8Reader = new Utf8JsonReader(jsonUtf8Bytes);
            users = JsonSerializer.Deserialize<List<User>>(ref utf8Reader);
        }
        else
        {
            Save();
        }

        if (File.Exists("key.txt"))
        {
            key = File.ReadAllText("key.txt");
        }
        else
        {
            key = Guid.NewGuid().ToString();
            File.WriteAllText("key.txt", key);
        }
    }

    public static void Save()
    {
        var jsonString = JsonSerializer.Serialize(users);
        File.WriteAllText("users.json", jsonString);
    }

    public static string NewUser(string name, int otp)
    {
        if (auth(otp))
        {
            var token = Guid.NewGuid().ToString();

            var hash = Encoding.ASCII.GetString(SHA512.HashData(Encoding.ASCII.GetBytes(token)));
            users.Add(new User {Name = name, TotpTokenHash = hash});
            Save();
            return token;
        }

        throw new UnauthorizedAccessException();
    }

    public static bool IsUser(string token)
    {
        var hash = Encoding.ASCII.GetString(SHA512.HashData(Encoding.ASCII.GetBytes(token)));
        foreach (var s in users)
            if (hash.Equals(s.TotpTokenHash))
                return true;

        return false;
    }

    private static bool auth(int otp)
    {
        var gen = new TotpGenerator();
        var val = new TotpValidator(gen);

        return val.Validate(key, otp);
    }
}