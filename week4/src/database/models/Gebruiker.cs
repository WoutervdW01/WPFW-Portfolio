namespace pretpark.database.models;

class Gebruiker
{
    public int Id{get; set;}
    public string Email{get; set;}

    public Gebruiker(string Email)
    {
        this.Email = Email;
    }

    public Gebruiker(){

    }
}

class Gast : Gebruiker
{
    public int Credits{get; set;}
    public DateTime GeboorteDatum{get; set;}
    public DateTime EersteBezoek{get; set;}
    public Gast? Begeleidt{get; set;}
    public Gast? Begeleider{get; set;}
    public GastInfo gastInfo;
    public int gastInfoId{get; set;}
    public Attractie? Favoriet{get; set;}
    public List<Reservering>? reservering {get; set;} = null;

    public Gast(string Email) : base(Email)
    {
        this.gastInfo = new GastInfo(this);
    }
}

class Medewerker : Gebruiker
{
    public Medewerker(string Email) : base(Email){

    }
}