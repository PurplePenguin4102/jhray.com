using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jhray.com.Models.GemMasterViewModels
{
    public class PictureMetadata
    {
        [Required]
        public string Title { get; set; }
        public string SummaryText { get; set; }
        public string HoverText { get; set; }

        [Required]
        public string ArtistName { get; set; }
        [Required]
        public string ArtistLink { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public IFormFile PictureFile { get; set; }
    }
}
