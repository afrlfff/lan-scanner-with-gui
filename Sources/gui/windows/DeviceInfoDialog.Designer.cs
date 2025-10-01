using lan_scanner.gui.ui_objects;

namespace lan_scanner.gui.windows
{
    partial class DeviceInfoDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceInfoDialog));
            tableLayoutPanelMain = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            labelName = new Label();
            labelNameText = new Label();
            labelMacVendor = new Label();
            labelMac = new Label();
            labelMacVendorText = new Label();
            labelMacText = new Label();
            labelIpText = new Label();
            labelIp = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            customButtonOk = new CustomButton();
            tableLayoutPanelMain.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanelMain.Controls.Add(tableLayoutPanel3, 0, 1);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 2;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tableLayoutPanelMain.Size = new Size(532, 278);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(labelName, 0, 0);
            tableLayoutPanel2.Controls.Add(labelNameText, 1, 0);
            tableLayoutPanel2.Controls.Add(labelMacVendor, 0, 3);
            tableLayoutPanel2.Controls.Add(labelMac, 0, 2);
            tableLayoutPanel2.Controls.Add(labelMacVendorText, 1, 3);
            tableLayoutPanel2.Controls.Add(labelMacText, 1, 2);
            tableLayoutPanel2.Controls.Add(labelIpText, 1, 1);
            tableLayoutPanel2.Controls.Add(labelIp, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 5;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(526, 197);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // labelName
            // 
            labelName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            labelName.AutoSize = true;
            labelName.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelName.Location = new Point(5, 13);
            labelName.Margin = new Padding(5, 0, 5, 0);
            labelName.Name = "labelName";
            labelName.Size = new Size(253, 23);
            labelName.TabIndex = 3;
            labelName.Text = "Имя устройства";
            // 
            // labelNameText
            // 
            labelNameText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            labelNameText.AutoSize = true;
            labelNameText.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelNameText.Location = new Point(268, 13);
            labelNameText.Margin = new Padding(5, 0, 5, 0);
            labelNameText.Name = "labelNameText";
            labelNameText.Size = new Size(253, 23);
            labelNameText.TabIndex = 7;
            labelNameText.Text = "Имя устройства";
            // 
            // labelMacVendor
            // 
            labelMacVendor.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            labelMacVendor.AutoSize = true;
            labelMacVendor.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelMacVendor.Location = new Point(5, 163);
            labelMacVendor.Margin = new Padding(5, 0, 5, 0);
            labelMacVendor.Name = "labelMacVendor";
            labelMacVendor.Size = new Size(253, 23);
            labelMacVendor.TabIndex = 4;
            labelMacVendor.Text = "Поставщик MAC-адреса";
            // 
            // labelMac
            // 
            labelMac.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            labelMac.AutoSize = true;
            labelMac.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelMac.Location = new Point(5, 113);
            labelMac.Margin = new Padding(5, 0, 5, 0);
            labelMac.Name = "labelMac";
            labelMac.Size = new Size(253, 23);
            labelMac.TabIndex = 2;
            labelMac.Text = "MAC-адрес";
            // 
            // labelMacVendorText
            // 
            labelMacVendorText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            labelMacVendorText.AutoSize = true;
            labelMacVendorText.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelMacVendorText.Location = new Point(268, 163);
            labelMacVendorText.Margin = new Padding(5, 0, 5, 0);
            labelMacVendorText.Name = "labelMacVendorText";
            labelMacVendorText.Size = new Size(253, 23);
            labelMacVendorText.TabIndex = 6;
            labelMacVendorText.Text = "Поставщик";
            // 
            // labelMacText
            // 
            labelMacText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            labelMacText.AutoSize = true;
            labelMacText.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelMacText.Location = new Point(268, 113);
            labelMacText.Margin = new Padding(5, 0, 5, 0);
            labelMacText.Name = "labelMacText";
            labelMacText.Size = new Size(253, 23);
            labelMacText.TabIndex = 5;
            labelMacText.Text = "FF:FF:FF:FF:FF:FF";
            // 
            // labelIpText
            // 
            labelIpText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            labelIpText.AutoSize = true;
            labelIpText.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelIpText.Location = new Point(268, 63);
            labelIpText.Margin = new Padding(5, 0, 5, 0);
            labelIpText.Name = "labelIpText";
            labelIpText.Size = new Size(253, 23);
            labelIpText.TabIndex = 1;
            labelIpText.Text = "255.255.255.255";
            // 
            // labelIp
            // 
            labelIp.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            labelIp.AutoSize = true;
            labelIp.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelIp.Location = new Point(5, 63);
            labelIp.Margin = new Padding(5, 0, 5, 0);
            labelIp.Name = "labelIp";
            labelIp.Size = new Size(253, 23);
            labelIp.TabIndex = 0;
            labelIp.Text = "IP-адрес";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(customButtonOk, 1, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 206);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(526, 69);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // customButtonOk
            // 
            customButtonOk.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            customButtonOk.AutoSize = true;
            customButtonOk.BackColor = Color.FromArgb(64, 136, 243);
            customButtonOk.CornerRadius = 100;
            customButtonOk.FlatAppearance.BorderSize = 0;
            customButtonOk.FlatStyle = FlatStyle.Flat;
            customButtonOk.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            customButtonOk.ForeColor = Color.White;
            customButtonOk.HoverBackColor = Color.FromArgb(56, 113, 197);
            customButtonOk.Location = new Point(138, 9);
            customButtonOk.Margin = new Padding(0);
            customButtonOk.Name = "customButtonOk";
            customButtonOk.Size = new Size(250, 50);
            customButtonOk.TabIndex = 1;
            customButtonOk.Text = "ОК";
            customButtonOk.UseVisualStyleBackColor = false;
            // 
            // DeviceInfoDialog
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(532, 278);
            Controls.Add(tableLayoutPanelMain);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(550, 325);
            MinimumSize = new Size(550, 325);
            Name = "DeviceInfoDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Информация об устройстве";
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelMain;
        private TableLayoutPanel tableLayoutPanel2;
        private lan_scanner.gui.ui_objects.CustomButton customButtonOk;
        private Label labelIp;
        private Label labelIpText;
        private Label labelNameText;
        private Label labelMacVendorText;
        private Label labelMacText;
        private Label labelMacVendor;
        private Label labelMac;
        private Label labelName;
        private TableLayoutPanel tableLayoutPanel3;
    }
}