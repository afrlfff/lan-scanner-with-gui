using lan_scanner.network;
using System.Net;

namespace lan_scanner.gui.widgets
{
    public partial class LanInterfacesWidget : UserControl
    {
        private Dictionary<Label, ToolTip> _labelToolTips;
        private int _scrollBarPadding = 10;

        // =====================================================================================================
        public LanInterfacesWidget()
        {
            InitializeComponent();

            this.comboBoxMain.DisplayMember = "Name";
            this.comboBoxMain.SelectedIndexChanged += (s, e) => { OnComboBoxIndexChanged(); };

            this.labelNameText.Text = "...";
            this.labelDescriptionText.Text = "...";
            this.labelTypeText.Text = "...";
            this.labelMainInterfaceText.Text = "...";
            this.labelSubnetMaskText.Text = "...";
            this.labelDefaultGatewayText.Text = "...";

            Label[] labels =
            {
                labelNameText,
                labelDescriptionText,
                labelTypeText,
                labelMainInterfaceText,
                labelSubnetMaskText,
                labelDefaultGatewayText
            };

            _labelToolTips = new Dictionary<Label, ToolTip>(labels.Length);

            foreach (var label in labels)
            {
                _labelToolTips[label] = new ToolTip();
                _labelToolTips[label].SetToolTip(label, "");
                label.TextChanged += (sender, e) =>
                {
                    var lbl = sender as Label;
                    if (lbl != null)
                        UpdateToolTip(lbl, lbl.Text);
                };
            }

            this.Layout += (s, e) => { this.HandleScroll(); };
            this.HandleScroll();

            this.InitializeInterfaces();
        }
        // =====================================================================================================
        private void InitializeInterfaces()
        {
            var allInterfaces = NetworkInterfaceService.GetAllInterfaces(onlyActive: true);
            var lanInterfaces = NetworkInterfaceService.FilterByLan(allInterfaces);

            foreach (var iface in lanInterfaces)
            {
                this.AddInterface(iface);
            }
        }
        // =====================================================================================================
        public void UpdateInterfaces()
        {
            this.InitializeInterfaces();
        }
        // =====================================================================================================
        public IPAddress? SubnetMaskAddress()
        {
            if (this.comboBoxMain.SelectedIndex != -1)
            {
                if (this.comboBoxMain.SelectedItem is NetworkInterfaceInfo info)
                {
                    return info.SubnetMask;
                }
            }

            return null;
        }
        // =====================================================================================================
        public IPAddress? MainInterfaceAddress()
        {
            if (this.comboBoxMain.SelectedIndex != -1)
            {
                if (this.comboBoxMain.SelectedItem is NetworkInterfaceInfo info)
                {
                    return info.MainInterface;
                }
            }

            return null;
        }
        // =====================================================================================================
        private void AddInterface(NetworkInterfaceInfo ifaceInfo)
        {
            this.comboBoxMain.Items.Add(ifaceInfo);

            if (this.comboBoxMain.Items.Count == 1)
            {
                this.comboBoxMain.SelectedIndex = 0;
                UpdateControls(ifaceInfo);
            }
        }
        // =====================================================================================================
        private void OnComboBoxIndexChanged()
        {
            if (this.comboBoxMain.SelectedIndex == -1) return;

            if (this.comboBoxMain.SelectedItem is NetworkInterfaceInfo iface)
            {
                this.UpdateControls(iface);
            }
        }
        // =====================================================================================================
        private void UpdateControls(NetworkInterfaceInfo ifaceInfo)
        {
            string mainInterfaceIp = (ifaceInfo.MainInterface != null) ? (ifaceInfo.MainInterface.ToString()) : ("");
            string subnetMask = (ifaceInfo.SubnetMask != null) ? (ifaceInfo.SubnetMask.ToString()) : ("");
            string defaultGaetway = (ifaceInfo.DefaultGateway != null) ? (ifaceInfo.DefaultGateway.ToString()) : ("");

            this.labelNameText.Text = ifaceInfo.Name;
            this.labelDescriptionText.Text = ifaceInfo.Description;
            this.labelTypeText.Text = ifaceInfo.Type.ToString();
            this.labelMainInterfaceText.Text = mainInterfaceIp;
            this.labelSubnetMaskText.Text = subnetMask;
            this.labelDefaultGatewayText.Text = defaultGaetway;
        }
        // =====================================================================================================
        private void UpdateToolTip(Label label, string text)
        {
            if (label == null) return;

            _labelToolTips[label].SetToolTip(label, text);
        }
        // =====================================================================================================
        private void HandleScroll()
        {
            if (this.MaximumSize.Height == 0) return;

            if (this.ClientRectangle.Height < this.MaximumSize.Height)
            {
                this.tableLayoutPanelInfo.Padding = new Padding(
                    this.tableLayoutPanelInfo.Padding.Left,
                    this.tableLayoutPanelInfo.Padding.Top,
                    _scrollBarPadding,
                    this.tableLayoutPanelInfo.Padding.Bottom
                    );
            }
            else
            {
                this.tableLayoutPanelInfo.Padding = new Padding(
                    this.tableLayoutPanelInfo.Padding.Left,
                    this.tableLayoutPanelInfo.Padding.Top,
                    0,
                    this.tableLayoutPanelInfo.Padding.Bottom
                    );
            }
        }
        // =====================================================================================================
    }
}
