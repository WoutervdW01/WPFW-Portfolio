namespace pretpark.database.models;

class Attractie
{
    public int Id{get; set;}
    public string Naam{get; set;}

    public Task<bool> OnderhoudBezig(DatabaseContext databaseContext)
    {
        // TODO
        return Task.FromResult(false);
    }

    public Task<bool> Vrij(DatabaseContext databaseContext, DateTimeBereik dateTimeBereik)
    {
        // TODO
        return Task.FromResult(true);
    }

    public Attractie(string Naam){
        this.Naam = Naam;
    }

    public Attractie(){
        
    }
}