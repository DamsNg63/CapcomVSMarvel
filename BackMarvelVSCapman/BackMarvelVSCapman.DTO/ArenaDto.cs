using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMarvelVSCapman.DTO
{
    public class ArenaDto
    {
        [Required]
        public int ArenaId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }
    }
}
