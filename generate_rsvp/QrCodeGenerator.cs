using QRCoder;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;

namespace wedding_site.RsvpGeneration;

public class QrCodeGenerator
{
    private readonly FontCollection _collection = new();
    private readonly FontFamily _family;
    private readonly Font _font;
    private int _size;
    private float _fontSize;

    public QrCodeGenerator(int size) {
        _size = size;
        _fontSize= 2 * size;
        _family = _collection.Add("/usr/share/fonts/TTF/DejaVuSans.ttf");
        _font = _family.CreateFont(_fontSize, FontStyle.Regular);
    }

    public void GenerateQrCodes(IEnumerable<(string Rsvp, string Passcode)> rsvps)
    {
        foreach (var (rsvp, passcode) in rsvps)
        {
            var url = $"https://emilie-alastair-wedding.azurewebsites.net/RSVP/{rsvp}";

            var qrBitmapBytes = GenerateQrCodeForUrl(url, _size);

            using var stream = new MemoryStream(qrBitmapBytes); 

            using var image = Image.Load(stream);

            AddRsvpText(image, rsvp);
            AddPasscodeText(image, passcode);

            image.SaveAsPng($"./qrCodes/{rsvp}.png");
        }
    }

    private static byte[] GenerateQrCodeForUrl(string url, int pixels)
    {
        var data = new PayloadGenerator.Url(url).ToString();

        using var qrCodeGenerator = new QRCodeGenerator();
        using var qrCodeData = qrCodeGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
        using var qrCode = new BitmapByteQRCode(qrCodeData);

        return qrCode.GetGraphic(pixels);
    }

    private void AddRsvpText(Image image, string rsvp) {
        var urlText = $"RSVP: {rsvp}";
        var urlOptions = new RichTextOptions(_font) {
            Origin = new PointF(image.Width/2, _fontSize),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        image.Mutate(img => img.DrawText(urlOptions, urlText, Color.Black));
    }

    private void AddPasscodeText(Image image, string passcode) {
        var urlText = $"Passcode: {passcode}";
        var urlOptions = new RichTextOptions(_font) {
            Origin = new PointF(image.Width/2, image.Height-_fontSize),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        image.Mutate(img => img.DrawText(urlOptions, urlText, Color.Black));
    }
}