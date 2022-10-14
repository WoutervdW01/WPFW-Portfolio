using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

public class GebruikerMetWachwoord : IdentityUser
{
    public string? Password{get; init;}
    public Geslacht geslacht{get; set;}
}

public class GebruikerLogin
{

    [Required(ErrorMessage = "Username is required")]
    public string? UserName{get; set;}
    [Required(ErrorMessage = "Password is required")]
    public string? Password{get; set;}
}

public enum Geslacht
{
    Man,
    Vrouw,
    Anders,
    Geheim
}