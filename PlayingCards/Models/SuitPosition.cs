namespace PlayingCards.Models
{
    public class SuitPosition
    {
        public double X { get; set; }
        public double Y { get; set; }
        public bool Mirrored { get; set; } = false;

        public SuitPosition(double x = 0, double y = 0, bool mirrored = false)
        {
            X = x;
            Y = y;
            Mirrored = mirrored;
        }
    }
}
