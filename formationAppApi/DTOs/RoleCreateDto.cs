using System.ComponentModel.DataAnnotations;

namespace formationAppApi.DTOs;
public class RoleCreateDto
{
    // [Required]
    // [EmailAddress]
    [Required(ErrorMessage = "Le role est obligatoire.")]
    public string RoleName { get; set; } = string.Empty;
}
