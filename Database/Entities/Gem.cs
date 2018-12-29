using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jhray.com.Database.Entities
{
    public class Gem
    {
        [Key]
        [ForeignKey("FK_Gem_Podcast")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public string SummaryText { get; set; }
        public GemType GemType { get; set; }

        [ForeignKey("FK_ChilledUser_Gem")]
        public string CreatedById { get; set; }
        
        public Podcast PodcastData { get; set; }
        public Picture PictureData { get; set; }
        public ChilledUser CreatedBy { get; set; }
    }
}
