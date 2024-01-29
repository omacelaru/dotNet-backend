using AutoMapper.Internal;

namespace dotNet_backend.Models.Participation.DTO;

public class ParticipationResponseDto
{
    public KeyValuePair<Guid,string> Competition { get; set; }
    public IEnumerable<KeyValuePair<Guid,string>>? Athletes { get; set; }
}