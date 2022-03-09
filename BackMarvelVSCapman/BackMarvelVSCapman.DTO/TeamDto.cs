using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMarvelVSCapman.DTO
{
    public class TeamDto
    {
        [Required]
        public int TeamId { get; set; }
        [Required]
        public string Name { get; set; } = "";


    }
}
