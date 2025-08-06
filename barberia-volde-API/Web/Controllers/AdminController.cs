using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet("GetAdmin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAdmin()
    {
        try
        {
            var admin = await _adminService.GetAdminAsync();
            if (admin == null)
            {
                return NotFound(new { message = "Administrador no encontrado." });
            }
            return Ok(admin);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error interno del servidor.", details = ex.Message });
        }
    }
}
