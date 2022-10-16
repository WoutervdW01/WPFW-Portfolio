namespace pretpark.database.models;
class GastInfo
{
    public int Id{get; set;}
    public string LaatstBezochteURL{get; set;}
    public Coordinate coordinate {get; set;}
}

class Coordinate
{
    public int Id{get; set;}
    public int X{get; set;}
    public int Y{get; set;}
}