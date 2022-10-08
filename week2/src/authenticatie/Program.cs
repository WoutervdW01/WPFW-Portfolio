namespace pretpark.authenticatie
{
class AuthApp
{
    private static GebruikerService gebruikerService = new GebruikerService(new GebruikerContext(), new EmailService());
    public static void Main(string[] args)
    {
        menu();
    }

    
        private static void menu()
        {            
            Boolean running = true;
            while (running)
            {
                string choices = "Maak een keuze: \n\t 1. Registreer \n\t 2. Login \n\t 3. Sluit progromma";
                Console.WriteLine(choices);
                int choice;
                try {
                    choice = Int32.Parse(Console.ReadLine()!);
                } catch (Exception) 
                {
                    choice = Int32.MaxValue;
                }
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Registreer");
                        registreerGebruiker();
                        break;
                    case 2:
                        Console.WriteLine("Login");
                        login();
                        break;
                    case 3:
                        Console.WriteLine("Sluit af");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Geen geldige keuze, probeer opnieuw");
                        break;
                }
            }
        }

        private static void registreerGebruiker()
        {
            Console.WriteLine("Email:");
            string Email = Console.ReadLine()!;
            Console.WriteLine("Wachtwoord:");
            string Wachtwoord = Console.ReadLine()!;

            //GebruikerService gebruikerService = new GebruikerService();
            gebruikerService.Registreer(Email, Wachtwoord);
        }

        private static void login()
        {
            Console.WriteLine("Email:");
            string Email = Console.ReadLine()!;
            Console.WriteLine("Wachtwoord:");
            string Wachtwoord = Console.ReadLine()!;
            bool correctLogin = gebruikerService.Login(Email, Wachtwoord);
            Console.WriteLine(correctLogin);
            
        }
        

}


}