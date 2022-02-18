using System.ComponentModel.DataAnnotations;

namespace BackMarvelVSCapman.DTO
{
    public class CreateChraraterDto
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Image { get; set; }
        
        [Required]
        public int TeamId { get; set; }
    }
}