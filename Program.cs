using Microsoft.EntityFrameworkCore;
using TestCompurent.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDistributedMemoryCache();

// Add session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add CORS (si es necesario para desarrollo)
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar HTTPS redirection
app.UseHttpsRedirection();

// CORS (opcional si aún usás Fetch desde otros dominios, pero no debería hacer falta si usás mismo dominio)
app.UseCors("CorsPolicy");

// Servir archivos estáticos y predeterminados (index.html)
app.UseDefaultFiles();   // busca index.html
app.UseStaticFiles();    // sirve archivos desde wwwroot

app.UseSession();        // sesiones antes de controllers
app.UseAuthorization();

app.MapControllers();

app.Run();
