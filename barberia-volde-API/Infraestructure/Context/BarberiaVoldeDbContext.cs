using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infraestructure.Context
{
    public class BarberiaVoldeDbContext : DbContext
    {
        public BarberiaVoldeDbContext(DbContextOptions<BarberiaVoldeDbContext> options) : base(options) {}
        public DbSet<Admin> Admins { get; set; } = null!;
        public DbSet<Turno> Turnos { get; set; } = null!;
    }
}
