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

public class GebruikerMetRoles
{
    public string UserName{get; set;}
    public List<string> Roles{get; set;}

    public GebruikerMetRoles(string UserName)
    {
        this.UserName = UserName;
        Roles = new List<string>();
    }

    public void AddRole(string Role)
    {
        Roles.Add(Role);
    }

}

public enum Geslacht
{
    Man,
    Vrouw,
    Anders,
    Geheim
}