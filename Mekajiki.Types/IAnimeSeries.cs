using System.Collections.Generic;

namespace Mekajiki.Types
{
    public interface IAnimeSeries
    {
        public string Name { get; set; }
        public string DirectoryName { get; set; }
        public List<IAnimeSeason> Seasons { get; set; }
    }
}