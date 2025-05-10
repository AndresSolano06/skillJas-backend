using Microsoft.EntityFrameworkCore;
using SkillJas.Infrastructure.Data;
using Microsoft.IdentityModel.Tokens;
using skillJas.Application.Interfaces;
using skillJas.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SkillJasDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://<TU_SUBDOMINIO>.clerk.accounts.dev"; 
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = true,
            ValidIssuer = "https://<TU_SUBDOMINIO>.clerk.accounts.dev" 
        };
    });


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
