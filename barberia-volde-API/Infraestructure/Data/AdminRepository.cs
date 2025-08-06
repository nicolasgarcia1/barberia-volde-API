using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data
{
    public class AdminRepository : IAdminRepository
    {
        private readonly BarberiaVoldeDbContext _context;

        public AdminRepository(BarberiaVoldeDbContext context)
        {
            _context = context;
        }

        public async Task<Admin?> GetAdminAsync()
        {
            return await _context.Admins.FirstOrDefaultAsync();
        }
    }
}
