public class Attractie
{
    public Guid Id{get; set;}
    public string Naam{get; set;}
    public int Engheid{get; set;}
    public int Bouwjaar{get; set;}

    public Attractie(Guid Id, string Naam, int Engheid, int Bouwjaar)
    {
        this.Id = Id;
        this.Naam = Naam;
        this.Engheid = Engheid;
        this.Bouwjaar = Bouwjaar;
    }
}

public class AttractieDTO
{
    public string Naam{get; set;}
    public int Engheid{get; set;}
    public int Bouwjaar{get; set;}
}