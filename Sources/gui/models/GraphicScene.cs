using lan_scanner.utils;
using lan_scanner.gui.other;

namespace lan_scanner.gui.models
{
    public abstract class SceneObject
    {
        public abstract Point Location { get; set; }
        public abstract Size Size { get; set; }
    }
    // =========================================================================================================
    public class SceneControl : SceneObject
    {
        // local varaibles which are using only within the scene
        private Point _sceneLocation;
        private Size _scenelSize;

        // control for scene viewer to use
        public Control ControlUI { get; private set; }

        public SceneControl(Control control, Point initialLocation, Size initialSize)
        {
            ControlUI = control;

            Location = initialLocation;
            Size = initialSize;

            SizeUI = initialSize;
            LocationUI = initialLocation;
        }

        public override Point Location
        {
            get => _sceneLocation;
            set => _sceneLocation = value;
        }

        public override Size Size
        {
            get => _scenelSize;
            set => _scenelSize = value;
        }

        public Point LocationUI
        {
            get => new Point(
                ControlUI.Location.X + ControlUI.Width / 2,
                ControlUI.Location.Y + ControlUI.Height / 2);

            set => ControlUI.Location = new Point(
                value.X - SizeUI.Width / 2,
                value.Y - SizeUI.Height / 2);
        }

        public Size SizeUI
        {
            get => ControlUI.Size;
            set => ControlUI.Size = value;
        }

        public void Transform(Transformation transform)
        {
            SizeUI = transform.Apply(Size);
            LocationUI = transform.Apply(Location);
        }
    }
    // =========================================================================================================
    public abstract class SceneDrawableObject : SceneObject
    {
        public Rectangle BoundingRect { get; protected set; }

        public abstract void Draw(Graphics g);

        public abstract void DrawTransformed(Graphics g, Transformation transform);
        protected abstract void Move(Point offset);
        protected abstract void Resize(Size size);

        public override Point Location
        {
            get => CommonUtils.GetCenter(BoundingRect);
            set
            {
                Move(new Point(value.X - Location.X, value.Y - Location.Y));
                BoundingRect = CommonUtils.GetRectangleFromCenter(value, Size);
            }
        }

        public override Size Size
        {
            get => BoundingRect.Size;
            set
            {
                Resize(value);
                BoundingRect = CommonUtils.GetRectangleFromCenter(Location, value);
            }
        }
    }
    // =========================================================================================================
    public class SceneLine : SceneDrawableObject
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public Pen Pen { get; set; }

        public SceneLine(Point start, Point end, Pen pen)
        {
            StartPoint = start;
            EndPoint = end;
            Pen = pen;
        }

        public override void Draw(Graphics g)
        {
            g.DrawLine(Pen, StartPoint, EndPoint);
        }

        public override void DrawTransformed(Graphics g, Transformation transform)
        {
            g.DrawLine(Pen, transform.Apply(StartPoint), transform.Apply(EndPoint));
        }

        protected override void Move(Point offset)
        {
            StartPoint = CommonUtils.Add(StartPoint, offset);
            EndPoint = CommonUtils.Add(EndPoint, offset);
        }

        protected override void Resize(Size size)
        {
            Size sizeDiff = size - BoundingRect.Size;

            StartPoint = new Point(
                StartPoint.X - sizeDiff.Width / 2,
                StartPoint.Y - sizeDiff.Height / 2
                );
            EndPoint = new Point(
                EndPoint.X + sizeDiff.Width / 2,
                EndPoint.Y + sizeDiff.Height / 2
                );
        }
    }
    // =========================================================================================================
    public class GraphicScene
    {
        public List<SceneDrawableObject> DrawableObjects { get; private set; }
        public List<SceneControl> Controls { get; private set; }
        public List<SceneControl> PermanentControls { get; private set; }

        public Rectangle SceneRect { get; private set; }

        private Size _minSceneSize;

        public event EventHandler<SceneControl>? SceneControlAdded;
        public event EventHandler<SceneDrawableObject>? SceneDrawableObjectAdded;
        public event Action? SceneRectChanged;
        public event Action? SceneCleared;

        // =====================================================================================================
        public GraphicScene()
        {
            DrawableObjects = new List<SceneDrawableObject>();
            Controls = new List<SceneControl>();
            PermanentControls = new List<SceneControl>();

            // initial value
            SceneRect = new Rectangle(0, 0, 0, 0);

            _minSceneSize = new Size(0, 0);
        }
        // =====================================================================================================
        public void AddControl(SceneControl control)
        {
            Controls.Add(control);

            SceneControlAdded?.Invoke(this, control);
        }
        // =====================================================================================================
        public void AddPermanentControl(SceneControl control)
        {
            PermanentControls.Add(control);

            SceneControlAdded?.Invoke(this, control);
        }
        // =====================================================================================================
        public void AddDrawableObject(SceneDrawableObject obj)
        {
            DrawableObjects.Add(obj);

            SceneDrawableObjectAdded?.Invoke(this, obj);
        }
        // =====================================================================================================
        public void SetSceneRect(Rectangle rect)
        {
            Size rectSize = new Size(
                rect.Width < _minSceneSize.Width ? _minSceneSize.Width : rect.Width,
                rect.Height < _minSceneSize.Height ? _minSceneSize.Height : rect.Height
                );

            SceneRect = new Rectangle(rect.Location, rectSize);

            SceneRectChanged?.Invoke();
        }
        // =====================================================================================================
        public void SetMinSceneSize(Size s)
        {
            _minSceneSize = s;
        }
        // =====================================================================================================
        public virtual void Clear()
        {
            DrawableObjects.Clear();
            Controls.Clear();

            SceneCleared?.Invoke();
        }
        // =====================================================================================================
    }
    // =========================================================================================================
}
