using System.ComponentModel.DataAnnotations;

namespace GameCatalogAPI.Models.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The name of the game must have between 3 and 100 character")]

        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The name of the producer must have between 3 and 100 character")]
        public string Producer { get; set; }
        [Required]
        [StringLength(350, ErrorMessage = "You are at the limit of the character")]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
