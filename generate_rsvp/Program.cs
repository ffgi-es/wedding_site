using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using wedding_site.RsvpGeneration;

var (rsvp, passcode) = Utilities.GenerateRsvpCredentials();

//await Utilities.CreateRsvpInCosmos(rsvp, passcode);

var qrGenerator = new QrCodeGenerator(12);

qrGenerator.GenerateQrCodes([(rsvp, passcode)]);

QuestPDF.Settings.License = LicenseType.Community;        
Document.Create(container =>
{
    container.Page(page =>
    {
        page.Size(PageSizes.A4);
        page.Content()
            .Column(c =>
            {
                c.Item().Row(r => {
                    r.RelativeItem().Image($"qrCodes/{rsvp}.png");
                    r.RelativeItem().Image($"qrCodes/{rsvp}.png");
                    r.RelativeItem().Image($"qrCodes/{rsvp}.png");
                });
                c.Item().Row(r => {
                    r.RelativeItem().Image($"qrCodes/{rsvp}.png");
                    r.RelativeItem().Image($"qrCodes/{rsvp}.png");
                    r.RelativeItem().Image($"qrCodes/{rsvp}.png");
                });
                c.Item().Row(r => {
                    r.RelativeItem().Image($"qrCodes/{rsvp}.png");
                    r.RelativeItem().Image($"qrCodes/{rsvp}.png");
                    r.RelativeItem().Image($"qrCodes/{rsvp}.png");
                });
                c.Item().Row(r => {
                    r.RelativeItem().Image($"qrCodes/{rsvp}.png");
                    r.RelativeItem().Image($"qrCodes/{rsvp}.png");
                    r.RelativeItem().Image($"qrCodes/{rsvp}.png");
                });
            });
    });
})
.GeneratePdf("test.pdf");