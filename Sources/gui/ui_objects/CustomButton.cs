using System.ComponentModel;
using System.Drawing.Drawing2D;
using CommonUtils = lan_scanner.utils.CommonUtils;
using GuiUtils = lan_scanner.utils.GuiUtils;

namespace lan_scanner.gui.ui_objects
{
    public class CustomButton : Button
    {
        private int _cornerRadiusInPercent = 0;
        private int _textImageSpacing = 0;
        private ContentAlignment _contentAlign = ContentAlignment.MiddleCenter;
        private Color _hoverBackColor;
        private Color _currentBackColor;


        private Rectangle _contentBounds = new Rectangle();
        private double _textHeightShrinkFactor = 0.2; // to avoid standard padding on the top of the text

        // =====================================================================================================
        [Category("Additional")]
        [Description("Выравнивание контента внутри кнопки")]
        [DefaultValue(ContentAlignment.MiddleCenter)]
        public ContentAlignment ContentAlign
        {
            get { return _contentAlign; }
            set
            {
                _contentAlign = value;
                this.Invalidate();
            }
        }
        // =====================================================================================================
        [Category("Additional")]
        [Description("Радиус закругления углов кнопки (%)")]
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
        [Category("Additional")]
        [Description("Зазор между текстом и изображением (px)")]
        [DefaultValue(0)]
        public int TextImageSpacing
        {
            get { return _textImageSpacing; }
            set
            {
                if (value < 0) value = 0;
                _textImageSpacing = value;
                this.Invalidate();
            }
        }
        // =====================================================================================================
        [Category("Additional")]
        [Description("Цвет при наведении курсора на элемент")]
        [DefaultValue(typeof(Color), "Empty")]
        public Color HoverBackColor
        {
            get { return _hoverBackColor; }
            set
            {
                _hoverBackColor = value;
                this.Invalidate();
            }
        }
        // =====================================================================================================

        // =====================================================================================================
        public CustomButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(100, 40);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Regular);
            this.ImageAlign = ContentAlignment.MiddleCenter;
            this.TextImageRelation = TextImageRelation.Overlay;
            this.ForeColor = Color.Black;
            this.BackColor = Color.White;
        }
        // =====================================================================================================
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            _currentBackColor = this.BackColor;
        }
        // =====================================================================================================
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            if (this.HoverBackColor != Color.Empty)
                _currentBackColor = this.HoverBackColor;
        }
        // =====================================================================================================
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _currentBackColor = this.BackColor;
        }
        // =====================================================================================================
        protected override void OnPaint(PaintEventArgs pevent)
        {
            //base.OnPaint(pevent);

            _contentBounds = GuiUtils.GetContentBounds(this.ClientRectangle, this.Padding);

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
                using (SolidBrush backBrush = new SolidBrush(_currentBackColor))
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

            // Draw background image
            if (this.BackgroundImage != null)
            {
                pevent.Graphics.DrawImage(this.BackgroundImage, this.ClientRectangle);
            }

            // Draw Image and Text
            if (this.Image != null || this.Text != "")
            {
                if (this.TextImageRelation != TextImageRelation.Overlay)
                {
                    DrawComplexContent(pevent, this.TextImageRelation);
                }
                else
                {
                    // Draw Image
                    if (this.Image != null)
                        pevent.Graphics.DrawImage(
                            this.Image,
                            new Rectangle(
                                GuiUtils.GetAlignedPosition(
                                    _contentBounds,
                                    this.Image.Size,
                                    this.ContentAlign),
                                this.Image.Size
                                )
                            );

                    // Draw Text
                    if (this.Text != "")
                    {
                        TextRenderer.DrawText(
                            pevent.Graphics,
                            this.Text, this.Font,
                            _contentBounds,
                            this.ForeColor,
                            CommonUtils.ToTextFormatFlags(_contentAlign) | TextFormatFlags.WordBreak
                        );
                    }
                }
            }
        }
        // =====================================================================================================
        private void DrawComplexContent(PaintEventArgs pevent, TextImageRelation textImageRelation)
        {
            if (textImageRelation == TextImageRelation.Overlay) return;

            Size imageSize = (this.Image != null) ? (this.Image.Size) : Size.Empty;
            Size textSize;
            Rectangle contentRect;

            // ===== Calculate contentRect ===== //

            if (textImageRelation == TextImageRelation.ImageBeforeText ||
                textImageRelation == TextImageRelation.TextBeforeImage)
            {
                Size textProposedSize = new Size(
                    Math.Max(0, this.Width - imageSize.Width - _textImageSpacing - this.Padding.Horizontal),
                    this.Height
                    );

                textSize = TextRenderer.MeasureText(
                    this.Text, this.Font, textProposedSize,
                    TextFormatFlags.NoPadding | TextFormatFlags.WordBreak
                    );

                Size contentSize = new Size(
                    Math.Min(_contentBounds.Width, imageSize.Width + _textImageSpacing + textSize.Width),
                    Math.Min(_contentBounds.Height, Math.Max(imageSize.Height, (int)(textSize.Height * _textHeightShrinkFactor)))
                    );

                Point contentLocation = GuiUtils.GetAlignedPosition(
                    _contentBounds, contentSize, this.ContentAlign
                    );

                contentRect = new Rectangle(contentLocation, contentSize);

                // TODO: remove
                    //double k = 0.8;
                    //Rectangle textRect = new Rectangle(
                    //    new Point(contentLocation.X + imageSize.Width + _textImageSpacing, contentLocation.Y + (int)(textSize.Height * (1 - k)) ),
                    //    new Size(textSize.Width, (int)(textSize.Height * k) )
                    //    );
                    //pevent.Graphics.DrawRectangle(new Pen(Color.Red), textRect);
                //
            }
            else
            {
                Size textProposedSize = new Size(
                    this.Width,
                    Math.Max(0, this.Height - imageSize.Height - _textImageSpacing - this.Padding.Vertical)
                );

                textSize = TextRenderer.MeasureText(
                    this.Text, this.Font, textProposedSize,
                    TextFormatFlags.NoPadding | TextFormatFlags.WordBreak
                    );

                Size contentSize = new Size(
                    Math.Min(_contentBounds.Width, Math.Max(imageSize.Width, textSize.Width)),
                    Math.Min(_contentBounds.Height, imageSize.Height + _textImageSpacing + (int)(textSize.Height * _textHeightShrinkFactor))
                    );

                Point contentLocation = GuiUtils.GetAlignedPosition(
                    _contentBounds, contentSize, this.ContentAlign
                    );

                contentRect = new Rectangle(contentLocation, contentSize);
            }

            // ===================================================== //

            Rectangle imageBounds, textArea;
            TextFormatFlags textFlags = TextFormatFlags.WordBreak | TextFormatFlags.NoPadding;

            int textVerticalOffset = (int)(textSize.Height * _textHeightShrinkFactor);

            // ===== Calculate data for content drawing ===== //

            if (textImageRelation == TextImageRelation.ImageBeforeText)
            {
                imageBounds = new Rectangle(
                    GuiUtils.GetAlignedPosition(contentRect, imageSize, ContentAlignment.MiddleLeft),
                    imageSize
                    );

                textArea = Rectangle.FromLTRB(
                    Math.Max(_contentBounds.Left, contentRect.X + imageSize.Width + _textImageSpacing),
                    _contentBounds.Top - textVerticalOffset,
                    _contentBounds.Right,
                    _contentBounds.Bottom);

                textFlags |=
                    TextFormatFlags.Left |
                    CommonUtils.ToTextFormatFlags(CommonUtils.ToVerticalAlignment(this.ContentAlign));
            }
            else if (textImageRelation == TextImageRelation.TextBeforeImage)
            {
                imageBounds = new Rectangle(
                    GuiUtils.GetAlignedPosition(contentRect, imageSize, ContentAlignment.MiddleRight),
                    imageSize
                    );

                textArea = Rectangle.FromLTRB(
                    _contentBounds.Left,
                    _contentBounds.Top - textVerticalOffset,
                    Math.Min(_contentBounds.Right, contentRect.Right - imageSize.Width - _textImageSpacing),
                    _contentBounds.Bottom);

                textFlags |=
                    TextFormatFlags.Right |
                    CommonUtils.ToTextFormatFlags(CommonUtils.ToVerticalAlignment(this.ContentAlign));
            }
            else if (textImageRelation == TextImageRelation.ImageAboveText)
            {
                imageBounds = new Rectangle(
                    GuiUtils.GetAlignedPosition(contentRect, imageSize, ContentAlignment.TopCenter),
                    imageSize
                    );

                textArea = Rectangle.FromLTRB(
                    _contentBounds.Left,
                    Math.Max(_contentBounds.Top, contentRect.Top + imageSize.Height + _textImageSpacing) - textVerticalOffset,
                    _contentBounds.Right,
                    _contentBounds.Bottom);

                textFlags |=
                    TextFormatFlags.Top |
                    CommonUtils.ToTextFormatFlags(CommonUtils.ToHorizontalAlignment(this.ContentAlign));
            }
            else
            {
                imageBounds = new Rectangle(
                    GuiUtils.GetAlignedPosition(contentRect, imageSize, ContentAlignment.BottomCenter),
                    imageSize
                    );

                textArea = Rectangle.FromLTRB(
                    _contentBounds.Left,
                    _contentBounds.Top - textVerticalOffset,
                    _contentBounds.Right,
                     contentRect.Bottom - imageSize.Height - _textImageSpacing);

                textFlags |=
                    TextFormatFlags.Bottom |
                    CommonUtils.ToTextFormatFlags(CommonUtils.ToHorizontalAlignment(this.ContentAlign));
            }

            // ===================================================== //

            // ===== Drawing ===== //

            // TODO: remove
                //Rectangle contentRect2 = new Rectangle(
                //    GuiUtils.GetAlignedPosition(this.ClientRectangle, contentRect.Size, ContentAlignment.MiddleCenter),
                //    contentRect.Size
                //    );
                //pevent.Graphics.DrawRectangle(new Pen(Color.Red), contentRect2);
            //

            if (this.Image != null)
            {
                pevent.Graphics.DrawImage(this.Image, imageBounds);
            }

            if (this.Text != "")
            {
                TextRenderer.DrawText(
                    pevent.Graphics,
                    this.Text, this.Font,
                    textArea,
                    this.ForeColor,
                    textFlags
                    );
            }

            // =================== //
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
        // Hidden Properties ===================================================================================
        // =====================================================================================================
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ContentAlignment TextAlign
        {
            get { return base.TextAlign; }
            set { base.TextAlign = value; }
        }
        // =====================================================================================================
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ContentAlignment ImageAlign
        {
            get { return base.ImageAlign; }
            set { base.ImageAlign = value; }
        }
        // =====================================================================================================
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ImageLayout BackgroundImageLayout
        {
            get { return base.BackgroundImageLayout; }
            set { base.BackgroundImageLayout = value; }
        }
        // =====================================================================================================
    }
}
