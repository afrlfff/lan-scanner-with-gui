
namespace lan_scanner.gui.other
{
    public class Transformation
    {
        private readonly float _scale;
        private readonly Point _offset;

        public Transformation(float scale, Point offset)
        {
            _scale = scale;
            _offset = offset;
        }

        public Point Apply(Point p)
        {
            return new Point(
                (int)(p.X * _scale + _offset.X),
                (int)(p.Y * _scale + _offset.Y)
            );
        }

        public Size Apply(Size s)
        {
            return new Size(
                (int)(s.Width * _scale),
                (int)(s.Height * _scale)
            );
        }
    }
}
