using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jhray.com.Database.Entities
{
    public class Podcast
    {
        [Key]
        [ForeignKey("FK_Podcast_Gem")]
        public int Id { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Location { get; set; }
        public string ItunesDuration { get; set; }
        public DateTimeOffset PubDate { get; set; }
        public long LengthInBytes { get; set; }

        public int FeedId { get; set; }
        public Gem GemData { get; set; }
    }
}
