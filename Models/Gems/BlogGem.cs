using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Markdig;
using jhray.com.Database.Entities;

namespace jhray.com.Models.Gems
{
    public class BlogGem : IGem
    {
        public BlogGem() { }
        public BlogGem(BlogPost blog)
        {
            Title = blog.Title;
            Id = blog.Id.ToString();
            FeedId = blog.RSSHeaderId;
            MarkdownContent = blog.MarkdownContent;
            Pictures = blog.Pictures.Select(p => new Uri(p.Picture.Location)).ToList();
            Published = blog.Published;
            AuthorName = blog.Author.UserName;
            Hashtags = blog.Hashtags.Split(',').ToList();
            Subtitle = blog.SubTitle;
        }

        public GemType Type { get; set; }
        public string Title { get; set; }
        public string Id { get; set; }
        public int FeedId { get; set; }
        public string MarkdownContent { get; set; }
        public string HtmlContent { get => Markdown.ToHtml(MarkdownContent); }
        public List<Uri> Pictures { get; set; }
        public DateTime Published { get; set; }
        public string AuthorName { get; set; }
        public List<string> Hashtags { get; set; }
        public string Subtitle { get; set; }
    }
}
