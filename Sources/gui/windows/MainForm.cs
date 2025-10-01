using System.Diagnostics;
using System.Net;
using lan_scanner.network;
using lan_scanner.utils;
using lan_scanner.gui.models;

namespace lan_scanner.gui.windows
{
    public partial class MainForm : Form
    {
        // =====================================================================================================

        private readonly NetworkDevicesScene? _devicesScene;
        private bool isScanning = false;

        // =====================================================================================================
        public MainForm()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.MainWindowIcon20;

            // set up window
            this.DoubleBuffered = true;

            // initialize scene
            _devicesScene = new NetworkDevicesScene();
            _devicesScene.ScanButtonClicked += OnScanButtonClicked;
            _devicesScene.DeviceButtonCLicked += OnDeviceButtonClicked;

            // initialize scan button
            this.customButtonScan.Click += (s, e) => { this.OnScanButtonClicked(); };

            // add scene to the graphic viewer
            this.graphicsPanel.SetScene(_devicesScene);

            // init and load settings
            this.InitializeSettingsSaving();
            this.LoadSettings();
        }
        // =====================================================================================================
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
        }
        // =====================================================================================================
        private void InitializeSettingsSaving()
        {
            //this.scanSettingsControl.NetworkIpChanged += (s, networkIp) =>
            //{
            //    Properties.Settings.Default.NetworkIp = networkIp;
            //    Properties.Settings.Default.Save();
            //};
        }
        // =====================================================================================================
        private void LoadSettings()
        {
            Debug.WriteLine("Загрузка пользовательских настроек ...");
            //string networkIpStr = Properties.Settings.Default.NetworkIp;
            //this.scanSettingsControl.NetworkIp = networkIpStr;
            Debug.WriteLine("Загрузка пользовательских настроек успешно завершена.");
        }
        // =====================================================================================================
        private async void OnScanButtonClicked()
        {
            Debug.WriteLine("Пользователь нажал на кнопку сканирования сети");

            if (isScanning)
            {
                Debug.WriteLine("Активное сканирование уже запущено.");
                MessageBox.Show("Дождитесь завершения текущего сканирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _devicesScene?.Clear();

            isScanning = true;

            IPAddress? mainInterfaceIp = this.lanInterfacesWidget.MainInterfaceAddress();
            IPAddress? subnetMaskIp = this.lanInterfacesWidget.SubnetMaskAddress();
            IPAddress? networkIp;

            if (mainInterfaceIp == null || subnetMaskIp == null)
            {
                Debug.WriteLine("Пользователь не выбрал сетевой интерфейс для сканирования.");
                MessageBox.Show("Не выбран сетевой интерфейс для сканирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //int maskPrefixLength = NetworkUtils.GetMaskPrefixLength(mask);
            //if (maskPrefixLength == 0) return;
            //int hostsNumber = (int)Math.Pow(2, 32 - maskPrefixLength);

            //this.toolStripStatusLabel.Visible = true;
            //this.toolStripStatusLabel.Text = "Сканирование сети ...";
            //this.toolStripProgressBar.Minimum = 0;
            //this.toolStripProgressBar.Maximum = hostsNumber;
            //this.toolStripProgressBar.Value = 0;
            //this.toolStripProgressBar.Visible = true;

            if (!NetworkUtils.TryGetNetworkAddress(mainInterfaceIp, subnetMaskIp, out networkIp))
            {
                Debug.WriteLine("Обнаружены некорректные адреса хоста или маски подсети");
                MessageBox.Show("Обнаружены некорректные адреса хоста или маски подсети.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 1. Find active hosts
            Dictionary<IPAddress, DeviceInfo> deviceDict = await LanScannerService.ScanSubnet(
                networkIp, subnetMaskIp, mainInterfaceIp 
                );

            // 2. Normalize MAC-addresses + Resolve MAC-address vendors
            foreach ( var deviceInfo in deviceDict.Values )
            {
                deviceInfo.MacAddress = NetworkUtils.NormalizeMacAddress(deviceInfo.MacAddress);
                deviceInfo.MacVendor = MacVendorService.Instance.GetVendor(deviceInfo.MacAddress);
            }

            // 3. Resolve host names
            List<string?> hostNames = await DnsService.GetHostNamesAsync(deviceDict.Keys);
            int i = 0;
            foreach (var ip in deviceDict.Keys)
            {
                if (i < hostNames.Count && hostNames[i] != null)
                    deviceDict[ip].HostName = hostNames[i];
                ++i;
            }

            // TODO: add ports scanning
            // ...

            // TODO: add sending raw SYN TCP sockets and read results
            // ...

            // TODO: add OS fingerprinting
            // ...

            // TODO: add device type analysis
            // ...

            foreach (var device in deviceDict.Values)
            {
                if (device.Ip != null)
                {
                    _devicesScene?.AddDevice(device);
                }
            }

            //this.toolStripStatusLabel.Text = "Сканирование сети завершено.";
            //this.toolStripProgressBar.Visible = false;

            isScanning = false;
        }
        // =====================================================================================================
        private void OnDeviceButtonClicked(object sender, DeviceInfo deviceInfo)
        {
            Debug.WriteLine($"Пользователь нажал на виджет с устройством {deviceInfo.Ip?.ToString()}");

            DeviceInfoDialog dialog = new DeviceInfoDialog();
            dialog.SetDeviceInfo(deviceInfo);
            dialog.ShowDialog();
        }
        // =====================================================================================================
    }
}
