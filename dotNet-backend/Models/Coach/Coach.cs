using System.ComponentModel.DataAnnotations.Schema;

namespace dotNet_backend.Models.Coach
{
    [Table("Coaches")]
    public class Coach : User.User
    {
        public string Name { get; set; }

        public Club.Club? Club { get; set; }
        public Guid? ClubId { get; set; }

        public ICollection<Athlete.Athlete>? Athletes { get; set; } = new List<Athlete.Athlete>();
    }
}