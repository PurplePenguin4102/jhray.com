using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jhray.com.Database.Entities
{
    public class Gem
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public string SummaryText { get; set; }
        public GemType GemType { get; set; }

        [ForeignKey("FK_ChilledUser_Gem")]
        public string CreatedById { get; set; }
        public ChilledUser CreatedBy { get; set; }
    }
}
