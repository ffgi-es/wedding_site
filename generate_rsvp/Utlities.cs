using Microsoft.Azure.Cosmos;
using QRCoder;
using QuestPDF.Infrastructure;
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

    public static async Task CreateRsvpInCosmos(string rsvp, string passcode)
    {
        var cosmosClient = new CosmosClient(Environment.GetEnvironmentVariable("COSMOS_CONNECTIONSTRING"));

        var rsvpRepo = new RsvpRepo(cosmosClient);

        await rsvpRepo.SaveRsvp(new Rsvp(rsvp, passcode));
    }
}
