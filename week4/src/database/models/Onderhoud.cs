namespace pretpark.database.models;

class Onderhoud
{
    public int Id{get; set;}
    public string Probleem{get; set;}
    public Attractie aanAttractie{get; set;}
}