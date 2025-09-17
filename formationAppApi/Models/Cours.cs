// using System.Runtime.InteropServices;
// using System.Data.Common;
using System.ComponentModel.DataAnnotations;
// using System.ComponentModel;
// using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations.Schema;

namespace formationAppApi.Models;

[Table("cours")]
public class Cours
{
    public int Id { get; set; }

    [MaxLengthAttribute(255), Required]
    public string Titre { get; set; } = string.Empty;
    public string? Description { get; set; }

    [MaxLengthAttribute(20)]
    public String? DureeHeures { get; set; }

    public ICollection<Apprenant> ApprenantsInscrits { get; set; } = new List<Apprenant>();
}