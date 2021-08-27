namespace WhackAStoodent.Client.Networking.Messages
{
    public enum EHoleIndex
    {
        TopLeft = 0,
        TopRight = 1,
        BottomRight = 2,
        BottomLeft = 3,
    }

    public static class EHoleIndexExtension
    {
        public static int Index(this EHoleIndex holeIndex) => (int) holeIndex;
    }
}