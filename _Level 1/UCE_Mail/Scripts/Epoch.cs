using System;

public static class Epoch
{
    private static readonly DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static long Current()
    {
        return (long)(DateTime.UtcNow - epochStart).TotalMilliseconds;
    }

    public static long DateTimeToEpoch(DateTime when)
    {
        return (long)(when - epochStart).TotalMilliseconds;
    }

    public static DateTime EpochToDateTime(long epoch)
    {
        return epochStart.AddMilliseconds(epoch);
    }
}