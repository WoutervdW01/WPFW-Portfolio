using Microsoft.AspNetCore.Identity;

public class GebruikerMetWachwoord : IdentityUser
{
    public string? Password{get; init;}
    public Geslacht geslacht{get; set;}
}

public enum Geslacht
{
    Man,
    Vrouw,
    Anders,
    Geheim
}