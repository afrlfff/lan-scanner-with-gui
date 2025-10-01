using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace lan_scanner.gui.ui_objects
{
    public class CustomPanel : Panel
    {
        private int _cornerRadiusInPercent = 0;

        // =====================================================================================================
        [Category("Additional")]
        [Description("Радиус закругления углов")]
        [DefaultValue(0)]
        public int CornerRadius
        {
            get { return _cornerRadiusInPercent; }
            set
            {
                if (value < 0) value = 0;
                if (value > 100) value = 100;
                _cornerRadiusInPercent = value;
                this.Invalidate();
            }
        }
        // =====================================================================================================

        // =====================================================================================================
        public CustomPanel()
        {
            this.Size = new Size(100, 100);
            this.ForeColor = Color.Black;
            this.BackColor = Color.White;
        }
        // =====================================================================================================
        protected override void OnPaint(PaintEventArgs pevent)
        {
            //base.OnPaint(pevent);

            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pevent.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            int maxRadius = Math.Min(Width, Height) / 2;
            int currentRaduis = (int)(CornerRadius / 100.0f * maxRadius);

            int effectiveRadius = Math.Min(currentRaduis, maxRadius);
            int arcSize = effectiveRadius * 2;

            RectangleF rectSurface = new RectangleF(0, 0, this.Width, this.Height);

            // Draw Shape
            using (GraphicsPath path = GetEllipsePath(rectSurface, arcSize))
            {
                // Bacakground
                using (SolidBrush backBrush = new SolidBrush(this.BackColor))
                {
                    pevent.Graphics.FillPath(backBrush, path);
                }

                // Main contour
                Color backColor = (this.Parent != null) ? (this.Parent.BackColor) : this.BackColor;
                using (Pen backPen = new Pen(backColor, 2))
                {
                    this.Region = new Region(path);
                    pevent.Graphics.DrawPath(backPen, path);
                }
            }
        }
        // =====================================================================================================
        private GraphicsPath GetEllipsePath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();

            if (radius <= 0)
            {
                path.AddRectangle(rect);
            }
            else
            {
                path.StartFigure();
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();
            }

            return path;
        }
        // =====================================================================================================
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate();
        }
        // =====================================================================================================
    }
}
