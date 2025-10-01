using lan_scanner.network;

namespace lan_scanner.gui.windows
{
    public partial class DeviceInfoDialog : Form
    {

        // =====================================================================================================
        public DeviceInfoDialog()
        {
            InitializeComponent();

            this.labelIpText.Text = "?";
            this.labelNameText.Text = "?";
            this.labelMacText.Text = "?";
            this.labelMacVendorText.Text = "?";

            this.customButtonOk.Click += (s, e) => { OnButtonOkClick(); };
        }
        // =====================================================================================================
        public void SetDeviceInfo(DeviceInfo deviceInfo)
        {
            if (deviceInfo.Ip != null)
                this.labelIpText.Text = deviceInfo.Ip.ToString();
            if (deviceInfo.HostName != null)
                this.labelNameText.Text = deviceInfo.HostName;
            if (deviceInfo.MacAddress != null)
                this.labelMacText.Text = deviceInfo.MacAddress;
            if (deviceInfo.MacVendor != null)
                this.labelMacVendorText.Text = deviceInfo.MacVendor;
        }
        // =====================================================================================================        
        private void OnButtonOkClick()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        // =====================================================================================================
    }
}
