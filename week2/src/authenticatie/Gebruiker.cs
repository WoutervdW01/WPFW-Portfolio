namespace pretpark.authenticatie;

class Gebruiker
{
    public string Wachtwoord{get; set;}
    public string Email{get; set;}
    public VerificatieToken? verificatieToken{get; set;}

    public bool Geverifieerd()
    {
        return verificatieToken == null;
    }

    public Gebruiker(string Email, string Wachtwoord)
    {
        Random rnd = new Random();
        string token = "" + rnd.Next(1000, 9999);
        this.Email = Email;
        this.Wachtwoord = Wachtwoord;
        this.verificatieToken = new VerificatieToken(token, DateTime.Now.AddDays(3));
    }

}