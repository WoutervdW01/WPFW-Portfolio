namespace pretpark.database.models;

class Gebruiker
{
    public int Id{get; set;}
    public string Email{get; set;}
}

class Gast : Gebruiker
{
    public int Credits{get; set;}
    public DateTime GeboorteDatum{get; set;}
    public DateTime EersteBezoek{get; set;}
    public Gast? Begeleidt{get; set;}
    public GastInfo gastInfo{get; set;}
}

class Medewerker : Gebruiker
{

}