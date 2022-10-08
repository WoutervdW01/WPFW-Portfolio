namespace pretpark.authenticatie;

public class authenticatieMockTests
{

    [Theory]
    [InlineData("Email", "Wachtwoord")]
    public void RegistreerTest(string email, string wachtwoord)
    {
        //Arrange
        Mock<iGebruikerContext> gebruikerContext = new Mock<iGebruikerContext>();
        Mock<iEmailService> emailService = new Mock<iEmailService>();
        emailService.Setup((i) => i.Email(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
        gebruikerContext.Setup((i) => i.NieuweGebruiker(It.IsAny<string>(), It.IsAny<string>())).Returns(new Gebruiker(email, wachtwoord));

        GebruikerService gebruikerService = new GebruikerService(gebruikerContext.Object, emailService.Object);

        //Act
        Gebruiker gebruiker = gebruikerService.Registreer(email, wachtwoord);

        //Assert
        Assert.Equal(email, gebruiker.Email);
    }

    [Theory]
    [InlineData("email")]
    public void testVerlopenVerificatieToken(string email)
    {
        //Arrange
        Mock<iGebruikerContext> gebruikerContext = new Mock<iGebruikerContext>();
        Mock<iEmailService> emailService = new Mock<iEmailService>();
        Gebruiker gebruiker = new Gebruiker(email, "ww");
        gebruiker.verificatieToken = new VerificatieToken("1234", DateTime.MinValue);
        gebruikerContext.Setup((i) => i.GebruikerByEmail(email)).Returns(gebruiker);

        GebruikerService gebruikerService = new GebruikerService(gebruikerContext.Object, emailService.Object);

        //Act
        bool result = gebruikerService.Verifieer(email, "1234");

        //Assert
        Assert.False(result);
        gebruikerContext.Verify((i) => i.GebruikerByEmail(email), Times.Once());
    }

    [Theory]
    [InlineData("email", "wachtwoord")]
    [InlineData("email2", "wachtwoord")]
    public void testFouteLogin(string Email, string Wachtwoord)
    {
        //Arrange
        Mock<iGebruikerContext> gebruikerContext = new Mock<iGebruikerContext>();
        Mock<iEmailService> emailService = new Mock<iEmailService>();
        gebruikerContext.Setup((i) => i.GebruikerByEmail(Email)).Returns(new Gebruiker(Email, Wachtwoord));

        GebruikerService gebruikerService = new GebruikerService(gebruikerContext.Object, emailService.Object);

        //Act
        bool result = gebruikerService.Login(Email, Wachtwoord + "j");

        //Assert
        Assert.False(result);
    }


}