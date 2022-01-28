using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMarvelVSCapman.DAL.Model
{
    public class Character
    {
        [Key]
        public int CharacterId { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Image { get; set; } = "";

        // One to Many
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
