using System.Text;

namespace FSH.Starter.WebApi.Zatca.Infrastructure.Tags;

public class Tag
{
    public int TagId { get; set; }
    public string Value { get; set; }

    public Tag(int tagId, string value)
    {
        TagId = tagId;
        Value = value ?? string.Empty;
    }

    public int GetLength() => Encoding.UTF8.GetByteCount(Value);

    public override string ToString()
    {
        return ToHex(TagId) + ToHex(GetLength()) + Value;
    }

    private static string ToHex(int value)
    {
        return Convert.ToHexString(new byte[] { (byte)value });
    }
}
