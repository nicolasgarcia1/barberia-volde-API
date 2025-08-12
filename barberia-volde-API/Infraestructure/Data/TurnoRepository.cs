using Application.Models.Request;
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
    public class TurnoRepository : ITurnoRepository
    {
        private readonly BarberiaVoldeDbContext _context;

        public TurnoRepository(BarberiaVoldeDbContext context)
        {
            _context = context;
        }

        public async Task<List<Turno>> GetAllTurnosAsync()
        {
            return await _context.Turnos.ToListAsync();
        }

        public async Task<List<Turno>> GetAllTurnosPendientesAsync()
        {
            return await _context.Turnos
                .Where(t => t.Estado == Domain.Enum.EstadoTurno.Pendiente)
                .ToListAsync();
        }

        public async Task<Turno?> GetTurnoByIdAsync(int id)
        {
            return await _context.Turnos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> CrearTurnoAsync(Turno turno)
        {
            bool existe = await _context.Turnos.AnyAsync(t => t.FechaHora == turno.FechaHora);
            if (existe)
            {
                return false;
            }

            _context.Turnos.Add(turno);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task ActualizarTurnoAsync(Turno turno)
        {
            _context.Turnos.Update(turno);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarEstadoAsync(Turno turno)
        {
            _context.Turnos.Update(turno);
            await _context.SaveChangesAsync();
        }
    }
}
