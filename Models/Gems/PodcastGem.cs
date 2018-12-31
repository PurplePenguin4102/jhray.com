using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jhray.com.Models.Gems
{
    public class PodcastGem : IGem
    {
        private string _audioLink;
        public string AudioLink
        {
            get
            {
                return _audioLink.StartsWith("http://") ? _audioLink.Substring(5) : _audioLink;
            }
            set
            {
                if (_audioLink != value)
                {
                    _audioLink = value;
                }
            }
        }
        public string Title { get; set; }
        public string Text { get; set; }
        public string DisplayText => Text.Substring(0, Math.Min(180, Text.Length));
        
        public GemType Type { get; set; }
        public string Id { get; set; }
        public int FeedId { get; set; }
    }
}
