using System.Threading.Tasks;
using Ocuco.Application.Services.OcucoHub.LuxotticaRXO.Dtos;

namespace Ocuco.Application.Services.OcucoHub.LuxotticaRXO
{
    public interface ILuxotticaRXOWSService
    {
        BaseResponseEntity CallCheckFrame(CheckFrameRequest model);
        Task<BaseResponseEntity> CallCheckFrameAsync(CheckFrameRequest model);
    }
}