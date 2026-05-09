namespace nShop.Shared;

public class GuidHelpers
{
#if NET9_0_OR_GREATER
        [Obsolete("Use Guid.CreateVersion7() instead")]
#endif
    public static Guid NewGuidVersion7(DateTimeOffset dateTimeOffset)
    {
        var guidBytes = Guid.NewGuid().ToByteArray();
        var unixTimeStamp = dateTimeOffset.ToUnixTimeMilliseconds();

        ArgumentOutOfRangeException.ThrowIfNegative(unixTimeStamp, nameof(unixTimeStamp));

        byte[] unixTimeStampBytes = BitConverter.GetBytes(unixTimeStamp);

        Array.Copy(unixTimeStampBytes, 2, guidBytes, 0, 6);
        guidBytes[6] = (byte)((guidBytes[6] & 0x0F) | (0x70));
        guidBytes[8] = (byte)((guidBytes[8] & 0x3F) | 0x80);

        return new Guid(guidBytes);
    }
    
#if NET9_0_OR_GREATER
        [Obsolete("Use Guid.CreateVersion7() instead")]
#endif
    public static Guid NewGuidVersion7() => NewGuidVersion7(DateTimeOffset.UtcNow);
}