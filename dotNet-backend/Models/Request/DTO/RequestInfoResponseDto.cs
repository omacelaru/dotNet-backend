namespace dotNet_backend.Models.Request.DTO
{
    public class RequestInfoResponseDto
    {
        public Guid Id { get; set; }
        public string RequestedByUser { get; set; }
        public string AssignedToUser { get; set; }
        public System.DateTime RequestDate { get; set; }
        public string? Message { get; set; }
        public string RequestType { get; set; }
        public string RequestStatus { get; set; }
    }
    
    public class RequestInfoWithCompetitionResponseDto : RequestInfoResponseDto
    {
        public Guid CompetitionId { get; set; }
        public string CompetitionName { get; set; }
    }
}
