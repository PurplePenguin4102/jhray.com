using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Text;

namespace jhray.com.Engine
{
    public class RSSFeed
    {

        public string ReadFromFolderContents()
        {
            var directories = Directory.GetDirectories(@"/home/penguin/podcast/");
            var feedBuilder = new StringBuilder();
            
            using (var xml = XmlWriter.Create(feedBuilder))
            {
                var openTags = 0;
                xml.WriteStartDocument();
                openTags += WriteRSSHeader(xml);

                xml.WriteStartElement("channel");
                openTags++;

                openTags += WriteChannelHeader(xml);
                openTags += WriteLogo(xml);
                openTags += WriteItunesStuff(xml);
                openTags += WriteAtomFeedInfo(xml);
                openTags += WritePodcastHeader(xml);
                
                foreach(var directory in directories)
                {
                    var lines = File.ReadAllLines(Path.Combine(directory, "Metadata.txt"));
                    var meta = lines.ToDictionary(
                        lin => lin.Substring(0, lin.IndexOf(":")), 
                        lin => lin.Substring(lin.IndexOf(":") + 1).Trim());
                    xml.WriteStartElement("Item");
                    xml.WriteElementString("title", meta["title"]);
                    xml.WriteElementString("description", meta["description"]);
                    xml.WriteElementString("itunes", "summary", null, meta["description"]);
                    xml.WriteElementString("itunes", "subtitle", null, meta["short_subtitle"]);
                    xml.WriteStartElement("enclosure");
                    xml.WriteAttributeString("url", meta["location"]);
                    xml.WriteAttributeString("type", "audio/mpeg");
                    xml.WriteAttributeString("length", "1");
                    xml.WriteEndElement();
                    xml.WriteElementString("guid", meta["location"]);
                    xml.WriteElementString("itunes", "duration", null, meta["itunes_duration"]);
                    xml.WriteElementString("pubDate", meta["pubDate"]);
                    xml.WriteEndElement();
                }

                while (openTags > 0)
                {
                    xml.WriteEndElement();
                    openTags--;
                }
            }

            return feedBuilder.ToString();
        }

        private int WritePodcastHeader(XmlWriter xml)
        {
            xml.WriteElementString("pubDate", "Mon, 23 April 2018 00:00:00 AEST");
            xml.WriteElementString("title", "Chilled E-Sports");
            xml.WriteElementString("itunes", "author", null, "Eugene and Joey");
            xml.WriteElementString("description", "Doing deep dives on the EU and NA Esports leagues of Heroes of the Storm. Discussing strategies and state-of-the-game ideas on how to get elite wins and dominate Silver & Gold leagues in all major Blizzard titles. Also talks about crypto currency, blockchain and technology from the point of view of engineering and comp sci.");
            xml.WriteElementString("itunes", "summary", null, "Doing deep dives on the EU and NA Esports leagues of Heroes of the Storm. Discussing strategies and state-of-the-game ideas on how to get elite wins and dominate Silver & Gold leagues in all major Blizzard titles. Also talks about crypto currency, blockchain and technology from the point of view of engineering and comp sci.");
            xml.WriteElementString("itunes", "subtitle", null, "A chill podcast about Blizzard games that you can drink beers to. Also talks about crypto");
            xml.WriteElementString("lastBuildDate", "Mon, 23 Apr 2018 00:00:00 AEST");
            return 0;
        }

        private int WriteAtomFeedInfo(XmlWriter xml)
        {
            xml.WriteStartElement("atom", "link", null);
            xml.WriteAttributeString("href", "http://jhray.com/home/GetRSSFeed");
            xml.WriteAttributeString("rel", "self");
            xml.WriteAttributeString("type", "application/rss+xml");
            xml.WriteEndElement();
            return 0;
        }

        private int WriteItunesStuff(XmlWriter xml)
        {
            xml.WriteStartElement("itunes", "owner", null);
            xml.WriteElementString("itunes", "name", null, "Eugene Cafun");
            xml.WriteElementString("itunes", "email", null, "Eugene.Cafun@gmail.com");
            xml.WriteEndElement();

            xml.WriteStartElement("itunes", "category", null);
            xml.WriteAttributeString("text","Games &amp; Hobbies");
            xml.WriteStartElement("itunes", "category", null);
            xml.WriteAttributeString("text", "Video Games");
            xml.WriteEndElement();
            xml.WriteEndElement();

            xml.WriteStartElement("itunes", "category", null);
            xml.WriteAttributeString("text", "Technology");
            xml.WriteStartElement("itunes", "category", null);
            xml.WriteAttributeString("text", "Tech News");
            xml.WriteEndElement();
            xml.WriteEndElement();

            xml.WriteElementString("itunes", "keywords", null, "Hots, Overwatch, Blizzard, Games, ESports, Blockchain, Crypto, Crypto Currency, Relaxed");
            xml.WriteElementString("itunes", "explicit", null, "yes");

            xml.WriteStartElement("itunes", "image", null);
            xml.WriteAttributeString("href", "http://jhray.com/images/chilled_beer.jpg");
            xml.WriteEndElement();

            return 0;
        }

        private int WriteRSSHeader(XmlWriter xml)
        {
            xml.WriteStartElement("rss");
            xml.WriteAttributeString("xmlns", "atom", null, "http://www.w3.org/2005/Atom");
            xml.WriteAttributeString("xmlns", "itunes", null, "http://www.itunes.com/dtds/podcast-1.0.dtd");
            xml.WriteAttributeString("version", "2.0");
            return 1;
        }

        private int WriteChannelHeader(XmlWriter xml)
        {
            xml.WriteElementString("link", "http://jhray.com");
            xml.WriteElementString("language", "en-us");
            xml.WriteElementString("copyright", "&copy; 2018");
            xml.WriteElementString("webMaster", "joseph.h.ray@protonmail.com (Joey Ray)");
            xml.WriteElementString("managingEditor", "joseph.h.ray@protonmail.com (Joey Ray)");

            return 0;
        }

        private int WriteLogo(XmlWriter xml)
        {
            xml.WriteStartElement("image");
            xml.WriteElementString("url", "http://jhray.com/images/chilled_beer.jpg");
            xml.WriteElementString("title", "Chilled E-Sports");
            xml.WriteElementString("link", "http://jhray.com");
            xml.WriteEndElement();
            return 0;
        }
    }
}
