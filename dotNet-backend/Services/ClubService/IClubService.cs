using dotNet_backend.Models.Club;
using dotNet_backend.Models.Club.DTO;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Services.ClubService
{
    public interface IClubService
    {
        Task<ActionResult<ClubResponseDto>> CreateClubAsync(ClubRequestDto clubRequestDto, string coachUsername);
        Task<ActionResult<IEnumerable<ClubResponseDto>>> GetAllClubsAsync();
        Task<ActionResult<ClubResponseDto>> DeleteClubAsync(string coachUsername);
        Task<ActionResult<ClubResponseDto>> UpdateClubAsync(ClubRequestDto clubRequestDto, string coachUsername);
    }
}
