using System;
using System.Collections.Immutable;

namespace Mekajiki.Types
{
    public class AnimeListing
    {
        public static AnimeListing? Cached;

        public ImmutableArray<AnimeSeries> Series
        {
            get => Series;
            set
            {
                CacheCreationTime = DateTime.Now;
                Series = value;
                Cached = this;
            }
        }

        public static DateTime? CacheCreationTime { get; set; }
    }
}