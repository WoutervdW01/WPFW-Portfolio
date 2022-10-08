namespace pretpark.kaart;

class Kaart
{
    public int Breedte{get; init;}
    public int Hoogte{get; init;}
    private List<KaartItem> items = new List<KaartItem>();
    private List<Pad> paden = new List<Pad>();

    public Kaart(int Breedte, int Hoogte)
    {
        this.Breedte = Breedte;
        this.Hoogte = Hoogte;
    }

    public void Teken(ConsoleTekener t)
    {
        foreach(KaartItem item in items)
        {
            item.TekenConsole(t);
        }
        foreach(Pad pad in paden)
        {
            pad.TekenConsole(t);
        }
    }

    public void VoegItemToe(KaartItem item)
    {
        items.Add(item);
    }

    public void VoegPadToe(Pad pad)
    {
        paden.Add(pad);
    }
}