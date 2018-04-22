namespace WindowsSorter
{
    public struct Position
    {
        public readonly int Left;
        public readonly int Top;
        public readonly int Width;
        public readonly int Height;

        public Position(int left, int top, int width, int height)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }
    }
}