using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using skillJas.Infrastructure.Data;
using skillJas.Application.Interfaces;
using skillJas.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// 🔧 Servicios base
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 🔐 Swagger + JWT + X-Role Header
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SkillJas.API", Version = "v1" });

    // JWT Bearer config
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa tu token Clerk: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    // Rol Header
    c.OperationFilter<AddRoleHeaderFilter>();
});

// 📦 DbContext y Servicios
builder.Services.AddDbContext<skillJasDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<ISkillJasDbContext>(provider => provider.GetRequiredService<skillJasDbContext>());
builder.Services.AddScoped<ICourseService, CourseService>();

// 🔐 Clerk JWT Config
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://actual-lizard-51.clerk.accounts.dev";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://actual-lizard-51.clerk.accounts.dev",
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization();

// 🌐 Networking
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(80);
});

// 🚀 App Pipeline
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => "✅ SkillJas API is running!");

app.Run();


