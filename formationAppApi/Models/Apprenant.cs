using System.Runtime.InteropServices;
using System.Data.Common;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace formationAppApi.Models;
public class Apprenant
{
    public int Id { get; set; }

    [MaxLengthAttribute(60), Required] //OU RequiredAttribute
    [DefaultValueAttribute("My name")] // Pour la base de données
    public string Nom { get; set; } = "My name"; // pour le code

    [MaxLengthAttribute(60)]
    //[DefaultValueAttribute("My name")]
    public string? Postnom { get; set; }

    [MaxLengthAttribute(60)]
    public string? Prenom { get; set; }

    [MaxLengthAttribute(12)]
    public string? Sexe { get; set; }
    public DateTime DateNaissance { get; set; } = DateTime.Now;

    [MaxLengthAttribute(100)]
    public string? Email { get; set; }

    [MaxLengthAttribute(20)]
    public string? Telephone { get; set; }
    public string? Adresse { get; set; }

    //public ICollection<Cours> CoursSuivis { get; set; } //= new List<Cours>();
    //public ICollection<Cours>? CoursSuivis { get; set; }
    //public required ICollection<Cours> CoursSuivis { get; set; } = new HashSet<Cours>();
    public ICollection<Cours> CoursSuivis { get; set; } = new HashSet<Cours>();
}