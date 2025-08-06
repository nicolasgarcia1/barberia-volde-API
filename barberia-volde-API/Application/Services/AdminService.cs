using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Response;
using Application.Mappings;
using Domain.Interfaces;

namespace Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
           _adminRepository = adminRepository;
        }

        public async Task<AdminResponse> GetAdminAsync()
        {
            var admin = await _adminRepository.GetAdminAsync();
            if (admin != null)
            {
                return AdminProfile.ToAdminResponse(admin);
            }
            return null;
        }
    }
}
