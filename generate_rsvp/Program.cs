using QuestPDF.Infrastructure;
using wedding_site.RsvpGeneration;

var rsvps = Enumerable.Range(1, 36)
    .Select(_ => Utilities.GenerateRsvpCredentials())
    .ToArray();

await Utilities.CreateRsvpsInCosmos(rsvps);

var qrGenerator = new QrCodeGenerator(12);

qrGenerator.GenerateQrCodes(rsvps);

QuestPDF.Settings.License = LicenseType.Community;        
var documentGenerator = new DocumentGenerator();
documentGenerator.GenerateRsvpsPdf(rsvps);