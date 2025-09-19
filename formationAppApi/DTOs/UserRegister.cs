using System.ComponentModel.DataAnnotations;

namespace formationAppApi.DTOs;

public class UserRegisterDto
{
    [Required(ErrorMessage = "L'email est requis.")]
    [EmailAddress(ErrorMessage = "Le format de l'email est invalide.")]
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty; // rôle à assigner
    
    // public List<string> Roles { get; set; } // Changement ici : une liste de rôles
}