using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using AspNetCore.Totp;


namespace Mekajiki.Security
{
    public static class SecurityManager
    {
        private static Dictionary<string, string> users = new();
        private static string key = "";
        
        public static void Initialize()
        {
            if (File.Exists("users.json"))
            {
                byte[] jsonUtf8Bytes = File.ReadAllBytes("users.json");
                var utf8Reader = new Utf8JsonReader(jsonUtf8Bytes);
                users = JsonSerializer.Deserialize<Dictionary<string, string>>(ref utf8Reader);
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
            string jsonString = JsonSerializer.Serialize(users);
            File.WriteAllText("users.json", jsonString);
        }
        
        public static string NewUser(string name, int otp)
        {
            if (auth(otp))
            {
                string token = Guid.NewGuid().ToString();

                string hash = Encoding.ASCII.GetString(SHA512.HashData(Encoding.ASCII.GetBytes(token)));
                users.Add(name, hash);
                Save();
                return token;
            }

            throw new UnauthorizedAccessException();
        }

        public static bool IsUser(string token)
        {
            string hash = Encoding.ASCII.GetString(SHA512.HashData(Encoding.ASCII.GetBytes(token)));
            foreach (string s in users.Values)
            {
                if (hash.Equals(s))
                {
                    return true;
                }
            }

            return false;
        }
        
        private static bool auth(int otp)
        {
            var gen = new TotpGenerator();
            var val = new TotpValidator(gen);

            return val.Validate(key, otp);
        }
    }
}