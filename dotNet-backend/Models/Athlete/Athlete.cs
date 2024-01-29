using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using dotNet_backend.Models.Base;

namespace dotNet_backend.Models.Athlete
{
    [Table("Athletes")]
    public class Athlete : User.User
    {
        public string Name { get; set; }

        public int Points { get; set; } = 0;
        public Coach.Coach? Coach { get; set; }
        public Guid? CoachId { get; set; }
        
        public List<Participation.Participation>? Participations { get; set; }
    }
}
