using System;

namespace pretpark.authenticatie
{
    public class GebruikerService
    {

        private iGebruikerContext gebruikerContext;
        private iEmailService emailService;

        public GebruikerService(iGebruikerContext gebruikerContext, iEmailService emailService)
        {
            this.gebruikerContext = gebruikerContext;
            this.emailService = emailService;
        }
        public Gebruiker Registreer(string Email, string Wachtwoord){
            Gebruiker gebruiker = gebruikerContext.NieuweGebruiker(Email, Wachtwoord);
            if(emailService.Email("VerificatieMail " + gebruiker.verificatieToken!.Token, Email))
            {
                this.Verifieer(Email, gebruiker.verificatieToken.Token);
                Console.WriteLine("Geverifieerd");
            }
            return gebruiker;
        }

        public Boolean Login(string Email, String Wachtwoord)
        {
            Gebruiker gebruiker = gebruikerContext.GebruikerByEmail(Email);
            if(gebruiker == null) return false;
            if(gebruiker.verificatieToken != null)
            {
                Console.WriteLine("Account is niet geverifieerd");
                return false;
            }
            return gebruiker.Wachtwoord == Wachtwoord;
            
        }

        public Boolean Verifieer(string Email, string verificatieCode)
        {
            Gebruiker gebruiker = gebruikerContext.GebruikerByEmail(Email);
            if(gebruiker != null 
                && gebruiker.verificatieToken!.VerloopDatum > DateTime.Now
                && gebruiker.verificatieToken!.Token == verificatieCode){
                gebruiker.verificatieToken = null;
                return true;
            } 
            return false;
        }
    }

    public interface iGebruikerContext
    {
        int AantalGebruikers();
        Gebruiker GetGebruiker(int i);
        Gebruiker NieuweGebruiker(string Email, string Wachtwoord);
        Gebruiker GebruikerByEmail(string Email);
    }

    public class GebruikerContext : iGebruikerContext
    {
        public List<Gebruiker> gebruikers = new List<Gebruiker>();
        public int AantalGebruikers()
        {
            return gebruikers.Count;
        }

        public Gebruiker GetGebruiker(int i)
        {
            return gebruikers[i];
        }

        public Gebruiker NieuweGebruiker(string Email, string Wachtwoord)
        {
            Gebruiker res = new Gebruiker(Email, Wachtwoord);
            gebruikers.Add(res);
            return res;
        }

        public Gebruiker GebruikerByEmail(string Email)
        {
            for(int i = 0; i < AantalGebruikers(); i++)
            {
                if(GetGebruiker(i).Email == Email)
                {
                    return GetGebruiker(i);
                }
            }
            return null!;
        }
    }

}