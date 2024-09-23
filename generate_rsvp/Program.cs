using QRCoder;

var rsvp = Random.Shared.Next(100_000, 1_000_000).ToString();

var passcode = Random.Shared.Next(1_000, 10_000).ToString();

var data = new PayloadGenerator.Url($"https://emilie-alastair-wedding.azurewebsites.net/RSVP/{rsvp}").ToString();

using var qrCodeGenerator = new QRCodeGenerator();
using var qrCodeData = qrCodeGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
using var ascii = new AsciiQRCode(qrCodeData);

var output = ascii.GetGraphic(1);

Console.WriteLine(output);