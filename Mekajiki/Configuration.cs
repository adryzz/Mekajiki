using System;
using System.IO;
using System.Text.Json;

namespace Mekajiki
{
    public class Configuration
    {
        public string LibraryPath { get; set; }

        public string[] VideoFileTypes { get; set; } = { "mp4", "mkv" };
        
        public TimeSpan LibraryCacheInvalidationTime { get; set; } = TimeSpan.FromMinutes(15);

        public static Configuration? FromFile(string name)
        {
            byte[] jsonUtf8Bytes = File.ReadAllBytes(name);
            var utf8Reader = new Utf8JsonReader(jsonUtf8Bytes);
            return JsonSerializer.Deserialize<Configuration>(ref utf8Reader);
        }

        public void Save(string name)
        {
            string jsonString = JsonSerializer.Serialize(this);
            File.WriteAllText(name, jsonString);
        }

        public static bool Exists(string name) => File.Exists(name);
    }
}