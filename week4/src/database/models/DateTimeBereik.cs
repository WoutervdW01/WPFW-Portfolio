namespace pretpark.database.models;

class DateTimeBereik
{
    public int Id{get; set;}
    public DateTime Begin{get; set;}
    public DateTime? Eind{get; set;}

    public bool Eindigt()
    {
        // TODO
        return false;
    }

    public bool Overlapt(DateTimeBereik that)
    {
        // TODO
        return false;
    }
}