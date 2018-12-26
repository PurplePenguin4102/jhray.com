using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using jhray.com.Models.GemMasterViewModels;
using static jhray.com.Utils.Utils;
using System.Globalization;
using jhray.com.Database;
using jhray.com.Database.Entities;
using System.Transactions;

namespace jhray.com.Engine
{

    public class RSSFeed
    {
        private readonly string DateFormat = "ddd, dd MMM yyyy 21:00:00 +1000";
        private string podcastDbDirectory;
        private string urlPath;

        public RSSFeed(Paths paths)
        {
            podcastDbDirectory = paths.PodcastDbDirectory;
            urlPath = paths.URLPath;
        }

        public async Task<bool> CreateNewEpisode(PodcastMetadata podCast, ChilledDbContext context, string currentUserId)
        {
            podCast.Title = podCast.Title.Trim();
            var filename = Regex.Replace(podCast.PodcastFile.FileName, " ", "_");
            using (var txn = context.Database.BeginTransaction())
            {
                var podcastEntity = new Gem()
                {
                    Title = podCast.Title,
                    SummaryText = new string(podCast.Description.Take(250).ToArray()),
                    GemType = GemType.Podcast,

                    CreatedById = currentUserId,
                    PodcastData = new Podcast()
                    {
                        Description = podCast.Description,
                        ShortDescription = podCast.ShortDescription,
                        LengthInBytes = podCast.PodcastFile.Length,
                        ItunesDuration = podCast.ItunesDuration,
                        PubDate = podCast.PubDate,
                        FeedId = podCast.FeedId
                    }
                };

                context.Gems.Add(podcastEntity);
                context.SaveChanges();

                podcastEntity.PodcastData.Location = $"http://{urlPath}/uploads/podcasts/{podcastEntity.Id}/{filename}";

                podcastEntity.FilePath = Path.Combine(podcastDbDirectory, podcastEntity.Id.ToString());
                Directory.CreateDirectory(podcastEntity.FilePath);
                using (var stream = new FileStream(Path.Combine(podcastEntity.FilePath, filename), FileMode.CreateNew))
                {
                    await podCast.PodcastFile.CopyToAsync(stream);
                    stream.Flush();
                }
                podcastEntity.FilePath = Path.Combine(podcastEntity.FilePath, filename);
                
                context.SaveChanges();
                txn.Commit();
            }
            return true;
        }

        private Dictionary<string, string> _feedMeta;

        public string ReadFromFolderContents(ChilledDbContext context, int id)
        {
            _feedMeta = GetLinesOfMetadata(Path.Combine(podcastDbDirectory, $"Metadata_{id}.txt"));
            var feedBuilder = new MemoryStream();
            
            using (var xml = XmlWriter.Create(feedBuilder, new XmlWriterSettings() { Encoding = Encoding.UTF8}))
            {
                xml.WriteStartDocument();
                WriteRSSHeader(xml);
                WriteChannelHeader(xml);
                WriteLogo(xml);
                WriteItunesStuff(xml);
                WriteAtomFeedInfo(xml);
                WritePodcastHeader(xml);

                foreach (var podcast in context.Podcasts.Where(pod => pod.FeedId == id).OrderByDescending(p => p.PubDate))
                {
                    context.Entry(podcast).Reference(p => p.GemData).Load();
                    WritePodcast(xml, podcast);
                }
                xml.WriteEndDocument();
            }
            feedBuilder.Position = 0;
            using (var reader = new StreamReader(feedBuilder))
            {
                return reader.ReadToEnd();
            }
        }

        private void WriteRSSHeader(XmlWriter xml)
        {
            xml.WriteStartElement("rss");
            xml.WriteAttributeString("xmlns", "atom", null, "http://www.w3.org/2005/Atom");
            xml.WriteAttributeString("xmlns", "itunes", null, "http://www.itunes.com/dtds/podcast-1.0.dtd");
            xml.WriteAttributeString("version", "2.0");
        }

        private void WriteChannelHeader(XmlWriter xml)
        {
            xml.WriteStartElement("channel");
            xml.WriteElementString("link", _feedMeta["channellink"]);
            xml.WriteElementString("language", "en-us");
            xml.WriteElementString("copyright", "&#169; 2018");
            xml.WriteElementString("webMaster", _feedMeta["webmaster"]);
            xml.WriteElementString("managingEditor", _feedMeta["managingeditor"]);
        }

        private void WriteLogo(XmlWriter xml)
        {
            xml.WriteStartElement("image");
            xml.WriteElementString("url", _feedMeta["logourl"]);
            xml.WriteElementString("title", _feedMeta["logotitle"]);
            xml.WriteElementString("link", _feedMeta["logolink"]);
            xml.WriteEndElement();
        }

        private void WriteItunesStuff(XmlWriter xml)
        {
            xml.WriteStartElement("itunes", "owner", null);
            xml.WriteElementString("itunes", "name", null, _feedMeta["itunesname"]);
            xml.WriteElementString("itunes", "email", null, _feedMeta["itunesemail"]);
            xml.WriteEndElement();

            xml.WriteStartElement("itunes", "category", null);
            xml.WriteAttributeString("text", _feedMeta["itunescategory"]);
            xml.WriteStartElement("itunes", "category", null);
            xml.WriteAttributeString("text", _feedMeta["itunessubcategory"]);
            xml.WriteEndElement();
            xml.WriteEndElement();

            xml.WriteStartElement("itunes", "category", null);
            xml.WriteAttributeString("text", _feedMeta["itunescategory2"]);
            xml.WriteStartElement("itunes", "category", null);
            xml.WriteAttributeString("text", _feedMeta["itunessubcategory2"]);
            xml.WriteEndElement();
            xml.WriteEndElement();

            xml.WriteElementString("itunes", "keywords", null, _feedMeta["ituneskeywords"]);
            xml.WriteElementString("itunes", "explicit", null, _feedMeta["itunesexplicit"]);

            xml.WriteStartElement("itunes", "image", null);
            xml.WriteAttributeString("href", _feedMeta["itunesimage"]);
            xml.WriteEndElement();
        }

        private void WriteAtomFeedInfo(XmlWriter xml)
        {
            xml.WriteStartElement("atom", "link", null);
            xml.WriteAttributeString("href", _feedMeta["atomlink"]);
            xml.WriteAttributeString("rel", "self");
            xml.WriteAttributeString("type", "application/rss+xml");
            xml.WriteEndElement();
        }

        private void WritePodcastHeader(XmlWriter xml)
        {
            xml.WriteElementString("pubDate", _feedMeta["pubdate"]);
            xml.WriteElementString("title", _feedMeta["title"]);
            xml.WriteElementString("itunes", "author", null, _feedMeta["author"]);
            xml.WriteElementString("description", _feedMeta["description"]);
            xml.WriteElementString("itunes", "summary", null, _feedMeta["description"]);
            xml.WriteElementString("itunes", "subtitle", null, _feedMeta["subtitle"]);
            xml.WriteElementString("lastBuildDate", _feedMeta["lastbuilddate"]);
        }

        private void WritePodcast(XmlWriter xml, Podcast podcast)
        {
            xml.WriteStartElement("item");
            xml.WriteElementString("title", podcast.GemData.Title);
            xml.WriteElementString("description", podcast.Description);
            xml.WriteElementString("itunes", "summary", null, podcast.Description);
            xml.WriteElementString("itunes", "subtitle", null, podcast.ShortDescription);
            xml.WriteStartElement("enclosure");
            xml.WriteAttributeString("url", podcast.Location);
            xml.WriteAttributeString("type", "audio/mpeg");
            xml.WriteAttributeString("length", podcast.LengthInBytes.ToString());
            xml.WriteEndElement();
            xml.WriteElementString("guid", podcast.Location);
            xml.WriteElementString("itunes", "duration", null, podcast.ItunesDuration);
            xml.WriteElementString("pubDate", podcast.PubDate.ToString(DateFormat));
            xml.WriteEndElement();
        }
    }
}
