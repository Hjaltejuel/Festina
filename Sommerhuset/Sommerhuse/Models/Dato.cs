using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sommerhuse.Models
{
    public class Dato
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        [Column(TypeName= "Date")]
        public DateTime Fra { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Til { get; set; }
        [Required]
        public string Personer { get; set; }

        public override string ToString()
        {
            return RemoveSign(Title + " med : " + Personer);
         
        }

        public string RemoveSign(String remove)
        {
            return remove.Replace("æ", "ea").Replace("ø", "eo").Replace("å", "aa").Replace("Æ", "Æa").Replace("ø", "Eo").Replace("å", "Aa");
        }

    }
}
