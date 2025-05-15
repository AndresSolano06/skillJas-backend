using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using skillJas.Infrastructure.Data;
using skillJas.Application.Interfaces;
using skillJas.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Servicios base
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger + JWT + Headers
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SkillJas.API", Version = "v1" });

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

    c.OperationFilter<AddRequiredHeadersParameter>();
});

// CORS abierto
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// DbContext y Servicios
builder.Services.AddDbContext<skillJasDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<ISkillJasDbContext>(provider => provider.GetRequiredService<skillJasDbContext>());
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();

// Clerk JWT Config
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://actual-lizard-51.clerk.accounts.dev";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://actual-lizard-51.clerk.accounts.dev",
            ValidateAudience = false,
            NameClaimType = "sub",
            ValidateLifetime = false // Solo para debug local
        };

        // ✅ Mapear sub -> NameIdentifier si no viene correcto
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                var identity = (System.Security.Claims.ClaimsIdentity)context.Principal.Identity;

                var subClaim = identity.FindFirst("sub");
                if (subClaim != null && !identity.HasClaim(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier))
                {
                    identity.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, subClaim.Value));
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

// Networking
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(80);
});

var app = builder.Build();

// Debug de Claims (temporal)
app.Use(async (context, next) =>
{
    if (context.User.Identity?.IsAuthenticated == true)
    {
        Console.WriteLine("✅ Claims del usuario:");
        foreach (var claim in context.User.Claims)
        {
            Console.WriteLine($"- {claim.Type}: {claim.Value}");
        }
    }
    else
    {
        Console.WriteLine("❌ Usuario no autenticado.");
    }

    await next();
});

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "✅ SkillJas API is running!");

app.Run();
