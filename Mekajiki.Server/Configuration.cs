using System.Text.Json;

namespace Mekajiki.Server;

public class Configuration
{
    /// <summary>
    ///     The path where all the video files are located
    /// </summary>
    public string LibraryPath { get; set; }

    /// <summary>
    ///     All the file extensions to include in the indexing
    /// </summary>
    public string[] VideoFileTypes { get; set; } = {"mp4", "mkv"};

    /// <summary>
    ///     The time it takes for the cache of the listing to invalidate and flag as not up to date
    /// </summary>
    public TimeSpan LibraryCacheInvalidationTime { get; set; } = TimeSpan.FromMinutes(15);

    /// <summary>
    ///     The buffer size of the video stream
    /// </summary>
    public int VideoBufferSize { get; set; } = 65536;

    public static Configuration? FromFile(string name)
    {
        var jsonUtf8Bytes = File.ReadAllBytes(name);
        var utf8Reader = new Utf8JsonReader(jsonUtf8Bytes);
        return JsonSerializer.Deserialize<Configuration>(ref utf8Reader);
    }

    public void Save(string name)
    {
        var jsonString = JsonSerializer.Serialize(this);
        File.WriteAllText(name, jsonString);
    }

    public static bool Exists(string name)
    {
        return File.Exists(name);
    }
}