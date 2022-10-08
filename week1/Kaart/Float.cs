namespace pretpark.kaart
{
    static class Float 
    {
        public static String metSuffixen(this float f)
        {
            float res = f;
            var resultString = "";
            if(f < 1000) resultString = f.ToString("N2");
            else if(f >= 1000 && f < 1000000 )
            {
                res = res / 1000;
                resultString = String.Format("{0:0.00}K", res);
            }
            else if (f >= 1000000 && f < 1000000000) 
            {
                res = res / 1000000;
                resultString = String.Format("{0:0.00}M", res);
            }
            else 
            {
                res = res / 1000000000;
                resultString = String.Format("{0:0.00}B", res);
            }
            return resultString;
        }
    }
}