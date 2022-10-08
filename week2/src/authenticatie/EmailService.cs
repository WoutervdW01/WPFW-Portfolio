namespace pretpark.authenticatie
{
    public interface iEmailService
    {
        bool Email(string tekst, string naarAdres);
    }

    class EmailService : iEmailService
    {
        public bool Email(string tekst, string naarAdres){
            Console.WriteLine("To: " + naarAdres);
            Console.WriteLine(tekst);
            Console.WriteLine("Wilt u dit account verifiëren? (Y/N)");
            var userInput = Console.ReadLine();
            return (userInput! == "Y");
        }
    }
}