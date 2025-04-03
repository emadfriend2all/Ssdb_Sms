namespace FSH.Starter.WebApi.Zatca.Infrastructure.Utils;

public class PDFA3Result
{
    public bool IsValid { get; internal set; }
    public string ErrorMessage { get; internal set; }
    public string PDFA3FileNameFullPath { get; internal set; }
    public string PDFA3FileName { get; internal set; }
    public byte[] PDFA3ContentFile { get; internal set; }
}
