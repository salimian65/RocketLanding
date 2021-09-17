namespace RocketLanding
{
    public class Point
    {
        public Point(int x, int y, PointStatus pointStatus)
        {
            X = x;
            Y = y;
            PointStatus = pointStatus;
        }

        public int X { get; private set; }

        public int Y { get; private set; }

        public PointStatus PointStatus { get; private set; }

        public void SetStatus(PointStatus pointStatus)
        {
            PointStatus = pointStatus;
        }
    }
}