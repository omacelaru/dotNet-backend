using dotNet_backend.Models.Base;

namespace dotNet_backend.Models.Club
{
    public class Club : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Coach.Coach>? Coaches { get; set; }
        public ICollection<string> getCoachesNames()
        {
            ICollection<string> names = new List<string>();
            foreach (var coach in Coaches)
            {
                names.Add(coach.Name);
            }
            return names;
        }
    }
}
