namespace lan_scanner.gui.ui_objects
{
    partial class TextBoxIPv4
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanelMain = new TableLayoutPanel();
            textBoxOctet1 = new TextBox();
            textBoxOctet4 = new TextBox();
            textBoxOctet3 = new TextBox();
            labelDot1 = new Label();
            textBoxOctet2 = new TextBox();
            labelDot2 = new Label();
            labelDot3 = new Label();
            tableLayoutPanelMain.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.BackColor = Color.White;
            tableLayoutPanelMain.ColumnCount = 9;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 15F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 15F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 15F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 15F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 15F));
            tableLayoutPanelMain.Controls.Add(textBoxOctet4, 7, 0);
            tableLayoutPanelMain.Controls.Add(labelDot3, 6, 0);
            tableLayoutPanelMain.Controls.Add(textBoxOctet3, 5, 0);
            tableLayoutPanelMain.Controls.Add(labelDot2, 4, 0);
            tableLayoutPanelMain.Controls.Add(textBoxOctet2, 3, 0);
            tableLayoutPanelMain.Controls.Add(labelDot1, 2, 0);
            tableLayoutPanelMain.Controls.Add(textBoxOctet1, 1, 0);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 1;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Size = new Size(714, 203);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // textBoxOctet1
            // 
            textBoxOctet1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBoxOctet1.BorderStyle = BorderStyle.None;
            textBoxOctet1.Location = new Point(15, 91);
            textBoxOctet1.Margin = new Padding(0);
            textBoxOctet1.MaxLength = 3;
            textBoxOctet1.Name = "textBoxOctet1";
            textBoxOctet1.Size = new Size(159, 20);
            textBoxOctet1.TabIndex = 0;
            textBoxOctet1.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxOctet4
            // 
            textBoxOctet4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBoxOctet4.BorderStyle = BorderStyle.None;
            textBoxOctet4.Location = new Point(537, 91);
            textBoxOctet4.Margin = new Padding(0);
            textBoxOctet4.MaxLength = 3;
            textBoxOctet4.Name = "textBoxOctet4";
            textBoxOctet4.Size = new Size(159, 20);
            textBoxOctet4.TabIndex = 3;
            textBoxOctet4.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxOctet3
            // 
            textBoxOctet3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBoxOctet3.BorderStyle = BorderStyle.None;
            textBoxOctet3.Location = new Point(363, 91);
            textBoxOctet3.Margin = new Padding(0);
            textBoxOctet3.MaxLength = 3;
            textBoxOctet3.Name = "textBoxOctet3";
            textBoxOctet3.Size = new Size(159, 20);
            textBoxOctet3.TabIndex = 2;
            textBoxOctet3.TextAlign = HorizontalAlignment.Center;
            // 
            // labelDot1
            // 
            labelDot1.AutoSize = true;
            labelDot1.Dock = DockStyle.Fill;
            labelDot1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelDot1.Location = new Point(174, 0);
            labelDot1.Margin = new Padding(0);
            labelDot1.Name = "labelDot1";
            labelDot1.Size = new Size(15, 203);
            labelDot1.TabIndex = 4;
            labelDot1.Text = ".";
            labelDot1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // textBoxOctet2
            // 
            textBoxOctet2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBoxOctet2.BorderStyle = BorderStyle.None;
            textBoxOctet2.Location = new Point(189, 91);
            textBoxOctet2.Margin = new Padding(0);
            textBoxOctet2.MaxLength = 3;
            textBoxOctet2.Name = "textBoxOctet2";
            textBoxOctet2.Size = new Size(159, 20);
            textBoxOctet2.TabIndex = 1;
            textBoxOctet2.TextAlign = HorizontalAlignment.Center;
            // 
            // labelDot2
            // 
            labelDot2.AutoSize = true;
            labelDot2.Dock = DockStyle.Fill;
            labelDot2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelDot2.Location = new Point(348, 0);
            labelDot2.Margin = new Padding(0);
            labelDot2.Name = "labelDot2";
            labelDot2.Size = new Size(15, 203);
            labelDot2.TabIndex = 5;
            labelDot2.Text = ".";
            labelDot2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelDot3
            // 
            labelDot3.AutoSize = true;
            labelDot3.Dock = DockStyle.Fill;
            labelDot3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelDot3.Location = new Point(522, 0);
            labelDot3.Margin = new Padding(0);
            labelDot3.Name = "labelDot3";
            labelDot3.Size = new Size(15, 203);
            labelDot3.TabIndex = 6;
            labelDot3.Text = ".";
            labelDot3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TextBoxIPv4
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanelMain);
            Name = "TextBoxIPv4";
            Size = new Size(714, 203);
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelMain;
        private TextBox textBoxOctet1;
        private TextBox textBoxOctet2;
        private TextBox textBoxOctet3;
        private TextBox textBoxOctet4;
        private Label labelDot1;
        private Label labelDot2;
        private Label labelDot3;
    }
}
