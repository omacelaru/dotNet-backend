using dotNet_backend.Models.Base;

namespace dotNet_backend.Models.Competition;

public class Competition : BaseEntity
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    
    public List<Participation.Participation>? Participations { get; set; }
}