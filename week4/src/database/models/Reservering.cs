namespace pretpark.database.models;

class Reservering
{
    public int Id{get; set;}
    public int GastId{get; set;}
    public Gast? Gast{get; set;}
    public int AttractieId{get; set;}
    public Attractie? Attractie{get; set;}
    public DateTimeBereik? Gedurende{get; set;} = null;
}