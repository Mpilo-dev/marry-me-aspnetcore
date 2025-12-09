using Marry_Me.EF.Context;
using Marry_Me.Services.Abstraction;
using Marry_Me.Services.Abstraction.BusinessLogic;
using Marry_Me.Services.Abstraction.DataAccess;
using Marry_Me.Services.Concrete;
using Marry_Me.Services.Concrete.BusinessLogic;
using Marry_Me.Services.Concrete.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
        policy.WithOrigins(
                "http://localhost:8000",
                "http://127.0.0.1:8000",
                "http://localhost:3000",
                "http://127.0.0.1:3000",
                "https://marry-me.online",
                "https://www.marry-me.online"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
    );
});

builder.Services.AddDbContext<MarriageSystemDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Marry_MEDatabase")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!)),
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJWTService, JWTService>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IMarriageRepository, MarriageRepository>();
builder.Services.AddScoped<IMarriageService, MarriageService>();
builder.Services.AddScoped<IDivorceRepository, DivorceRepository>();
builder.Services.AddScoped<IDivorceService, DivorceService>();

builder.WebHost.UseUrls("http://0.0.0.0:5000");
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Frontend");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
