using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
//Pour identity
// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace formationAppApi.Models;
public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}