using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jhray.com.Database.Entities
{
    public class RSSHeader
    {
        [Key]
        public int Id { get; set; }
        public int RSSNumber { get; set; }
        public string ChannelLink { get; set; }
        public string WebMaster { get; set; }
        public string ManagingEditor { get; set; }
        public string LogoTitle { get; set; }
        public string LogoUrl { get; set; }
        public string LogoLink { get; set; }
        public string ITunesName { get; set; }
        public string ITunesEmail { get; set; }
        public string ITunesCategory { get; set; }
        public string ITunesSubCategory { get; set; }
        public string ITunesCategory2 { get; set; }
        public string ITunesSubCategory2 { get; set; }
        public string ITunesKeywords { get; set; }
        public string ITunesExplicit { get; set; }
        public string ITunesImage { get; set; }
        public string AtomLink { get; set; }
        public string PubDate { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Subtitle { get; set; }
        public string LastBuildDate { get; set; }
    }
}
