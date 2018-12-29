using jhray.com.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static jhray.com.Utils.Utils;

namespace jhray.com.Database
{
    public class SeedDatabase
    {
        public static async Task<IdentityResult> Go(ChilledDbContext context, Paths paths, UserManager<ChilledUser> userManager, RoleManager<IdentityRole> roleManager, string email)
        {
            if (!await roleManager.RoleExistsAsync("SuperGenius"))
            {
                await roleManager.CreateAsync(new IdentityRole("SuperGenius"));
            }

            var user = await userManager.FindByEmailAsync(email);
            var role = await roleManager.FindByNameAsync("SuperGenius");
            if (user != null && !await userManager.IsInRoleAsync(user, role.Name))
            {
                var result = await userManager.AddToRoleAsync(user, role.Name);
                return result;
            }
            await context.SaveChangesAsync();
            
            return IdentityResult.Failed();
        }

        public static void FillOutPodcastMeta(ChilledDbContext context, Paths paths)
        {
            var header0 = context.RSSHeaders.FirstOrDefault(rss => rss.RSSNumber == 0);
            if (header0 == null)
            {
                var _feedMeta0 = GetLinesOfMetadata(Path.Combine(paths.PodcastDbDirectory, $"Metadata_0.txt"));
                var meta0 = new RSSHeader()
                {
                    RSSNumber = 0,
                    ChannelLink = _feedMeta0["channellink"],
                    WebMaster = _feedMeta0["webmaster"],
                    ManagingEditor = _feedMeta0["managingeditor"],
                    LogoTitle = _feedMeta0["logotitle"],
                    LogoUrl = _feedMeta0["logourl"],
                    LogoLink = _feedMeta0["logolink"],
                    ITunesName = _feedMeta0["itunesname"],
                    ITunesEmail = _feedMeta0["itunesemail"],
                    ITunesCategory = _feedMeta0["itunescategory"],
                    ITunesSubCategory = _feedMeta0["itunessubcategory"],
                    ITunesCategory2 = _feedMeta0["itunescategory2"],
                    ITunesSubCategory2 = _feedMeta0["itunessubcategory2"],
                    ITunesKeywords = _feedMeta0["ituneskeywords"],
                    ITunesExplicit = _feedMeta0["itunesexplicit"],
                    ITunesImage = _feedMeta0["itunesimage"],
                    AtomLink = _feedMeta0["atomlink"],
                    PubDate = _feedMeta0["pubdate"],
                    Title = _feedMeta0["title"],
                    Author = _feedMeta0["author"],
                    Description = _feedMeta0["description"],
                    Subtitle = _feedMeta0["subtitle"],
                    LastBuildDate = _feedMeta0["lastbuilddate"]
                };
                context.RSSHeaders.Add(meta0);
                context.SaveChanges();
            }
            var header1 = context.RSSHeaders.FirstOrDefault(rss => rss.RSSNumber == 1);
            {
                var _feedMeta1 = GetLinesOfMetadata(Path.Combine(paths.PodcastDbDirectory, $"Metadata_1.txt"));
                var meta1 = new RSSHeader()
                {
                    RSSNumber = 1,
                    ChannelLink = _feedMeta1["channellink"],
                    WebMaster = _feedMeta1["webmaster"],
                    ManagingEditor = _feedMeta1["managingeditor"],
                    LogoTitle = _feedMeta1["logotitle"],
                    LogoUrl = _feedMeta1["logourl"],
                    LogoLink = _feedMeta1["logolink"],
                    ITunesName = _feedMeta1["itunesname"],
                    ITunesEmail = _feedMeta1["itunesemail"],
                    ITunesCategory = _feedMeta1["itunescategory"],
                    ITunesSubCategory = _feedMeta1["itunessubcategory"],
                    ITunesCategory2 = _feedMeta1["itunescategory2"],
                    ITunesSubCategory2 = _feedMeta1["itunessubcategory2"],
                    ITunesKeywords = _feedMeta1["ituneskeywords"],
                    ITunesExplicit = _feedMeta1["itunesexplicit"],
                    ITunesImage = _feedMeta1["itunesimage"],
                    AtomLink = _feedMeta1["atomlink"],
                    PubDate = _feedMeta1["pubdate"],
                    Title = _feedMeta1["title"],
                    Author = _feedMeta1["author"],
                    Description = _feedMeta1["description"],
                    Subtitle = _feedMeta1["subtitle"],
                    LastBuildDate = _feedMeta1["lastbuilddate"]
                };
                context.RSSHeaders.Add(meta1);
                context.SaveChanges();
            }
        }
    }
}
