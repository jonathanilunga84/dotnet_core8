using Microsoft.AspNetCore.Mvc;

//pour UserManager
using Microsoft.AspNetCore.Identity;
using formationAppApi.Models;
using formationAppApi.DTOs;
//End pour UserManager

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _config;
    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        //await Task.Delay(1000);
        if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            return Unauthorized("Email ou mot de passe incorrect");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id!),
            new Claim(ClaimTypes.Email, user.Email!)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });

        // var message = new { message = "Your login ok" };
        // return Ok(message);
    }

    [HttpPost("register")]
    public async Task<IActionResult> register(UserRegisterDto dto)
    {
        var user = new ApplicationUser //IdentityUser
        {
            UserName = dto.Email,
            Email = dto.Email
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);
        
        // 🔗 Assigner le rôle
        if (!await _roleManager.RoleExistsAsync(dto.Role))
            await _roleManager.CreateAsync(new IdentityRole(dto.Role));

        await _userManager.AddToRoleAsync(user, dto.Role);

        return Ok($"Utilisateur '{dto.Email}' créé avec rôle '{dto.Role}'");
    }

    // Avec plusieurs role
    // public async Task<IActionResult> Register(UserRegisterDto dto)
    // {
    //     var user = new ApplicationUser
    //     {
    //         UserName = dto.Email,
    //         Email = dto.Email
    //     };

    //     var result = await _userManager.CreateAsync(user, dto.Password);
    //     if (!result.Succeeded)
    //         return BadRequest(result.Errors);

    //     // Assigner les rôles
    //     foreach (var roleName in dto.Roles)
    //     {
    //         // Vérifier si le rôle existe, le créer si nécessaire
    //         if (!await _roleManager.RoleExistsAsync(roleName))
    //         {
    //             await _roleManager.CreateAsync(new IdentityRole(roleName));
    //         }

    //         // Assigner le rôle à l'utilisateur
    //         await _userManager.AddToRoleAsync(user, roleName);
    //     }

    //     return Ok($"Utilisateur '{dto.Email}' créé avec les rôles : {string.Join(", ", dto.Roles)}");
        // {
        // "email": "utilisateur.multi-role@exemple.com",
        // "password": "MotDePasseSuperSecret",
        // "roles": [
        //     "agent",
        //     "utilisateur",
        //     "client"
        // ]
        // }
    // }

    [HttpPost("role")]
    //public async Task<IActionResult> CreateRole(string roleName)
    public async Task<IActionResult> CreateRole(RoleCreateDto dto)
    {
        if (!await _roleManager.RoleExistsAsync(dto.RoleName))
        {
            var role = new IdentityRole(dto.RoleName);
            await _roleManager.CreateAsync(role);
        }

        return Ok($"Rôle '{dto.RoleName}' créé");
    }
}