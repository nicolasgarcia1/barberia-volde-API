using Application.Interfaces;
using Application.Models.Helpers;
using Application.Services;
using Domain.Interfaces;
using Infraestructure.Context;
using Infraestructure.Data;
using Infraestructure.ThirstService;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// configuración del db context
builder.Services.AddDbContext<BarberiaVoldeDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// configuración de autenticación
builder.Services.Configure<AuthServiceOptions>(builder.Configuration.GetSection("AuthServiceOptions"));

// configuracion del swagger
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("BarberiaVoldeApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Please, paste the token to login for use all endpoints."
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "BarberiaVoldeApiBearerAuth"
                        }
                    },
                    new List<string>()
                }
            });
});

builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true, // Valida el tiempo de vida del token
                    ClockSkew = TimeSpan.Zero, // Elimina la tolerancia por defecto de 5 minutos del token
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["AuthServiceOptions:Issuer"],
                    ValidAudience = builder.Configuration["AuthServiceOptions:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["AuthServiceOptions:SecretForKey"]!))
                };
            }
        );

// configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// servicios de terceros
builder.Services.AddScoped<IAuthService, AuthService>();

// servicios de infraestructura
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ITurnoService, TurnoService>();

// repositorios de infraestructura
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<ITurnoRepository, TurnoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;

    if (response.StatusCode == 401)
    {
        await response.WriteAsJsonAsync(new { message = "No estás autenticado. Iniciá sesión para continuar." });
    }
    else if (response.StatusCode == 403)
    {
        await response.WriteAsJsonAsync(new { message = "No tenés permisos para acceder a este recurso." });
    }
});


app.UseAuthentication();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
