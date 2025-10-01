
namespace lan_scanner.gui.ui_objects
{
    public partial class TextBoxIPv4 : UserControl
    {
        private readonly List<TextBox> _octetTextBoxes;
        private readonly List<Label> _dotLabels;

        public event EventHandler? TextChanged;

        // =====================================================================================================
        public string IPAddress
        {
            get => this.GetIpAdrress();
            set => this.SetIpAddress(value);
        }
        // =====================================================================================================
        public TextBoxIPv4()
        {
            InitializeComponent();

            this.MinimumSize = new Size(38 * 4, 27);
            this.MaximumSize = new Size(75 * 4, 27);
            this.BackColor = SystemColors.Window;
            this.AutoSize = false;

            this._octetTextBoxes = new List<TextBox>(4)
            {
                this.textBoxOctet1,
                this.textBoxOctet2,
                this.textBoxOctet3,
                this.textBoxOctet4
            };

            this._dotLabels = new List<Label>(3)
            {
                this.labelDot1,
                this.labelDot2,
                this.labelDot3
            };

            InitializeEvents();
        }
        // =====================================================================================================
        private void InitializeEvents()
        {
            for (int i = 0; i < _octetTextBoxes.Count; ++i)
            {
                int index = i;
                _octetTextBoxes[i].KeyPress += (s, e) => OnOctetKeyPress(s, e, index);
                _octetTextBoxes[i].KeyDown += (s, e) => OnOctetKeyDown(s, e, index);
                _octetTextBoxes[i].TextChanged += (s, e) => { this.TextChanged?.Invoke(this, e); };
            }

            for (int i = 0; i < _dotLabels.Count; ++i)
            {
                int index = i;
                _dotLabels[i].MouseClick += (s, e) => OnDotLabelClick(s, e, index);
            }
        }
        // =====================================================================================================
        private string GetIpAdrress()
        {
            string ipAddressString = this._octetTextBoxes[0].Text;

            for (int i = 1; i < 4; ++i)
                ipAddressString += '.' + this._octetTextBoxes[i].Text;

            return ipAddressString;
        }
        // =====================================================================================================
        private bool SetIpAddress(string ipString)
        {
            if (ipString == "")
            {
                foreach (var octet in _octetTextBoxes)
                    octet.Text = "";
                return true;
            }
            if (Validate(ipString))
            {
                var octets = ipString.Split('.');
                for (int i = 0; i < 4; ++i)
                    _octetTextBoxes[i].Text = octets[i];

                return true;
            }
            return false;
        }
        // =====================================================================================================
        private bool Validate(string ipString)
        {
            int dotsNumber = 0;
            int octetLengthCounter = 0;
            foreach (var c in ipString)
            {
                if (c == '.')
                {
                    if (++dotsNumber > 3) return false;
                    octetLengthCounter = 0;
                }
                else
                {
                    int digit = c - '0';
                    if (digit < 0 || digit > 9) return false;
                    if (++octetLengthCounter > 3) return false;
                }
            }

            return true;
        }
        // =====================================================================================================
        private void OnDotLabelClick(object sender, MouseEventArgs e, int dotIdx)
        {
            if (sender is Label label)
            {
                int x = e.X;
                int labelCenterX = label.Width / 2;

                TextBox currentTextBox;
                if (x < labelCenterX)
                    currentTextBox = _octetTextBoxes[dotIdx];
                else
                    currentTextBox = _octetTextBoxes[dotIdx + 1];

                currentTextBox.Focus();

                int textBoxCenterX = currentTextBox.Location.X + currentTextBox.Width / 2;

                if (x < textBoxCenterX)
                    currentTextBox.SelectionStart = 0;
                else
                    currentTextBox.SelectionStart = currentTextBox.Text.Length;
                currentTextBox.SelectionLength = 0;
            }
        }
        // =====================================================================================================
        private void OnOctetKeyPress(object sender, KeyPressEventArgs e, int octetIdx)
        {
            if (e.KeyChar == '.')
            {
                if (octetIdx < (4 - 1) && sender is TextBox octetTextBox)
                {
                    TextBox nextOctetTextBox = this._octetTextBoxes[octetIdx + 1];
                    nextOctetTextBox.Focus();
                    nextOctetTextBox.SelectionStart = nextOctetTextBox.Text.Length;
                    e.Handled = true;
                }
            }
            else if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        // =====================================================================================================
        private void OnOctetKeyDown(object sender, KeyEventArgs e, int octetIdx)
        {
            if (sender is not TextBox) return;

            TextBox octetTextBox = sender as TextBox;

            if (e.KeyCode == Keys.Left
                && octetIdx > 0
                && octetTextBox.SelectionStart == 0)
            {
                this._octetTextBoxes[octetIdx - 1].Focus();
                this._octetTextBoxes[octetIdx - 1].SelectionStart = this._octetTextBoxes[octetIdx - 1].Text.Length;
            }
            if (e.KeyCode == Keys.Right
                && octetIdx < 4 - 1
                && octetTextBox.SelectionStart == octetTextBox.Text.Length)
            {
                this._octetTextBoxes[octetIdx + 1].Focus();
                this._octetTextBoxes[octetIdx + 1].SelectionStart = 0;
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.PageUp)
            {
                this._octetTextBoxes[0].Focus();
                this._octetTextBoxes[0].SelectionStart = 0;
            }
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.PageDown)
            {
                this._octetTextBoxes[4 - 1].Focus();
                this._octetTextBoxes[4 - 1].SelectionStart = this._octetTextBoxes[4 - 1].Text.Length;
            }
            if (e.KeyCode == Keys.Home)
            {
                octetTextBox.SelectionStart = 0;
            }
            if (e.KeyCode == Keys.End)
            {
                octetTextBox.SelectionStart = octetTextBox.Text.Length;
            }
        }
        // =====================================================================================================
        public bool Validate()
        {
            foreach (TextBox octetTextBox in this._octetTextBoxes)
            {
                int value = int.Parse(octetTextBox.Text);
                if (!(value >= 0 && value <= 255)) return false;
            }
            return true;
        }
        // =====================================================================================================
    }
}
