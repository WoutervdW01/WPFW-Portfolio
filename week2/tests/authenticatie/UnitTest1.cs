namespace pretpark.authenticatie;

public class UnitTest1
{
    [Theory]
    [InlineData("email", "wachtwoord")]
    public void RegistreerTest(string email, string wachtwoord)
    {
        //Arrange
        GebruikerContextMock gebruikerContextMock = new GebruikerContextMock();
        EmailServiceMock emailServiceMock = new EmailServiceMock();
        GebruikerService gebruikerService = new GebruikerService(gebruikerContextMock, emailServiceMock);

        //Act
        Gebruiker gebruiker = gebruikerService.Registreer(email, wachtwoord);

        //Assert
        Assert.Equal(email, gebruiker.Email);
        Assert.Equal(wachtwoord, gebruiker.Wachtwoord);
    }




    [Theory]
    [InlineData("email", "wachtwoord")]
    [InlineData("email2", "wachtwoord2")]
    public void testVerlopenVerificatieToken(string email, string wachtwoord)
    {
        //Arrange
        GebruikerContextMock gebruikerContextMock = new GebruikerContextMock();
        EmailServiceMock emailServiceMock = new EmailServiceMock();

        GebruikerService gebruikerService = new GebruikerService(gebruikerContextMock, emailServiceMock);

        //Act
        Gebruiker gebruiker = new Gebruiker(email, wachtwoord);
        gebruiker.verificatieToken = new VerificatieToken("1234", DateTime.MinValue);
        bool result = gebruikerService.Verifieer(email, "1234");
        int GebruikerByEmailCallCount = gebruikerContextMock.GebruikerByEmailCallCount;

        //Assert
        Assert.False(result);

        //Verify correct method call
        Assert.Equal(1, GebruikerByEmailCallCount);
        Assert.Equal(email, gebruikerContextMock.GebruikerByEmailInputList[GebruikerByEmailCallCount - 1]);
    }

    [Theory]
    [InlineData("email", "wachtwoord")]
    public void testFouteLogin(string Email, string Wachtwoord)
    {
        //Arrange
        GebruikerContextMock gebruikerContextMock = new GebruikerContextMock();
        EmailServiceMock emailServiceMock = new EmailServiceMock();

        GebruikerService gebruikerService = new GebruikerService(gebruikerContextMock, emailServiceMock);

        //Act
        bool result = gebruikerService.Login(Email, Wachtwoord + "j");

        //Assert
        Assert.False(result);
    }


    //------------------------------------ V MOCK CLASSES V ------------------------------------\\

    class EmailServiceMock : iEmailService
    {
        public bool Email(string Tekst, string naarAdres)
        {
            return true;
        }
    }

    class GebruikerContextMock: iGebruikerContext
    {
        public int GebruikerByEmailCallCount;
        public List<String> GebruikerByEmailInputList = new List<string>();

        List<Gebruiker> gebruikers = new List<Gebruiker>();

        public int AantalGebruikers(){
            return 5;
        }

        public Gebruiker GetGebruiker(int i)
        {
            return new Gebruiker("Email", "Wachtwoord");
        }

        public Gebruiker NieuweGebruiker(string Email, string Wachtwoord)
        {
            Gebruiker res = new Gebruiker(Email, Wachtwoord);
            gebruikers.Add(res);
            return res;
        }

        public Gebruiker GebruikerByEmail(string Email)
        {
            GebruikerByEmailCallCount++;
            GebruikerByEmailInputList.Add(Email);
            Gebruiker res = gebruikers.Find((i) => i.Email == Email)!;
            return res;
        }
    }
}