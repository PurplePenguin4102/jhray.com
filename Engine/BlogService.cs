using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jhray.com.Models.GemMasterViewModels;
using jhray.com.Models.Gems;
using jhray.com.Database.Entities;
using jhray.com.Database;
using jhray.com.Utils;
using System.Text.RegularExpressions;

namespace jhray.com.Engine
{
    public class BlogService
    {
        public void AddNewBlogPost(BlogGem blog, ChilledDbContext context, ChilledUser user)
        {
            var rss = context.RSSHeaders.FirstOrDefault(r => r.RSSNumber == blog.FeedId);
            context.BlogPosts.Add(new BlogPost()
            {
                Hashtags = GetHashTags(blog.Hashtags),
                MarkdownContent = blog.MarkdownContent,
                Published = blog.Published,
                RSSHeaderId = rss.Id,
                SubTitle = blog.Subtitle,
                Title = blog.Title,
                Author = user
            });
            context.SaveChanges();
        }

        public void EditBlogPost(BlogGem blog, ChilledDbContext context)
        {
            var oldData = context.BlogPosts.FirstOrDefault(b => b.Id == int.Parse(blog.Id));
            if (oldData != null)
            {
                var rss = context.RSSHeaders.FirstOrDefault(r => r.RSSNumber == blog.FeedId);
                oldData.Hashtags = GetHashTags(blog.Hashtags);
                oldData.MarkdownContent = blog.MarkdownContent;
                oldData.Published = blog.Published;
                oldData.RSSHeaderId = rss.Id;
                oldData.SubTitle = blog.Subtitle;
                oldData.Title = blog.Title;
                context.SaveChanges();
            }
        }

        private string GetHashTags(List<string> lst) => lst == null || lst.Contains(null) ? "" : Regex.Replace(lst.ToString(','), @"\s+", "");
    }
}
