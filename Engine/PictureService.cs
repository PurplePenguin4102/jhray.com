using jhray.com.Database;
using jhray.com.Database.Entities;
using jhray.com.Models.GemMasterViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace jhray.com.Engine
{
    public class PictureService
    {

        public async Task SavePictureToDatabase(PictureGemManagerViewModel gem, ChilledUser user, Paths paths, ChilledDbContext context)
        {
            var gemDB = new Gem()
            {
                Title = gem.PictureMetadata.Title,
                CreatedBy = user, 
                GemType = GemType.Picture,
                SummaryText = gem.PictureMetadata.SummaryText,
                PictureData = new Picture()
                {
                    HoverText = gem.PictureMetadata.HoverText,
                    ArtistName = gem.PictureMetadata.ArtistName,
                    ArtistLink = gem.PictureMetadata.ArtistLink,
                    CreatedDate = DateTime.Now,
                    FileSize = gem.PictureMetadata.PictureFile.Length
                }
            };

            context.Gems.Add(gemDB);
            context.SaveChanges();
            var filePath = paths.PicturesDirectory;
            var fileFolder = Path.Combine(filePath, gemDB.Id.ToString());
            var fileName = gem.PictureMetadata.PictureFile.FileName;
            var filetype = gem.PictureMetadata.PictureFile.ContentType;
            var fullyQualified = Path.Combine(fileFolder, fileName);
            Directory.CreateDirectory(fileFolder);
            using (var stream = new FileStream(fullyQualified, FileMode.CreateNew))
            {
                await gem.PictureMetadata.PictureFile.CopyToAsync(stream);
                stream.Flush();
            }
            gemDB.PictureData.Location = $"//{paths.URLPath}/uploads/pictures/{gemDB.Id}/{fileName}";
            gemDB.FilePath = fullyQualified;
            context.SaveChanges();
        }
    }
}
