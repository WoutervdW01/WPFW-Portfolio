using pretpark.database.models;
using Microsoft.EntityFrameworkCore;
namespace pretpark.database.administratie;

class DemografischRapport : Rapport
{
    private DatabaseContext context;
    public DemografischRapport(DatabaseContext context) => this.context = context;
    public override string Naam() => "Demografie";
    
    public override async Task<string> Genereer()
    {
        
        string ret = "Dit is een demografisch rapport: \n";
        ret += $"Er zijn in totaal { await AantalGebruikers() } gebruikers van dit platform (dat zijn gasten en medewerkers)\n";
        var dateTime = new DateTime(2000, 1, 1);
        ret += $"Er zijn { await AantalSinds(dateTime) } bezoekers sinds { dateTime }\n";
        if (await AlleGastenHebbenReservering())
            ret += "Alle gasten hebben een reservering\n";
        else
            ret += "Niet alle gasten hebben een reservering\n";
        ret += $"Het percentage bejaarden is { await PercentageBejaarden() }\n";
        /*

        ret += $"De oudste gast heeft een leeftijd van { await HoogsteLeeftijd() } \n";

        ret += "De verdeling van de gasten per dag is als volgt: \n";
        /*
        var dagAantallen = await VerdelingPerDag();
        var totaal = dagAantallen.Select(t => t.aantal).Max();
        foreach (var dagAantal in dagAantallen)
            ret += $"{ dagAantal.dag }: { new string('#', (int)(dagAantal.aantal / (double)totaal * 20)) }\n";

        ret += $"{ await FavorietCorrect() } gasten hebben de favoriete attractie inderdaad het vaakst bezocht. \n";
        */

        return ret;
    }
    private async Task<int> AantalGebruikers() => context.gebruikers.Count();
    
    private async Task<bool> AlleGastenHebbenReservering() => 
        context.gasten.Where(gast => gast.reservering.Count() > 0).Count() == context.gasten.Count();
    
    private async Task<int> AantalSinds(DateTime sinds) =>  await Task<int>.Run(() => {
        return context.gasten.Where(gast => gast.EersteBezoek >= sinds).Count();
    }) ;
   
    private async Task<Gast?> GastBijEmail(string email) =>  await Task<Gast>.Run(() => {
        var res = context.gasten.Where(g => g.Email == email);
        if(res.Count() == 1){
            return res.First();
        } else 
        return null;    
    });
    
    private async Task<Gast?> GastBijGeboorteDatum(DateTime d) {
        var queryResult = await Task.Run(() => context.gasten.Where(gast => gast.GeboorteDatum == d));
        if(queryResult.Count() == 1) return queryResult.First();
        else {
            throw new Exception();
        }
    } 
    
    private async Task<double> PercentageBejaarden() =>  await Task<double>.Run(() => {
        var bejaarden = context.gasten.Where(g => g.GeboorteDatum < DateTime.Today.AddYears(-79));
        double percentage = (double) bejaarden.Count() / context.gasten.Count() * 100;
        return percentage;
    }) ;
    
    /*
    private async Task<int> HoogsteLeeftijd() => await Task<int>.Run(() => {
        var minDateTime = context.gasten.Min(g => g.GeboorteDatum);
        int age = EF.Functions.DateDiffYear(minDateTime, DateTime.Today);
        return age;
    }) ;
    /*
    private async Task<(string dag, int aantal)[]> VerdelingPerDag() =>  ... ;
    
    private async Task<int> FavorietCorrect() => /* ... ; 
    */
}