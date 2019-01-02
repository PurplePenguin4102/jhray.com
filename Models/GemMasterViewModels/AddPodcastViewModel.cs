using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jhray.com.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace jhray.com.Models.GemMasterViewModels
{
    public class AddPodcastViewModel
    {
        public List<RSSHeader> RSSHeaders { get; set; }

        [Required] public int RSSNumber { get; set; }
        [Required] public string ChannelLink { get; set; }
        [Required] public string WebMaster { get; set; }
        [Required] public string ManagingEditor { get; set; }
        [Required] public string LogoTitle { get; set; }
        [Required] public string LogoUrl { get; set; }
        [Required] public string LogoLink { get; set; }
        [Required] public string ITunesName { get; set; }
        [Required] public string ITunesEmail { get; set; }
        [Required] public string ITunesCategory { get; set; }
        [Required] public string ITunesSubCategory { get; set; }
        [Required] public string ITunesCategory2 { get; set; }
        [Required] public string ITunesSubCategory2 { get; set; }
        [Required] public string ITunesKeywords { get; set; }
        [Required] public string ITunesExplicit { get; set; }
        [Required] public string ITunesImage { get; set; }
        [Required] public string AtomLink { get; set; }
        [Required] public string PubDate { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Author { get; set; }
        [Required] public string Description { get; set; }
        [Required] public string Subtitle { get; set; }
        [Required] public string LastBuildDate { get; set; }

        public RSSHeader NewHeader { get => new RSSHeader
        {
            RSSNumber = RSSNumber,
            ChannelLink = ChannelLink,
            WebMaster = WebMaster,
            ManagingEditor = ManagingEditor,
            LogoTitle = LogoTitle,
            LogoUrl = LogoUrl,
            LogoLink = LogoLink,
            ITunesName = ITunesName,
            ITunesEmail = ITunesEmail,
            ITunesCategory = ITunesCategory,
            ITunesSubCategory = ITunesSubCategory,
            ITunesCategory2 = ITunesCategory2,
            ITunesSubCategory2 = ITunesSubCategory2,
            ITunesKeywords = ITunesKeywords,
            ITunesExplicit = ITunesExplicit,
            ITunesImage = ITunesImage,
            AtomLink = AtomLink,
            PubDate = PubDate,
            Title = Title,
            Author = Author,
            Description = Description,
            Subtitle = Subtitle,
            LastBuildDate = LastBuildDate
        };
        }
    }
}
