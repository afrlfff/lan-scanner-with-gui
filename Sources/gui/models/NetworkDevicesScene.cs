using lan_scanner.utils;
using lan_scanner.network;
using lan_scanner.gui.ui_objects;
using lan_scanner.gui.widgets;

namespace lan_scanner.gui.models
{
    public class NetworkDevicesScene : GraphicScene
    {
        private readonly LinkedList<NetworkDeviceWidget> _devices;

        private readonly Size _scanButtonSize;
        private readonly Size _deviceWidgetSize;
        private readonly Pen _connectionPen;

        private readonly Random _random;

        private int _currentCircleRadius;
        private Queue<Point> _currentCircleAvailableLocations;

        public Action? ScanButtonClicked;
        public EventHandler<DeviceInfo>? DeviceButtonCLicked;

        // =====================================================================================================
        public NetworkDevicesScene() : base()
        {
            _devices = new LinkedList<NetworkDeviceWidget>();
            _scanButtonSize = new Size(100, 100);
            _deviceWidgetSize = new Size(75, 75);
            _connectionPen = new Pen(Color.White);
            _random = new Random();

            _currentCircleRadius = 0;
            _currentCircleAvailableLocations = new Queue<Point>();

            InitializeSceneRect();
            InitializeScanButton();
        }
        // =====================================================================================================
        public void AddDevice(DeviceInfo deviceInfo)
        {
            Point location = GetNextDeviceLocation();

            string widgetLabel = 
                (deviceInfo.HostName != null) ? (deviceInfo.HostName) : 
                (deviceInfo.Ip != null) ? (deviceInfo.Ip.ToString()) : ("");

            NetworkDeviceWidget deviceWidget = new NetworkDeviceWidget
            {
                DeviceName = widgetLabel
            };
            deviceWidget.Click += (s, e) => { this.DeviceButtonCLicked?.Invoke(s, deviceInfo); };

            Point deviceButtonCenter = new Point(
                location.X,
                location.Y - (int)(0.25 / 2 * _deviceWidgetSize.Height)
                );

            AddDrawableObject(new SceneLine(new Point(0, 0), deviceButtonCenter, _connectionPen));
            AddControl(new SceneControl(deviceWidget, location, _deviceWidgetSize));
        }
        // =====================================================================================================
        public override void Clear()
        {
            base.Clear();

            _devices.Clear();
            _currentCircleAvailableLocations.Clear();
            _currentCircleRadius = 0;

            InitializeSceneRect();
        }
        // =====================================================================================================
        private void InitializeSceneRect()
        {
            int maxSide = Math.Max(_scanButtonSize.Width, _scanButtonSize.Height);

            SetMinSceneSize(new Size(maxSide * 4, maxSide * 4));

            // make scene 6 times bigger than ScanButton
            SetSceneRect(new Rectangle((int)(-maxSide * 2), (int)(-maxSide * 2), maxSide * 4, maxSide * 4));
        }
        // =====================================================================================================
        private void InitializeScanButton()
        {
            // scene center
            Point location = new Point(0, 0);

            CustomButton scanButton = new CustomButton
            {
                Size = _scanButtonSize,

                Text = "",
                BackColor = Color.White,
                HoverBackColor = Color.FromArgb(224, 224, 224),
                ForeColor = Color.White,
                CornerRadius = 100,
                BackgroundImage = Properties.Resources.ScanButtonIcon,
                BackgroundImageLayout = ImageLayout.Stretch,
                Location = location
            };

            scanButton.Click += (s, e) => { ScanButtonClicked?.Invoke(); };

            AddPermanentControl(new SceneControl(scanButton, location, _scanButtonSize));
        }
        // =====================================================================================================
        private Point GetNextDeviceLocation()
        {
            if (_currentCircleAvailableLocations.Count == 0)
            {
                // update radius
                if (_currentCircleRadius == 0)
                    _currentCircleRadius = GetFirstCircleRadius();
                else
                { _currentCircleRadius += GetRadiusStep(); }

                // update scene rectangle
                SetSceneRect(new Rectangle(
                    -(_currentCircleRadius + _deviceWidgetSize.Width / 2),
                    -(_currentCircleRadius + _deviceWidgetSize.Height / 2),
                    _currentCircleRadius * 2 + _deviceWidgetSize.Width,
                    _currentCircleRadius * 2 + _deviceWidgetSize.Height
                    ));

                // update available idxs queue
                InitializeDeviceAvailableLocations();
            }

            // return new location
            return _currentCircleAvailableLocations.Dequeue();
        }
        // =====================================================================================================
        private void InitializeDeviceAvailableLocations()
        {
            int numDevices = GetNumDevicesPerCircle(_currentCircleRadius);

            // initialize list of locations for current circle
            List<Point> locationsList = new List<Point>(numDevices);
            for (int i = 0; i < numDevices; ++i)
            {
                double angle = i * (360.0 / numDevices);

                locationsList.Add(GetDeviceWidgetLocation(_currentCircleRadius, angle));
            }

            // shuffle the list to get random location during extraction
            CommonUtils.ShuffleList(locationsList);

            // initialize new list as a queue
            _currentCircleAvailableLocations = new Queue<Point>(locationsList);
        }
        // =====================================================================================================
        private int GetRadiusStep()
        {
            double deviceWidgetDiagonal = CommonUtils.GetDiagonal(_deviceWidgetSize);

            int minPadding = 10;

            return (int)(deviceWidgetDiagonal + minPadding);
        }
        // =====================================================================================================
        private Point GetDeviceWidgetLocation(int circleRadius, double angle)
        {
            return new Point(
                (int)(circleRadius * Math.Cos(angle * Math.PI / 180.0)),
                (int)(circleRadius * Math.Sin(angle * Math.PI / 180.0))
                );
        }
        // =====================================================================================================
        private int GetFirstCircleRadius()
        {
            float scanButtonRadius = _scanButtonSize.Width / 2.0f;
            double deviceWidgetDiagonal = CommonUtils.GetDiagonal(_deviceWidgetSize);

            int minPadding = 10;

            return (int)(scanButtonRadius + deviceWidgetDiagonal / 2.0 + minPadding);
        }
        // =====================================================================================================
        private int GetNumDevicesPerCircle(int radius)
        {
            double circleLength = 2 * Math.PI * radius;
            double deviceWidgetDiagonal = CommonUtils.GetDiagonal(_deviceWidgetSize);

            return (int)(circleLength / deviceWidgetDiagonal);
        }
        // =====================================================================================================
    }
}
