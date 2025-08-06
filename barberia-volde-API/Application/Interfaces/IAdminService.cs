using System.Threading.Tasks;
using Application.Models.Response;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        Task<AdminResponse> GetAdminAsync();
    }
}
