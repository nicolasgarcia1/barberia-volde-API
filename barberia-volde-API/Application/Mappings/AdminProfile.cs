using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Response;
using Domain.Entities;

namespace Application.Mappings
{
    public static class AdminProfile
    {
        public static AdminResponse ToAdminResponse(Admin admin)
        {
            return new AdminResponse()
            {
                Username = admin.Username,
                Id = admin.Id
            };
        }
    }
}
