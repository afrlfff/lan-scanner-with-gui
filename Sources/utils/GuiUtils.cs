
namespace lan_scanner.utils
{
    public static class GuiUtils
    {
        // =====================================================================================================
        public static Point GetCenter(Control obj)
        {
            return new Point(
                obj.Location.X + obj.Width / 2,
                obj.Location.Y + obj.Height / 2
                );
        }
        // =====================================================================================================
        public static Point GetLocationByCenter(Control obj, Point objectCenter)
        {
            return new Point(
                objectCenter.X - obj.Width / 2,
                objectCenter.Y - obj.Height / 2
                );
        }
        // =====================================================================================================
        public static Rectangle GetContentBounds(Rectangle container, Padding padding)
        {
            return Rectangle.FromLTRB(
                container.Left + padding.Left,
                container.Top + padding.Top,
                container.Right - padding.Right,
                container.Bottom - padding.Bottom
                );
        }
        // =====================================================================================================
        public static Point GetAlignedPosition(Rectangle container, Size contentSize, ContentAlignment alignment)
        {
            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                    return new Point(
                        container.Left,
                        container.Top);
                case ContentAlignment.TopCenter:
                    return new Point(
                        container.Left + (container.Width - contentSize.Width) / 2,
                        container.Top);
                case ContentAlignment.TopRight:
                    return new Point(
                        container.Right - contentSize.Width,
                        container.Top);
                case ContentAlignment.MiddleLeft:
                    return new Point(
                        container.Left,
                        container.Top + (container.Height - contentSize.Height) / 2);
                case ContentAlignment.MiddleCenter:
                    return new Point(
                        container.Left + (container.Width - contentSize.Width) / 2,
                        container.Top + (container.Height - contentSize.Height) / 2);
                case ContentAlignment.MiddleRight:
                    return new Point(
                        container.Right - contentSize.Width,
                        container.Top + (container.Height - contentSize.Height) / 2);
                case ContentAlignment.BottomLeft:
                    return new Point(
                        container.Left,
                        container.Bottom - contentSize.Height);
                case ContentAlignment.BottomCenter:
                    return new Point(
                        container.Left + (container.Width - contentSize.Width) / 2,
                        container.Bottom - contentSize.Height);
                case ContentAlignment.BottomRight:
                    return new Point(
                        container.Right - contentSize.Width,
                        container.Bottom - contentSize.Height);
                default:
                    return Point.Empty;
            }
        }
        // =====================================================================================================
    }
}
