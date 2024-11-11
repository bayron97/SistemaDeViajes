using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaDeViajes;
using SistemaDeViajes.Context;
using SistemaDeViajes.Interfaces;
using SistemaDeViajes.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<SiViajeContext>(options => options.UseSqlServer("DefaultConnection"));
builder.Services.AddScoped<ISucursale, SucursaleService>();
builder.Services.AddScoped<IUsuario, UsuarioService>();
builder.Services.AddScoped<IEmpleado, EmpleadoService>();
builder.Services.AddScoped<ITransportista, TransportistaService>();
builder.Services.AddScoped<IAsignarSucursal, AsignarSucursaleService>();
builder.Services.AddScoped<IViaje, ViajeService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




//mapper
var mapperConfig = new MapperConfiguration(m => { m.AddProfile(new MappingProfile()); });

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);




builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
    
app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();


