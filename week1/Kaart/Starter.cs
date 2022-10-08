namespace pretpark.kaart;

class Starter
{
    public static void Main(string[] args)
    {
        Kaart kaart = new Kaart(40, 50);
        Console.WriteLine("Breedte: " + kaart.Breedte);
        Console.WriteLine("Hoogte: " + kaart.Hoogte);
    }
}