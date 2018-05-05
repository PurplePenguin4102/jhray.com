using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Text;
using static jhray.com.Utils.Utils;

namespace jhray.com.Engine
{

    public class RSSFeed
    {
        private Dictionary<string, string> _feedMeta;
        public string ReadFromFolderContents(string podcastDirectory)
        {
            var directories = Directory.GetDirectories(podcastDirectory).OrderBy(a => a);
            _feedMeta = GetLinesOfMetadata(Path.Combine(podcastDirectory, "Metadata.txt"));
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
                
                foreach(var directory in directories)
                {
                    WriteItemInfoFromDirectory(xml, directory);
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

        private void WriteItemInfoFromDirectory(XmlWriter xml, string directory)
        {
            var meta = GetLinesOfMetadata(Path.Combine(directory, "Metadata.txt"));
            xml.WriteStartElement("item");
            xml.WriteElementString("title", meta["title"]);
            xml.WriteElementString("description", meta["description"]);
            xml.WriteElementString("itunes", "summary", null, meta["description"]);
            xml.WriteElementString("itunes", "subtitle", null, meta["short_subtitle"]);
            xml.WriteStartElement("enclosure");
            xml.WriteAttributeString("url", meta["location"]);
            xml.WriteAttributeString("type", "audio/mpeg");
            xml.WriteAttributeString("length", meta["lengthinbytes"]);
            xml.WriteEndElement();
            xml.WriteElementString("guid", meta["location"]);
            xml.WriteElementString("itunes", "duration", null, meta["itunes_duration"]);
            xml.WriteElementString("pubDate", meta["pubDate"]);
            xml.WriteEndElement();
        }
    }
}
