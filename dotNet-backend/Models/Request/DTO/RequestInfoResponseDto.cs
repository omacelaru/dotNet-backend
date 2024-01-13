namespace dotNet_backend.Models.Request.DTO
{
    public class RequestInfoResponseDto
    {
        //CreateMap<RequestInfo, RequestInfoResponseDto>()
        // .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RequestByUser));
        public string Name { get; set; }
    }

}
