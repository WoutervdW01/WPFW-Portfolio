using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AsyncBoekOpdracht
{
    class Boek
    {
        public string Titel { get; set; }
        public string Auteur { get; set; }
        public async Task<(Boek, float)> AIScore() {
            // Deze 'berekening' is eigenlijk een ingewikkeld AI algoritme.
            // Pas de volgende vier regels niet aan.
            Stopwatch timer = new Stopwatch();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t (" + DateTime.Now.ToString("{0:hh:mm:ss:fff}") + ") start berekening AI score voor boek " + this.Titel);
            //Console.ResetColor();
            timer.Start();
            double ret = 1.0f;
            for (int i = 0; i < 10000000; i++)
                for (int j = 0; j < 10; j++)
                    ret = (ret + Willekeurig.Random.NextDouble()) % 1.0;
            timer.Stop();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\t (" + DateTime.Now.ToString("{0:hh:mm:ss:fff}") + ") AI score voor boek " + this.Titel + " = " + (float) ret + " (" + timer.ElapsedMilliseconds + " ms)");
            //Console.ResetColor();
            return (this, (float)ret);
        }
    }
    static class Willekeurig
    {
        public static Random Random = new Random();
        public static async Task Vertraging(int MinAantalMs = 500, int MaxAantalMs = 1000)
        {
            await Task.Delay(Random.Next(MinAantalMs, MaxAantalMs));
        }
    }
    static class Database
    {
        private static List<Boek> lijst = new List<Boek>();
        public static async Task VoegToe(Boek b)
        {
            await Willekeurig.Vertraging(); // INSERT INTO ...
            lijst.Add(b);
        }
        public static async Task<List<Boek>> HaalLijstOp()
        {
            await Willekeurig.Vertraging(); // SELECT * FROM ...
            return lijst;
        }
        public static async void Logboek(string melding)
        {
            await Willekeurig.Vertraging(); // schrijf naar logbestand
        }

        public static void Seed()
        {
            for(int i = 0; i < 20; i++)
            {
                Boek b = new Boek();
                b.Titel = "Boek " + i;
                lijst.Add(b);
            }
            Console.WriteLine("Database seeded");
        }
    }
    class Program
    {
        private static List<(Boek, float)> AIScoreList = new List<(Boek, float)>();

        static async Task VoegBoekToe() {
            Console.WriteLine("Geef de titel op: ");
            var titel = Console.ReadLine();
            Console.WriteLine("Geef de auteur op: ");
            var auteur = Console.ReadLine();
            await Database.VoegToe(new Boek {Titel = titel, Auteur = auteur});
            Database.Logboek("Er is een nieuw boek!");
            Console.WriteLine("De huidige lijst met boeken is: ");
            foreach (var boek in await Database.HaalLijstOp()) {
                Console.WriteLine(boek.Titel);
            }
        }


        static async Task ZoekBoekParallel() {
            Console.WriteLine("Waar gaat het boek over?");
            var beschrijving = Console.ReadLine();
            Boek beste = null;
            float highestScore = 0;
            List<Task<(Boek, float)>> myTasks = new List<Task<(Boek, float)>>();
            Console.WriteLine("\t ZoekBoek methode called on " + DateTime.Now.ToString("{0:hh:mm:ss:fff}"));

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\t (" + DateTime.Now.ToString("{0:hh:mm:ss:fff}") + ") Lijst ophalen... ");
            var BoekenLijst = await Database.HaalLijstOp();
            Console.WriteLine("\t (" + DateTime.Now.ToString("{0:hh:mm:ss:fff}") + ") Lijst opgehaald");
                        
            Parallel.ForEach(BoekenLijst, async b => {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t (" + DateTime.Now.ToString("{0:hh:mm:ss:fff}") + ") Calling AI Score on boek: " + b.Titel);
                (Boek, float) score = await b.AIScore();
                AIScoreList.Add(score);
            });
            
            foreach((Boek, float) tuple in AIScoreList)
            {
                if (beste == null || tuple.Item2 > highestScore)
                    beste = tuple.Item1;
                    highestScore = tuple.Item2;
            }
            Console.ResetColor();
            Console.WriteLine("Het boek dat het beste overeenkomt met de beschrijving is: ");
            Console.WriteLine(beste.Titel);
        }

        static async Task ZoekBoekTaskPool()
        {
            Console.WriteLine("Waar gaat het boek over?");
            var beschrijving = Console.ReadLine();
            Boek beste = null;
            float highestScore = 0;
            List<Task<(Boek, float)>> myTasks = new List<Task<(Boek, float)>>();
            Console.WriteLine("\t ZoekBoek methode called on " + DateTime.Now.ToString("{0:hh:mm:ss:fff}"));            

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\t (" + DateTime.Now.ToString("{0:hh:mm:ss:fff}") + ") Lijst ophalen... ");
            var BoekenLijst = await Database.HaalLijstOp();
            Console.WriteLine("\t (" + DateTime.Now.ToString("{0:hh:mm:ss:fff}") + ") Lijst opgehaald");
                        
            foreach(var boek in BoekenLijst)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t (" + DateTime.Now.ToString("{0:hh:mm:ss:fff}") + ") Calling AI Score on boek: " + boek.Titel);
                myTasks.Add(Task.Run(() => boek.AIScore()));
                //myTasks.Add(boek.AIScore());
            }

            var result = await Task.WhenAll(myTasks).ConfigureAwait(false); 
            foreach((Boek, float) tuple in result)
            {
                if (beste == null || tuple.Item2 > highestScore)
                    beste = tuple.Item1;
                    highestScore = tuple.Item2;
            }
            Console.ResetColor();
            Console.WriteLine("Het boek dat het beste overeenkomt met de beschrijving is: ");
            Console.WriteLine(beste.Titel);
        }


        static async Task ZoekBoekBasic() {
            Console.WriteLine("Waar gaat het boek over?");
            var beschrijving = Console.ReadLine();
            Boek beste = null;
            foreach (var boek in await Database.HaalLijstOp())
                if (beste == null || (await boek.AIScore()).Item2 > (await beste.AIScore()).Item2)
                    beste = boek;
            Console.WriteLine("Het boek dat het beste overeenkomt met de beschrijving is: ");
            Console.WriteLine(beste.Titel);
        }


        static bool Backupping = false;
        // "Backup" kan lang duren. We willen dat de gebruiker niet hoeft te wachten,
        // en direct daarna boeken kan toevoegen en zoeken.
        static async Task Backup() {
            if (Backupping)
                return;
            Backupping = true;
            await Willekeurig.Vertraging(2000, 3000);
            Backupping = false;
        }
        static async Task Main(string[] args)
        {
            Database.Seed();
            Stopwatch timer = new Stopwatch();
            Console.WriteLine("Welkom bij de boeken administratie!");
            string key = null;
            DateTime start;
            DateTime end;
            while (key != "q") {
                Console.WriteLine("+) Boek toevoegen");
                Console.WriteLine("z) Boek zoeken");
                Console.WriteLine("b) Backup maken van de boeken");
                Console.WriteLine("q) Quit");
                key = Console.ReadLine();
                if (key == "+")
                    await VoegBoekToe();
                else if (key == "z")
                {
                    timer.Start();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\t (" + DateTime.Now.ToString("{0:hh:mm:ss:fff}") + ") Calling ZoekBoek()...");
                    Console.ResetColor();

                    await ZoekBoekParallel();
                    timer.Stop();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\t (" + DateTime.Now.ToString("{0:hh:mm:ss:fff}") + ") ZoekBoek() finished");
                    
                    Console.WriteLine("\t Time elapsed: " + timer.ElapsedMilliseconds + " ms");
                    Console.ResetColor();
                    timer.Reset();
                }
                else if (key == "b")
                    Backup();
                else Console.WriteLine("Ongeldige invoer!");
            }
        }
    }
}