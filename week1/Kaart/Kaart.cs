namespace pretpark.kaart;

class Kaart
{
    public int Breedte{get; init;}
    public int Hoogte{get; init;}

    public Kaart(int Breedte, int Hoogte)
    {
        this.Breedte = Breedte;
        this.Hoogte = Hoogte;
    }
}