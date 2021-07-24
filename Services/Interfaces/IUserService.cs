using System.Threading.Tasks;
using SantafeApi.Infraestrucutre.Data;

namespace SantafeApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<SantafeApiUser> ManageUserAccess(string userId, string clientId, bool hasAccess);
    }
}