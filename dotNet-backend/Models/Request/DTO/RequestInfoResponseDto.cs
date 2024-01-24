namespace dotNet_backend.Models.Request.DTO
{
    public class RequestInfoResponseDto
    {
        public Guid Id { get; set; }
        public string RequestByUser { get; set; }
        public string AssignedToUser { get; set; }
        public System.DateTime RequestDate { get; set; }
        public string? Message { get; set; }
        public Enum.RequestType RequestType { get; set; }
        public Enum.RequestStatus RequestStatus { get; set; }
    }

}
