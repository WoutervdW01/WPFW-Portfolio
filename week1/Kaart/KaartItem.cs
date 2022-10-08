namespace pretpark.kaart;

abstract class KaartItem : Tekenbaar
{
    private Coordinaat _locatie;

    public KaartItem(Kaart kaart, Coordinaat _locatie)
    {
        setLocatie(kaart, _locatie);
    }

    public Coordinaat getLocatie()
    {
        return _locatie;
    }

    public void setLocatie(Kaart kaart, Coordinaat _locatie)
    {
        if((_locatie.x > 0 && _locatie.x <= kaart.Breedte) && (_locatie.y > 0 && _locatie.y <= kaart.Hoogte))
        {
            this._locatie = _locatie;
        }
    }

    public void TekenConsole(ConsoleTekener t)
    {
        t.SchrijfOp(getLocatie(), getKarakter().ToString());
    }

    public abstract char getKarakter();
}