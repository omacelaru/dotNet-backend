using System.ComponentModel.DataAnnotations;

namespace dotNet_backend.Models.Club.DTO
{
    public class ClubRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
