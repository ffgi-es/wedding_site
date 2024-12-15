using Microsoft.Azure.Cosmos;
using wedding_site.Data;
using wedding_site.Domain;

namespace wedding_site.RsvpGeneration;

public class Utilities
{
    private static readonly string _chars = "0123456789abcdefghijklmnopqrstuvwxyz";

    public static (string Rsvp, string Passcode) GenerateRsvpCredentials() =>
        (
            string.Join("", Enumerable.Range(1,8).Select(_ => _chars[Random.Shared.Next(_chars.Length)])),
            Random.Shared.Next(1_000, 10_000).ToString()
        );

    public static void PrettyPrintRsvps(IEnumerable<(string Rsvp, string Passcode)> rsvps, int perLine = 3)
    {
        int i = 0;
        foreach(var rsvp in rsvps.Select(r => r.Rsvp))
        {
            i++;
            if (i % perLine == 0) Console.WriteLine(rsvp);
            else Console.Write($"{rsvp} ");
        }
        if (i % perLine != 0) Console.WriteLine();
    }

    public static async Task CreateRsvpsInCosmos(IEnumerable<(string Rsvp, string Passcode)> rsvps)
    {
        var cosmosClient = new CosmosClient(Environment.GetEnvironmentVariable("COSMOS_CONNECTIONSTRING"));
        Console.WriteLine("Connected to DB");

        var rsvpRepo = new RsvpRepo(cosmosClient);

        Console.Write("Creating rsvps in DB");
        foreach (var (rsvp, passcode) in rsvps)
        {
            for (int i=0; i < 3; i++)
            {
                try
                {
                    await rsvpRepo.CreateRsvp(new Rsvp(rsvp, passcode));
                    Console.Write(".");
                    break;
                }
                catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.RequestTimeout)
                {
                    Console.Write("-");
                    if (i > 1) throw;
                    await Task.Delay(1000);
                }
                catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    Console.Write(".");
                    break;
                }
            }
        }
        Console.WriteLine();
    }
}
