using System.ComponentModel.DataAnnotations.Schema;
using dotNet_backend.Models.Base;

namespace dotNet_backend.Models.Request
{
    [Table("Requests")]
    public class RequestInfo : BaseEntity
    {
        public string RequestedByUser { get; set; }
        public string AssignedToUser { get; set; }
        public Guid? CompetitionId { get; set; }
        public string? CompetitionName { get; set; }
        public System.DateTime RequestDate { get; set; }
        public string? Message { get; set; }
        public Enum.RequestType RequestType { get; set; }
        public Enum.RequestStatus RequestStatus { get; set; }

    }
}
