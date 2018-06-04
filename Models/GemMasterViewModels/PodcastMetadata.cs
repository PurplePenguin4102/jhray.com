using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace jhray.com.Models.GemMasterViewModels
{
    public class PodcastMetadata
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ShortDescription { get; set; }
        
        [Required]
        public string ItunesDuration { get; set; }

        [Required]
        public IFormFile PodcastFile { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTimeOffset PubDate { get; set; }

        private ulong LengthInBytes { get; set; }
        private string Location { get; set; }

    }
}
