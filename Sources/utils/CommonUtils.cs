
namespace lan_scanner.utils
{
    using ContentAlignment = System.Drawing.ContentAlignment;
    using VerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment;
    using HorizontalAlignment = System.Windows.Forms.VisualStyles.HorizontalAlign;

    public static class CommonUtils
    {
        // =====================================================================================================
        public static Point GetCenter(Rectangle rect)
        {
            return new Point(
                rect.X + rect.Width / 2,
                rect.Y + rect.Height / 2
                );
        }
        // =====================================================================================================
        public static Rectangle GetRectangleFromCenter(Point location, Size size)
        {
            return new Rectangle(
                location.X - size.Width / 2,
                location.Y - size.Height / 2,
                size.Width, size.Height
                );
        }
        // =====================================================================================================
        public static Size Multiply(Size s1, Size s2)
        {
            return new Size(
                (s1.Width * s2.Width),
                (s1.Height * s2.Height)
                );
        }
        // =====================================================================================================
        public static SizeF Multiply(Size s1, SizeF s2)
        {
            return new SizeF(
                (s1.Width * s2.Width),
                (s1.Height * s2.Height)
                );
        }
        // =====================================================================================================
        public static SizeF Multiply(SizeF s1, Size s2)
        {
            return new SizeF(
                (s1.Width * s2.Width),
                (s1.Height * s2.Height)
                );
        }
        // =====================================================================================================
        public static SizeF Multiply(SizeF s1, SizeF s2)
        {
            return new SizeF(
                (s1.Width * s2.Width),
                (s1.Height * s2.Height)
                );
        }
        // =====================================================================================================
        public static SizeF Divide(Size s1, Size s2)
        {
            return new SizeF(
                (s1.Width / s2.Width),
                (s1.Height / s2.Height)
                );
        }
        // =====================================================================================================
        public static Point Add(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }
        // =====================================================================================================
        public static double GetDiagonal(int width, int height)
        {
            return Math.Sqrt(width * width + height * height);
        }
        // =====================================================================================================
        public static double GetDiagonal(Size s)
        {
            return Math.Sqrt(s.Width * s.Width + s.Height * s.Height);
        }
        // =====================================================================================================
        public static void ShuffleList<T>(List<T> list)
        {
            var random = Random.Shared;
            
            // Shuffle with Fisher-Yets algorithm
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]); // exchange
            }
        }
        // =====================================================================================================
        public static VerticalAlignment ToVerticalAlignment(ContentAlignment align)
        {
            switch (align)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopRight:
                    return VerticalAlignment.Top;

                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomRight:
                    return VerticalAlignment.Bottom;

                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:
                default:
                    return VerticalAlignment.Center;
            }
        }
        // =====================================================================================================
        public static HorizontalAlignment ToHorizontalAlignment(ContentAlignment align)
        {
            switch (align)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                    return HorizontalAlignment.Left;

                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    return HorizontalAlignment.Right;

                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                default:
                    return HorizontalAlignment.Center;
            }
        }
        // =====================================================================================================
        public static TextFormatFlags ToTextFormatFlags(ContentAlignment textAlign)
        {
            switch (textAlign)
            {
                case ContentAlignment.TopLeft:
                    return TextFormatFlags.Top | TextFormatFlags.Left;
                case ContentAlignment.TopCenter:
                    return TextFormatFlags.Top | TextFormatFlags.HorizontalCenter;
                case ContentAlignment.TopRight:
                    return TextFormatFlags.Top | TextFormatFlags.Right;
                case ContentAlignment.MiddleLeft:
                    return TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                case ContentAlignment.MiddleCenter:
                    return TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;
                case ContentAlignment.MiddleRight:
                    return TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
                case ContentAlignment.BottomLeft:
                    return TextFormatFlags.Bottom | TextFormatFlags.Left;
                case ContentAlignment.BottomCenter:
                    return TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
                case ContentAlignment.BottomRight:
                    return TextFormatFlags.Bottom | TextFormatFlags.Right;
                default:
                    return TextFormatFlags.Default;
            }
        }
        // =====================================================================================================
        public static TextFormatFlags ToTextFormatFlags(VerticalAlignment textAlign)
        {
            switch (textAlign)
            {
                case VerticalAlignment.Top:
                    return TextFormatFlags.Top;
                case VerticalAlignment.Center:
                    return TextFormatFlags.VerticalCenter;
                case VerticalAlignment.Bottom:
                    return TextFormatFlags.Bottom;
                default:
                    return TextFormatFlags.VerticalCenter;
            }
        }
        // =====================================================================================================
        public static TextFormatFlags ToTextFormatFlags(HorizontalAlignment textAlign)
        {
            switch (textAlign)
            {
                case HorizontalAlignment.Left:
                    return TextFormatFlags.Left;
                case HorizontalAlignment.Center:
                    return TextFormatFlags.HorizontalCenter;
                case HorizontalAlignment.Right:
                    return TextFormatFlags.Right;
                default:
                    return TextFormatFlags.HorizontalCenter;
            }
        }
        // =====================================================================================================
    }
}
