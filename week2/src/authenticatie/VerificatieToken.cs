namespace pretpark.authenticatie;

class VerificatieToken
{
    public string Token{get; set;}
    public DateTime VerloopDatum{get; set;}

    public VerificatieToken(string Token, DateTime VerloopDatum)
    {
        this.Token = Token;
        this.VerloopDatum = VerloopDatum;
    }
}