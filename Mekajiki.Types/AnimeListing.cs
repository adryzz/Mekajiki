using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Mekajiki.Types
{
    public class AnimeListing
    {
        public static AnimeListing? Cached;

        public ImmutableArray<AnimeSeries> Series
        {
            get => _series.ToImmutableArray();
            set
            {
                CacheCreationTime = DateTime.Now;
                _series = value.ToList();
                Cached = this;
            }
        }

        private List<AnimeSeries> _series;

        public static DateTime? CacheCreationTime { get; set; }
    }
}