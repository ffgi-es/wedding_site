using Microsoft.Azure.Cosmos;
using QRCoder;
using SixLabors.ImageSharp;
using wedding_site.Data;
using wedding_site.Domain;

var rsvp = Random.Shared.Next(100_000, 1_000_000).ToString();

var passcode = Random.Shared.Next(1_000, 10_000).ToString();

var cosmosClient = new CosmosClient(Environment.GetEnvironmentVariable("COSMOS_CONNECTIONSTRING"));

var rsvpRepo = new RsvpRepo(cosmosClient);

//await rsvpRepo.SaveRsvp(new Rsvp(rsvp, passcode));

var data = new PayloadGenerator.Url($"https://emilie-alastair-wedding.azurewebsites.net/RSVP/{rsvp}").ToString();

using var qrCodeGenerator = new QRCodeGenerator();
using var qrCodeData = qrCodeGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
using var ascii = new AsciiQRCode(qrCodeData);

var output = ascii.GetGraphicSmall();

Console.WriteLine(output);
Console.WriteLine($"passcode: {passcode}");

using var qrCode = new BitmapByteQRCode(qrCodeData);
var bitmapOutput = qrCode.GetGraphic(4);
using var stream = new MemoryStream(bitmapOutput); 

using var image = Image.Load(stream);

image.SaveAsPng("./test.png");