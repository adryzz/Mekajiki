using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.Json.Serialization;

namespace Mekajiki.Types
{
    public class AnimeListing
    {
        public static AnimeListing? Cached;

        public ImmutableArray<IAnimeSeries> Series
        {
            get => _series.ToImmutableArray();
            set
            {
                CacheCreationTime = DateTime.Now;
                _series = value.ToList();
                getEpisodes(value);
                Cached = this;
            }
        }
        
        private List<IAnimeSeries> _series = new();

        private void getEpisodes(ImmutableArray<IAnimeSeries> episodes)
        {
            Dictionary<Guid, IAnimeEpisode> oldEpisodes = Cached == null ?  new() : Cached._episodes;
            _episodes = new Dictionary<Guid, IAnimeEpisode>();
            for (int i = 0; i < episodes.Length; i++)
            {
                for (int j = 0; j < episodes[i].Seasons.Count; j++)
                {
                    for (int k = 0; k < episodes[i].Seasons[j].Episodes.Count; k++)
                    {
                        if (episodes[i].Seasons[j].Episodes[k].EpisodeId == Guid.Empty)
                        {
                            for (int w = 0; w < oldEpisodes.Count; w++)
                            {
                                if (oldEpisodes.Values.ElementAt(w).FilePath.Equals(episodes[i].Seasons[j].Episodes[k].FilePath))
                                {
                                    episodes[i].Seasons[j].Episodes[k].EpisodeId =
                                        oldEpisodes.Values.ElementAt(w).EpisodeId;
                                }
                            }
                        }
                        _episodes.Add(episodes[i].Seasons[j].Episodes[k].EpisodeId, episodes[i].Seasons[j].Episodes[k]);
                    }
                }
            }
        }

        [JsonIgnore]
        public ImmutableDictionary<Guid, IAnimeEpisode> Episodes => _episodes.ToImmutableDictionary();
        
        private Dictionary<Guid, IAnimeEpisode> _episodes = new();

        public static DateTime? CacheCreationTime { get; set; }
    }
}