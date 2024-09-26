using SixLabors.ImageSharp;
using wedding_site.RsvpGeneration;

var (rsvp, passcode) = Utilities.GenerateRsvpCredentials();

//await Utilities.CreateRsvpInCosmos(rsvp, passcode);

var url = $"https://emilie-alastair-wedding.azurewebsites.net/RSVP/{rsvp}";

var qrBitmapBytes = Utilities.GenerateQrCodeForUrl(url, 8);

using var stream = new MemoryStream(qrBitmapBytes); 

using var image = Image.Load(stream);

var textAdder = new ImageTextAdder(18);

textAdder.AddRsvpText(image, rsvp);
textAdder.AddPasscodeText(image, passcode);

image.SaveAsPng("./test.png");