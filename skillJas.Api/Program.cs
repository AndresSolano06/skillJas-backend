using Microsoft.EntityFrameworkCore;
using skillJas.Infrastructure.Data;
using Microsoft.IdentityModel.Tokens;
using skillJas.Application.Interfaces;
using skillJas.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<skillJasDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
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


builder.Services.AddAuthorization();
builder.Services.AddScoped<ICourseService, CourseService>();


builder.Services.AddAuthorization();

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(80);
});
var app = builder.Build();
app.MapGet("/", () => "✅ SkillJas API is running!");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.Run();
