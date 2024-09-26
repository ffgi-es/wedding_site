using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;

namespace wedding_site.RsvpGeneration;

public class ImageTextAdder {
    private readonly FontCollection collection = new();
    private readonly FontFamily family;
    private readonly Font _font;
    private float _size;

    public ImageTextAdder(float size) {
        _size = size;
        family = collection.Add("/usr/share/fonts/TTF/DejaVuSans.ttf");
        _font = family.CreateFont(size, FontStyle.Regular);
    }

    public void AddRsvpText(Image image, string rsvp) {
        var urlText = $"RSVP: {rsvp}";
        var urlOptions = new RichTextOptions(_font) {
            Origin = new PointF(image.Width/2, _size),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        image.Mutate(img => img.DrawText(urlOptions, urlText, Color.Black));
    }

    public void AddPasscodeText(Image image, string passcode) {
        var urlText = $"Passcode: {passcode}";
        var urlOptions = new RichTextOptions(_font) {
            Origin = new PointF(image.Width/2, image.Height-_size),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        image.Mutate(img => img.DrawText(urlOptions, urlText, Color.Black));
    }
}
