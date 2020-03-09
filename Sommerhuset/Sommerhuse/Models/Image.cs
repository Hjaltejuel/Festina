using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sommerhuse.Models
{
    public class Image
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string src { get; set; }

        [Required]
        public DateTime uploadedAt { get; set; }
    }
}
