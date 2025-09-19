using formationAppApi.Data;
using Microsoft.EntityFrameworkCore;
// using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
// using System;
// using MySql.EntityFrameworkCore.Extensions;
using formationAppApi.Models;

//pour JWT
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
//End JWT

//Swagger
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
//End Swagger

using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Active les controllers
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(); // Pour generer la documatation swagger

// Configuration pour MySQL
builder.Services.AddDbContext<MonDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 43)) // Adaptez la version à votre MySQL
    ));

// Configuration JWT
// Ce code configure l'authentification JWT dans votre application ASP.NET Core. et Configure le système d'authentification pour utiliser les JSON Web Tokens (JWT).
//
try
{
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var jwtKey = builder.Configuration["Jwt:Key"]
                ?? throw new ArgumentException("Jwt:Key manquant dans appsettings.json");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });
}
catch (ArgumentException ex)
{
    // Log l'erreur et arrête l'application proprement
    Console.WriteLine($"Erreur de configuration: {ex.Message}");
    throw;
}

// APPROCHE OPTIMALE (la meilleure)
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = builder.Configuration["Jwt:Issuer"],
//             ValidAudience = builder.Configuration["Jwt:Audience"],
//             IssuerSigningKey = new SymmetricSecurityKey(
//                 Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//         };
//     });

// End Configuration JWT

//Activer l’authentification dans Swagger
builder.Services.AddSwaggerGen(c =>
{
    //c.SwaggerDoc("v1", new OpenApiInfo { Title = "Formation API", Version = "v1" });

    // 🔐 Ajouter le support JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Exemple : \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});
//Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...

// End Activer l’authentification dans Swagger

//Pour UserManager
// Ajoutez les services d'identité à votre conteneur.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Vous pouvez configurer des options ici, comme les règles de mot de passe.
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<MonDbContext>()
.AddDefaultTokenProviders();
// End Pour UserManager

var app = builder.Build();

// CORRECT : Authentication AVANT Authorization
app.UseAuthentication();    // 1. Qui êtes-vous ?
app.UseAuthorization();     // 2. Avez-vous le droit ?

// app.UseAuthorization(); // Si tu as un middleware d’auth

app.MapControllers(); // Très important pour que les [ApiController] soient pris en compte

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //app.UseAuthentication();
    //app.UseAuthorization();

}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// app.MapGet("/ab", () => "Hello World ab!");

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
