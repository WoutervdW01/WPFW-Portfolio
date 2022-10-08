namespace pretpark.kaart;

struct Coordinaat
{
    public int x{get; init;}
    public int y{get; init;}

    public Coordinaat(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static Coordinaat operator +(Coordinaat A, Coordinaat B)
    {
        return new Coordinaat(A.x + B.x, A.y + B.y);
    }
}