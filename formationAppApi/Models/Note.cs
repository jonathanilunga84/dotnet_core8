using System.ComponentModel.DataAnnotations;
using System;
namespace formationAppApi.Models;

public class Note
{
    public int Id { get; set; }
    public int Valeur { get; set; } = 0;

    [MaxLengthAttribute(255)]
    public string? Commentaire { get; set; } = string.Empty;

    public int ApprenantId { get; set; } = 0;
    public Apprenant Apprenant { get; set; } = null!;

    public int? CoursId { get; set; }
    public Cours? Cours { get; set; }
}