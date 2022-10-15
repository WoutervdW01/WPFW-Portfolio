public class Attractie
{
    public Guid Id{get; set;}
    public string Naam{get; set;}
    public int Engheid{get; set;}
    public int Bouwjaar{get; set;}
    public IList<LikedBy> LikedBy{get; init;}

    public Attractie(Guid Id, string Naam, int Engheid, int Bouwjaar)
    {
        this.Id = Id;
        this.Naam = Naam;
        this.Engheid = Engheid;
        this.Bouwjaar = Bouwjaar;
        this.LikedBy = new List<LikedBy>();
    }

    public void AddLikeFromUser(LikedBy gebruiker){
        LikedBy.Add(gebruiker);
    }

    public void RemoveLikeFromUser(LikedBy gebruiker){
        LikedBy.Remove(gebruiker);
    }
}

public class AttractieDTO
{
    public string Naam{get; set;}
    public int Engheid{get; set;}
    public int Bouwjaar{get; set;}
}