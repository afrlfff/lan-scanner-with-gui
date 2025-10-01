using lan_scanner.utils;
using lan_scanner.gui.models;
using lan_scanner.gui.other;

namespace lan_scanner.gui.ui_objects
{
    public class GraphicsPanel : Panel
    {
        private GraphicScene? _scene;

        // offset to move scene to the center of the panel
        private Point _basicOffset;

        // scene local offset
        private Point _sceneOffset;
        private float _sceneScale;

        // using for resize with no rounding errors
        private Size _initialSize;

        // transformation cache
        private Transformation _transformation;

        // =====================================================================================================
        public GraphicsPanel()
        {
            _sceneOffset = new Point(0, 0);
            _sceneScale = 1.0f;
            _initialSize = new Size(0, 0);
            _basicOffset = new Point(0, 0);

            _transformation = new Transformation(_sceneScale, CommonUtils.Add(_basicOffset, _sceneOffset));
        }
        // =====================================================================================================
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            _initialSize = this.Size;
            _basicOffset = new Point(this.Width / 2, this.Height / 2);

            _transformation = new Transformation(_sceneScale, CommonUtils.Add(_basicOffset, _sceneOffset));
        }
        // =====================================================================================================
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_scene != null)
            {
                // draw drawable objects
                foreach (var obj in _scene.DrawableObjects)
                {
                    obj.DrawTransformed(e.Graphics, _transformation);
                }
            }
        }
        // =====================================================================================================
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            _basicOffset = new Point(this.Width / 2, this.Height / 2);

            // update scaler and transformation
            this.UpdateSceneScale();

            // redraw scene controls with new transformation
            this.UpdateSceneControls();

            // to draw drawable objects
            this.Invalidate();
        }
        // =====================================================================================================
        public void SetScene(GraphicScene scene)
        {
            _scene = scene;

            // update scaler before add objects to the grphic view
            this.UpdateSceneScale();

            // intialize default controls
            foreach (var control in scene.Controls)
            {
                this.InitializeControl(control);
            }

            // initialize permanent controls
            foreach (var control in scene.PermanentControls)
            {
                this.InitializeControl(control);
            }

            // draw drawable object if they exist
            if (_scene.DrawableObjects.Count > 0)
            {
                this.Invalidate();
            }

            // track new controls to initialize them
            _scene.SceneControlAdded += (s, c) => { this.InitializeControl(c); };

            // track new drawable objects to draw them immediately
            _scene.SceneDrawableObjectAdded += (s, c) => { this.Invalidate(); };

            // clear controls from grpahic view when scene is cleared
            _scene.SceneCleared += () => { OnSceneClear(); };

            // redraw scene when scene bounding rectangle was changed
            _scene.SceneRectChanged += () => {
                this.UpdateSceneScale();
                this.Invalidate(); 
                this.UpdateSceneControls(); 
            };
        }
        // =====================================================================================================
        private void OnSceneClear()
        {
            this.Invalidate();
            this.SuspendLayout();

            try
            {
                var permanentControls = _scene.PermanentControls.Select(pc => pc.ControlUI).ToHashSet();

                var toRemove = this.Controls
                    .Cast<Control>()
                    .Where(c => !permanentControls.Contains(c))
                    .ToList();

                foreach (var ctrl in toRemove)
                {
                    this.Controls.Remove(ctrl);
                    ctrl.Dispose();
                }
            }
            finally
            {
                this.ResumeLayout();
            }
        }
        // =====================================================================================================
        private void InitializeControl(SceneControl control)
        {
            // transorm to the grpahic view before add to the UI
            control.Transform(_transformation);

            // adds control to the panel and draws it automatically
            this.Controls.Add(control.ControlUI);
        }
        // =====================================================================================================
        private void UpdateSceneControls()
        {
            if (_scene != null)
            {
                foreach (var control in _scene.Controls)
                {
                    control.Transform(_transformation);
                }

                foreach (var control in _scene.PermanentControls)
                {
                    control.Transform(_transformation);
                }
            }
        }
        // =====================================================================================================
        private void UpdateSceneScale()
        {
            if (_scene != null)
            {
                int panelMinSide = Math.Min(this.Width, this.Height);
                int sceneMaxSide = Math.Max(this._scene.SceneRect.Width, this._scene.SceneRect.Height);

                _sceneScale = (float)panelMinSide / sceneMaxSide;
            }

            _transformation = new Transformation(_sceneScale, CommonUtils.Add(_basicOffset, _sceneOffset));
        }
        // =====================================================================================================
    }
}
