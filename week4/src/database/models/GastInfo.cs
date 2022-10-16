namespace pretpark.database.models;
class GastInfo
{
    public int Id{get; set;}
    public string? LaatstBezochteURL{get; set;}
    public Gast gast{get; set;} = null!;
    public Coordinate coordinate {get; set;}

    public GastInfo(Gast gast)
    {
        this.gast = gast;
    }

    public GastInfo(){

    }
}

class Coordinate
{
    public int Id{get; set;}
    public int X{get; set;}
    public int Y{get; set;}
}