using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mekajiki.Types
{
    public interface IAnimeEpisode
    {
        public int? EpisodeIndex { get; set; }
        public string FileName { get; set; }
        
        /// <summary>
        /// Do NOT serialize the file path, as doing so exposes the internal file system.
        /// </summary>
        [JsonIgnore]
        public string FilePath { get; set; }

        public string Name { get; set; }
        
        public Guid EpisodeId { get; set; }
    }
}