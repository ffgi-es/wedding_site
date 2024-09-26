using QuestPDF.Infrastructure;
using wedding_site.RsvpGeneration;

var number = args.Length > 0 ? int.Parse(args[0]) : 1;

if (number < 1) throw new ArgumentException("Number to generate must be greater than 0");

var storeResults = args.Length > 1 ? bool.Parse(args[1]) : true;

Console.WriteLine($"Generating {number} of rsvps");

var rsvps = Enumerable.Range(1, number)
    .Select(_ => Utilities.GenerateRsvpCredentials())
    .ToArray();

Console.WriteLine("-----------------------------------------");
Console.WriteLine($"Generated: ");
Utilities.PrettyPrintRsvps(rsvps);
Console.WriteLine("-----------------------------------------");

if (storeResults) {
    Console.WriteLine("Storing rsvps in the database");
    await Utilities.CreateRsvpsInCosmos(rsvps);
    Console.WriteLine("Rsvps stored");
}

Console.WriteLine("-----------------------------------------");

Console.WriteLine("Generating QR Codes");
var qrGenerator = new QrCodeGenerator(12);
qrGenerator.GenerateQrCodes(rsvps);
Console.WriteLine("QR Codes generated");

if (number < 3) return;

Console.WriteLine("-----------------------------------------");
Console.WriteLine("Generating PDF of QR codes");
QuestPDF.Settings.License = LicenseType.Community;        
var documentGenerator = new DocumentGenerator();
var file = documentGenerator.GenerateRsvpsPdf(rsvps);
Console.WriteLine($"PDF Generated: {file}");