using System.Net;
using lan_scanner.gui.other;

namespace lan_scanner.gui.widgets
{
    public partial class NetworkDeviceWidget : UserControl
    {
        private int maxDeviceNameLength = 11;
        private string deviceName = "";

        // font size step lower than 1.0f does not guarantee smooth adaptability
        // it depends on PC architecture and physical DPI
        // for instance, on my HP laptop there is no difference between 1.0f and lower values
        // So I will set 0.5f for other PCs
        private float fontSizeStep = 0.5f;

        public EventHandler? Click;

        // =========================================================================================================
        public string DeviceName
        {
            get => this.deviceName;
            set
            {
                bool valueIsIp = IPAddress.TryParse(value, out IPAddress ipAddress);

                if (valueIsIp || value.Length <= maxDeviceNameLength)
                {
                    this.labelDeviceName.Text = value;
                }
                else
                {
                    this.labelDeviceName.Text = value.Substring(0, maxDeviceNameLength - 2) + "...";
                }

                this.deviceName = value;
            }
        }
        // =========================================================================================================
        public Button DeviceButton
        {
            get => this.customButtonDevice;
        }
        // =========================================================================================================
        public Image? DeviceImage
        {
            get => this.customButtonDevice.Image;
            set => this.customButtonDevice.Image = value;
        }
        // =========================================================================================================
        public event EventHandler ButtonClick
        {
            add => this.customButtonDevice.Click += value;
            remove => this.customButtonDevice.Click -= value;
        }
        // =========================================================================================================
        // =========================================================================================================
        public NetworkDeviceWidget()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.customButtonDevice.Click += (s, e) => { this.Click?.Invoke(this, e); };
        }
        // =========================================================================================================
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
        }
        // =========================================================================================================
        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);

            Size maxTextSize = new Size(
                this.customButtonDevice.Width,
                (int)(this.Height * 0.25)
                );

            FitLabel(this.labelDeviceName, maxTextSize);
        }
        // =========================================================================================================
        private Size GetLabelTextSize(Label label)
        {
            using (Graphics g = label.CreateGraphics())
            {
                return TextRenderer.MeasureText(g, label.Text, label.Font);
            }
        }
        // =========================================================================================================
        private void FitLabel(Label label, Size maxTextSize)
        {
            float currentFontSize = label.Font.Size;

            Size textSize = GetLabelTextSize(label);

            if (textSize.Width > maxTextSize.Width || textSize.Height > maxTextSize.Height)
            {
                // reduce font size to make it fit maxTextWidth

                while ((textSize.Width > maxTextSize.Width || (textSize.Height > maxTextSize.Height)) && (currentFontSize > 1.0f))
                {
                    // reduce font size
                    currentFontSize -= this.fontSizeStep;

                    // set new font
                    label.Font = ResourceCache.GetFont(label.Font.Name, currentFontSize, label.Font.Style);

                    // update text size
                    textSize = GetLabelTextSize(label);
                }
            }
            else
            {
                // increase font size to make label fit maxTextWidth

                while (textSize.Width <= maxTextSize.Width && textSize.Height <= maxTextSize.Height)
                {
                    // increase font size
                    currentFontSize += this.fontSizeStep;

                    // set new font
                    label.Font = ResourceCache.GetFont(label.Font.Name, currentFontSize, label.Font.Style);

                    // update text size
                    textSize = GetLabelTextSize(label);
                }

                // set last font size back
                label.Font = new Font(label.Font.Name, currentFontSize - this.fontSizeStep, label.Font.Style);
            }
        }
        // =========================================================================================================
    }
}
