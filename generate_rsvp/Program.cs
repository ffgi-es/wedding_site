using QuestPDF.Infrastructure;
using wedding_site.RsvpGeneration;

var number = args.Length > 0 ? int.Parse(args[0]) : 1;

if (number < 1) throw new ArgumentException("Number to generate must be greater than 0");

var storeResults = args.Length > 1 ? bool.Parse(args[1]) : true;

var rsvps = Enumerable.Range(1, number)
    .Select(_ => Utilities.GenerateRsvpCredentials())
    .ToArray();

if (storeResults) await Utilities.CreateRsvpsInCosmos(rsvps);

var qrGenerator = new QrCodeGenerator(12);

qrGenerator.GenerateQrCodes(rsvps);

if (number < 3) return;

QuestPDF.Settings.License = LicenseType.Community;        
var documentGenerator = new DocumentGenerator();
documentGenerator.GenerateRsvpsPdf(rsvps);