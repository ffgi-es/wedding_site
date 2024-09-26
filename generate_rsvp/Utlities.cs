using Microsoft.Azure.Cosmos;
using QRCoder;
using wedding_site.Data;
using wedding_site.Domain;

namespace wedding_site.RsvpGeneration;

public class Utilities {
    public static (string Rsvp, string Passcode) GenerateRsvpCredentials() =>
        (Random.Shared.Next(100_000, 1_000_000).ToString(), Random.Shared.Next(1_000, 10_000).ToString());

    public static async Task CreateRsvpInCosmos(string rsvp, string passcode) {
        var cosmosClient = new CosmosClient(Environment.GetEnvironmentVariable("COSMOS_CONNECTIONSTRING"));

        var rsvpRepo = new RsvpRepo(cosmosClient);

        await rsvpRepo.SaveRsvp(new Rsvp(rsvp, passcode));
    }

    public static byte[] GenerateQrCodeForUrl(string url, int pixels) {
        var data = new PayloadGenerator.Url(url).ToString();

        using var qrCodeGenerator = new QRCodeGenerator();
        using var qrCodeData = qrCodeGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
        using var qrCode = new BitmapByteQRCode(qrCodeData);

        return qrCode.GetGraphic(pixels);
    }

    public static void AddRsvpTextToQr(string rsvp) {

    }
}
