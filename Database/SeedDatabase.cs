using jhray.com.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
            if (!context.Podcasts.Any())
            {
                // EP 1
                using (var txn = context.Database.BeginTransaction())
                {
                    var podcastEntity = new Gem()
                    {
                        Title = "Chilled E-Sports Episode 1",
                        SummaryText = new String("Podcasters Joey and Eugene introduce themselves. We talk about Week 8 of Na and EU Phase 1. Also talk about Dash and WeAreChange.org partnership.".Take(250).ToArray()),
                        GemType = GemType.Podcast,

                        CreatedById = user.Id,
                        PodcastData = new Podcast()
                        {
                            Description = "Podcasters Joey and Eugene introduce themselves. We talk about Week 8 of Na and EU Phase 1. Also talk about Dash and WeAreChange.org partnership.",
                            ShortDescription = "Week 8 Phase 1 E-Sports plus Dash cryptocurrency",
                            LengthInBytes = 66337043,
                            ItunesDuration = "1:01:23",
                            PubDate = DateTimeOffset.Parse("Sun, 15 Apr 2018 21:00:00 +1000"),
                        }
                    };

                    context.Gems.Add(podcastEntity);
                    context.SaveChanges();

                    podcastEntity.FilePath = Path.Combine(paths.PodcastDbDirectory, podcastEntity.Id.ToString());
                    Directory.CreateDirectory(podcastEntity.FilePath);

                    podcastEntity.FilePath = Path.Combine(podcastEntity.FilePath, "Chilled_Podcast_Episode_1.mp3");
                    podcastEntity.PodcastData.Location = $"http://{paths.URLPath}/uploads/{podcastEntity.Id}/Chilled_Podcast_Episode_1.mp3";
                    context.SaveChanges();
                    txn.Commit();
                }

                // EP 2
                using (var txn = context.Database.BeginTransaction())
                {
                    var podcastEntity = new Gem()
                    {
                        Title = "Chilled E-Sports Episode 2",
                        SummaryText = new String("Eugene's rig has died and we are in despair. We also talk about placing in ranked overwatch and seeded games in HoTS. Week 10 Phase 1 NA and EU games with Fenix counterplay strats. Verge has partnered with interesting business entities and good stuff ensues".Take(250).ToArray()),
                        GemType = GemType.Podcast,

                        CreatedById = user.Id,
                        PodcastData = new Podcast()
                        {
                            Description = "Eugene's rig has died and we are in despair. We also talk about placing in ranked overwatch and seeded games in HoTS. Week 10 Phase 1 NA and EU games with Fenix counterplay strats. Verge has partnered with interesting business entities and good stuff ensues",
                            ShortDescription = "NA and EU Phase 10 Week 1, Fenix counterplay discussion. Verge new partnership",
                            LengthInBytes = 90079488,
                            ItunesDuration = "1:27:36",
                            PubDate = DateTimeOffset.Parse("Sun, 22 Apr 2018 21:00:00 +1000"),
                        }
                    };

                    context.Gems.Add(podcastEntity);
                    context.SaveChanges();

                    podcastEntity.FilePath = Path.Combine(paths.PodcastDbDirectory, podcastEntity.Id.ToString());
                    Directory.CreateDirectory(podcastEntity.FilePath);

                    podcastEntity.FilePath = Path.Combine(podcastEntity.FilePath, "Chilled_Podcast_Episode_2.mp3");
                    podcastEntity.PodcastData.Location = $"http://{paths.URLPath}/uploads/{podcastEntity.Id}/Chilled_Podcast_Episode_2.mp3";
                    context.SaveChanges();
                    txn.Commit();
                }
                // EP 3
                using (var txn = context.Database.BeginTransaction())
                {
                    var podcastEntity = new Gem()
                    {
                        Title = "Chilled E-Sports Episode 3",
                        SummaryText = new String("Eugene managed to fix his computer with a super weird trick! First week of Deckerd Cain being live. The HGC Playoffs are on and it's really exciting in the NA sphere with hero TLV action. Crypto-talk we talk about Snoop Dogg and Ripple and AWS templates".Take(250).ToArray()),
                        GemType = GemType.Podcast,

                        CreatedById = user.Id,
                        PodcastData = new Podcast()
                        {
                            Description = "Eugene managed to fix his computer with a super weird trick! First week of Deckerd Cain being live. The HGC Playoffs are on and it's really exciting in the NA sphere with hero TLV action. Crypto-talk we talk about Snoop Dogg and Ripple and AWS templates",
                            ShortDescription = "NA and EU PLAYOFFS, Deckerd Cain discussion. AWS Ethereum templates ",
                            LengthInBytes = 106849698,
                            ItunesDuration = "1:42:31",
                            PubDate = DateTimeOffset.Parse("Sun, 29 Apr 2018 21:00:00 +1000"),
                        }
                    };

                    context.Gems.Add(podcastEntity);
                    context.SaveChanges();

                    podcastEntity.FilePath = Path.Combine(paths.PodcastDbDirectory, podcastEntity.Id.ToString());
                    Directory.CreateDirectory(podcastEntity.FilePath);

                    podcastEntity.FilePath = Path.Combine(podcastEntity.FilePath, "Chilled_Podcast_Episode_3.mp3");
                    podcastEntity.PodcastData.Location = $"http://{paths.URLPath}/uploads/{podcastEntity.Id}/Chilled_Podcast_Episode_3.mp3";
                    context.SaveChanges();
                    txn.Commit();
                }

                // EP 4
                using (var txn = context.Database.BeginTransaction())
                {
                    var podcastEntity = new Gem()
                    {
                        Title = "Chilled E-Sports Episode 4",
                        SummaryText = new String("A new Podcaster has entered the arena! Kristian introduces himself and talks about Overwatch. Joey's an internet grandpa and doesn't know about Twitch.tv. HCT is going off the chain with Cube lock vs Quest Rogue. Joey discusses HGC Europe and Zealots dream run. Kristian gives our first overwatch analysis. Crypto talk we talk about social media monetization in Minds.com and Holochain, a new player in the market.".Take(250).ToArray()),
                        GemType = GemType.Podcast,

                        CreatedById = user.Id,
                        PodcastData = new Podcast()
                        {
                            Description = "A new Podcaster has entered the arena! Kristian introduces himself and talks about Overwatch. Joey's an internet grandpa and doesn't know about Twitch.tv. HCT is going off the chain with Cube lock vs Quest Rogue. Joey discusses HGC Europe and Zealots dream run. Kristian gives our first overwatch analysis. Crypto talk we talk about social media monetization in Minds.com and Holochain, a new player in the market.",
                            ShortDescription = "EU PLAYOFFS Zealots dream run, HCT cube lock vs questing rogue. Overwatch analysis. Minds.com ethereum integration ",
                            LengthInBytes = 74343896,
                            ItunesDuration = "1:21:25",
                            PubDate = DateTimeOffset.Parse("Sun, 06 May 2018 21:00:00 +1000"),
                        }
                    };

                    context.Gems.Add(podcastEntity);
                    context.SaveChanges();

                    podcastEntity.FilePath = Path.Combine(paths.PodcastDbDirectory, podcastEntity.Id.ToString());
                    Directory.CreateDirectory(podcastEntity.FilePath);

                    podcastEntity.FilePath = Path.Combine(podcastEntity.FilePath, "Chilled_Podcast_Episode_4.mp3");
                    podcastEntity.PodcastData.Location = $"http://{paths.URLPath}/uploads/{podcastEntity.Id}/Chilled_Podcast_Episode_4.mp3";
                    context.SaveChanges();
                    txn.Commit();
                }

                // EP 5
                using (var txn = context.Database.BeginTransaction())
                {
                    var podcastEntity = new Gem()
                    {
                        Title = "Chilled E-Sports Episode 5",
                        SummaryText = new String("Cube Lock counterplay and Even Dreams in HCT. Kristian is starting to build some beastly Hearthstone decks and Eugene remains UNCONVINCED that Odd Paladin is any good. EU Crucible is a harsh mistress for Worst Positioning and Kristian shares elite pro strats for winning on Temple of Anubis. IBM issues challenge to solve water shortage with Ethereum supply verification".Take(250).ToArray()),
                        GemType = GemType.Podcast,

                        CreatedById = user.Id,
                        PodcastData = new Podcast()
                        {
                            Description = "Cube Lock counterplay and Even Dreams in HCT. Kristian is starting to build some beastly Hearthstone decks and Eugene remains UNCONVINCED that Odd Paladin is any good. EU Crucible is a harsh mistress for Worst Positioning and Kristian shares elite pro strats for winning on Temple of Anubis. IBM issues challenge to solve water shortage with Ethereum supply verification",
                            ShortDescription = "EU crucible, HCT cube lock pwnage. Overwatch Boston DESTROYED. IBM and Ethereum",
                            LengthInBytes = 82779147,
                            ItunesDuration = "1:32:04",
                            PubDate = DateTimeOffset.Parse("Sun, 13 May 2018 21:00:00 +1000"),
                        }
                    };

                    context.Gems.Add(podcastEntity);
                    context.SaveChanges();

                    podcastEntity.FilePath = Path.Combine(paths.PodcastDbDirectory, podcastEntity.Id.ToString());
                    Directory.CreateDirectory(podcastEntity.FilePath);

                    podcastEntity.FilePath = Path.Combine(podcastEntity.FilePath, "Chilled_Podcast_Episode_5.mp3");
                    podcastEntity.PodcastData.Location = $"http://{paths.URLPath}/uploads/{podcastEntity.Id}/Chilled_Podcast_Episode_5.mp3";
                    context.SaveChanges();
                    txn.Commit();
                }

                // EP 6
                using (var txn = context.Database.BeginTransaction())
                {
                    var podcastEntity = new Gem()
                    {
                        Title = "Chilled E-Sports Episode 6",
                        SummaryText = new String("HCT is going off as Eugene finds out that his favourite deck is actually also viable. NERFS EVERYWHERE. Shudderwock Shaman makes waves as the dominance of Paladin/Warlock/Rogue is clear. Boston Uprising met against Houston Outlaws in Overwatch with unexpected results. Heroes of the Dorm is in full SCHWING with some great college games!! Smallest computer can fit on your finger and play Kings Quest".Take(250).ToArray()),
                        GemType = GemType.Podcast,

                        CreatedById = user.Id,
                        PodcastData = new Podcast()
                        {
                            Description = "HCT is going off as Eugene finds out that his favourite deck is actually also viable. NERFS EVERYWHERE. Shudderwock Shaman makes waves as the dominance of Paladin/Warlock/Rogue is clear. Boston Uprising met against Houston Outlaws in Overwatch with unexpected results. Heroes of the Dorm is in full SCHWING with some great college games!! Smallest computer can fit on your finger and play Kings Quest",
                            ShortDescription = "Heroes of the Dorm, HCT Australia Shudderwock. Overwatch Boston DESTROYED. IBM and Ethereum",
                            LengthInBytes = 1703936,
                            ItunesDuration = "1:58:04",
                            PubDate = DateTimeOffset.Parse("Sun, 20 May 2018 21:00:00 +1000"),
                        }
                    };

                    context.Gems.Add(podcastEntity);
                    context.SaveChanges();

                    podcastEntity.FilePath = Path.Combine(paths.PodcastDbDirectory, podcastEntity.Id.ToString());
                    Directory.CreateDirectory(podcastEntity.FilePath);

                    podcastEntity.FilePath = Path.Combine(podcastEntity.FilePath, "Chilled_Podcast_Episode_6.mp3");
                    podcastEntity.PodcastData.Location = $"http://{paths.URLPath}/uploads/{podcastEntity.Id}/Chilled_Podcast_Episode_6.mp3";
                    context.SaveChanges();
                    txn.Commit();
                }

                // EP 7
                using (var txn = context.Database.BeginTransaction())
                {
                    var podcastEntity = new Gem()
                    {
                        Title = "Chilled E-Sports Episode 7",
                        SummaryText = new String("Not much going on in the ESports scene for heroes but we still managed to get in a bunch of good games. Delivery truck ruins everything a few minutes in. Eugene talks about mindset of being in a losing streak and how to break it also benefits of aim training. Kristian talks about Hanzo and how to get the most out of anime power. Talk about meta strategy with camps in HoTS. Crypto Kitties are generous!".Take(250).ToArray()),
                        GemType = GemType.Podcast,

                        CreatedById = user.Id,
                        PodcastData = new Podcast()
                        {
                            Description = "Not much going on in the ESports scene for heroes but we still managed to get in a bunch of good games. Delivery truck ruins everything a few minutes in. Eugene talks about mindset of being in a losing streak and how to break it also benefits of aim training. Kristian talks about Hanzo and how to get the most out of anime power. Talk about meta strategy with camps in HoTS. Crypto Kitties are generous!",
                            ShortDescription = "NEED MORE ESPORTS BLIZZZ!!!!",
                            LengthInBytes = 94820992,
                            ItunesDuration = "1:41:38",
                            PubDate = DateTimeOffset.Parse("Sun, 27 May 2018 21:00:00 +1000"),
                        }
                    };

                    context.Gems.Add(podcastEntity);
                    context.SaveChanges();

                    podcastEntity.FilePath = Path.Combine(paths.PodcastDbDirectory, podcastEntity.Id.ToString());
                    Directory.CreateDirectory(podcastEntity.FilePath);

                    podcastEntity.FilePath = Path.Combine(podcastEntity.FilePath, "Chilled_Podcast_Episode_7.mp3");
                    podcastEntity.PodcastData.Location = $"http://{paths.URLPath}/uploads/{podcastEntity.Id}/Chilled_Podcast_Episode_7.mp3";
                    context.SaveChanges();
                    txn.Commit();
                }

                // EP 8
                using (var txn = context.Database.BeginTransaction())
                {
                    var podcastEntity = new Gem()
                    {
                        Title = "Chilled E-Sports Episode 8",
                        SummaryText = new String("DREAMHACK AUSTIN!!! Eugene and Kristian hold down the fort back to back. Eugene tells it like it is in the Hearthstone league. Murloc Paladin is BACK. Warlock had merely a setback and has returned stronger than ever. Kristian schools us on where to put your big German ass when you're doing Payload escort and more. Eugene now confirmed Overwatch fanatic for NYXL".Take(250).ToArray()),
                        GemType = GemType.Podcast,

                        CreatedById = user.Id,
                        PodcastData = new Podcast()
                        {
                            Description = "DREAMHACK AUSTIN!!! Eugene and Kristian hold down the fort back to back. Eugene tells it like it is in the Hearthstone league. Murloc Paladin is BACK. Warlock had merely a setback and has returned stronger than ever. Kristian schools us on where to put your big German ass when you're doing Payload escort and more. Eugene now confirmed Overwatch fanatic for NYXL",
                            ShortDescription = "Dreamhack Austin Roundup and Overwatch strategies",
                            LengthInBytes = 94820992,
                            ItunesDuration = "1:08:58",
                            PubDate = DateTimeOffset.Parse("Sun, 03 Jun 2018 21:00:00 +1000"),
                        }
                    };

                    context.Gems.Add(podcastEntity);
                    context.SaveChanges();

                    podcastEntity.FilePath = Path.Combine(paths.PodcastDbDirectory, podcastEntity.Id.ToString());
                    Directory.CreateDirectory(podcastEntity.FilePath);

                    podcastEntity.FilePath = Path.Combine(podcastEntity.FilePath, "Chilled_Podcast_Episode_8.mp3");
                    podcastEntity.PodcastData.Location = $"http://{paths.URLPath}/uploads/{podcastEntity.Id}/Chilled_Podcast_Episode_8.mp3";
                    context.SaveChanges();
                    txn.Commit();
                }

                // EP 9
                using (var txn = context.Database.BeginTransaction())
                {
                    var podcastEntity = new Gem()
                    {
                        Title = "Chilled E-Sports Episode 9",
                        SummaryText = new String("Kristian drops the ball and leaves it to Eugene and Joey. Joey's riding the rails and the two talk about the latest Blizzard news. New games in the making, new champion released and Battle for Azeroth is close by. First week of the Brawl leads to BLOOD IN THE STREETS. Can you mount in a teamfight?".Take(250).ToArray()),
                        GemType = GemType.Podcast,

                        CreatedById = user.Id,
                        PodcastData = new Podcast()
                        {
                            Description = "Kristian drops the ball and leaves it to Eugene and Joey. Joey's riding the rails and the two talk about the latest Blizzard news. New games in the making, new champion released and Battle for Azeroth is close by. First week of the Brawl leads to BLOOD IN THE STREETS. Can you mount in a teamfight?",
                            ShortDescription = "Heroes of the Storm, HGC, Overwatch, OWL",
                            LengthInBytes = 70943445,
                            ItunesDuration = "1:26:20",
                            PubDate = DateTimeOffset.Parse("Tue, 12 Jun 2018 21:00:00 +1000"),
                        }
                    };

                    context.Gems.Add(podcastEntity);
                    context.SaveChanges();

                    podcastEntity.FilePath = Path.Combine(paths.PodcastDbDirectory, podcastEntity.Id.ToString());
                    Directory.CreateDirectory(podcastEntity.FilePath);

                    podcastEntity.FilePath = Path.Combine(podcastEntity.FilePath, "Chilled_Podcast_Episode_9.mp3");
                    podcastEntity.PodcastData.Location = $"http://{paths.URLPath}/uploads/{podcastEntity.Id}/Chilled_Podcast_Episode_9.mp3";
                    context.SaveChanges();
                    txn.Commit();
                }

                // EP 10
                using (var txn = context.Database.BeginTransaction())
                {
                    var podcastEntity = new Gem()
                    {
                        Title = "Chilled E-Sports Episode 10",
                        SummaryText = new String("The three chill bros RETURN after fixing Kristian's mic. FOR THE ALLIANCE Eugene and Joey pick a side. Yrel first thoughts and super HOTS and Overwatch strategies with HGC Brawl Playoffs and more! Is 2-2-2 best meta".Take(250).ToArray()),
                        GemType = GemType.Podcast,

                        CreatedById = user.Id,
                        PodcastData = new Podcast()
                        {
                            Description = "The three chill bros RETURN after fixing Kristian's mic. FOR THE ALLIANCE Eugene and Joey pick a side. Yrel first thoughts and super HOTS and Overwatch strategies with HGC Brawl Playoffs and more! Is 2-2-2 best meta",
                            ShortDescription = "Heroes of the Storm, HGC, Overwatch, OWL",
                            LengthInBytes = 67112846,
                            ItunesDuration = "1:21:40",
                            PubDate = DateTimeOffset.Parse("Tue, 19 Jun 2018 21:00:00 +1000"),
                        }
                    };

                    context.Gems.Add(podcastEntity);
                    context.SaveChanges();

                    podcastEntity.FilePath = Path.Combine(paths.PodcastDbDirectory, podcastEntity.Id.ToString());
                    Directory.CreateDirectory(podcastEntity.FilePath);

                    podcastEntity.FilePath = Path.Combine(podcastEntity.FilePath, "Chilled_Podcast_Episode_10.mp3");
                    podcastEntity.PodcastData.Location = $"http://{paths.URLPath}/uploads/{podcastEntity.Id}/Chilled_Podcast_Episode_10.mp3";
                    context.SaveChanges();
                    txn.Commit();
                }

                // EP 11
                using (var txn = context.Database.BeginTransaction())
                {
                    var podcastEntity = new Gem()
                    {
                        Title = "Chilled E-Sports Episode 11",
                        SummaryText = new String("HGC FINALS! We do a deep dive on the crazy 6 game finale of the Heroes Brawl. The boys are getting back into Wow just ahead of BFA and we talk about the brand new Alterac Valley. Eugene finally got to try out Yrel. Kristian's turning his opponents into bananas as Winston. We also talk about what might Dignitas taught us in the HGC Brawl finals.".Take(250).ToArray()),
                        GemType = GemType.Podcast,

                        CreatedById = user.Id,
                        PodcastData = new Podcast()
                        {
                            Description = "HGC FINALS! We do a deep dive on the crazy 6 game finale of the Heroes Brawl. The boys are getting back into Wow just ahead of BFA and we talk about the brand new Alterac Valley. Eugene finally got to try out Yrel. Kristian's turning his opponents into bananas as Winston. We also talk about what might Dignitas taught us in the HGC Brawl finals.",
                            ShortDescription = "Week 8 Phase 1 E-Sports plus Dash cryptocurrency",
                            LengthInBytes = 74961474,
                            ItunesDuration = "1:34:40",
                            PubDate = DateTimeOffset.Parse("Mon, 25 Jun 2018 21:00:00 +1000"),
                        }
                    };

                    context.Gems.Add(podcastEntity);
                    context.SaveChanges();

                    podcastEntity.FilePath = Path.Combine(paths.PodcastDbDirectory, podcastEntity.Id.ToString());
                    Directory.CreateDirectory(podcastEntity.FilePath);

                    podcastEntity.FilePath = Path.Combine(podcastEntity.FilePath, "Chilled_Podcast_Episode_11.mp3");
                    podcastEntity.PodcastData.Location = $"http://{paths.URLPath}/uploads/{podcastEntity.Id}/Chilled_Podcast_Episode_11.mp3";
                    context.SaveChanges();
                    txn.Commit();
                }
            }
            return IdentityResult.Failed();
        }
    }
}
