namespace FSH.Starter.Blazor.Infrastructure.Helpers;

public static class QrCodeService
{
    public static byte[] GenerateQrCode(string content)
    {
        using QRCoder.QRCodeGenerator qrGenerator = new QRCoder.QRCodeGenerator();
        QRCoder.QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCoder.QRCodeGenerator.ECCLevel.Q);
        using QRCoder.PngByteQRCode qrCode = new QRCoder.PngByteQRCode(qrCodeData);
        return qrCode.GetGraphic(20);
    }

}
