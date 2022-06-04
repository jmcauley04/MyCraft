namespace MyGameEngine.Shared.Records;

public record ColorRecord
{
    public byte R { get; init; }
    public byte G { get; init; }
    public byte B { get; init; }
    public int X { get; init; }
    public int Y { get; init; }
    public int Z { get; init; }

    public ColorRecord(byte r, byte g, byte b, int x = 0, int y = 0, int z = 0)
    {
        R = r;
        G = g;
        B = b;

        X = x;
        Y = y;
        Z = z;
    }
}
