using System.ComponentModel.DataAnnotations;

namespace dotNet_backend.Models.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
