using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace wedding_site.RsvpGeneration;

public class DocumentGenerator
{
    public string GenerateRsvpsPdf((string Rsvp, string Passcode)[] rsvps)
    {
        var date = DateTime.Now.ToString("u");
        var filename = $"rsvps-{date}.pdf";

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(0.25f, Unit.Centimetre);
                page.Content()
                    .Column(c =>
                    {
                        var blankImage = QrCodeGenerator.BlankImage(100);
                        var i = 0;
                        while (i < rsvps.Length)
                        {
                            c.Item().Row(r => {
                                for (var x=0; x<3; x++)
                                {
                                    if (i < rsvps.Length)
                                        r.RelativeItem().Image($"qrCodes/{rsvps[i].Rsvp}.png");
                                    else
                                        r.RelativeItem().Image(blankImage);
                                    i++;
                                }
                            });
                        }
                    });
            });
        })
        .GeneratePdf(filename);
        return filename;
    }
}