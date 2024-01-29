using System.Runtime.InteropServices.JavaScript;
using dotNet_backend.Models.Base;

namespace dotNet_backend.Models.Competition;

public class Competition : BaseEntity
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<Participation.Participation>? Participations { get; set; }
}