using lan_scanner.gui.ui_objects;

namespace lan_scanner.gui.widgets
{
    partial class NetworkDeviceWidget
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
            labelDeviceName = new Label();
            tableLayoutPanelMain = new TableLayoutPanel();
            customButtonDevice = new CustomButton();
            tableLayoutPanelMain.SuspendLayout();
            SuspendLayout();
            // 
            // labelDeviceName
            // 
            labelDeviceName.AutoSize = true;
            labelDeviceName.BackColor = Color.Transparent;
            labelDeviceName.Dock = DockStyle.Fill;
            labelDeviceName.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelDeviceName.ForeColor = SystemColors.Control;
            labelDeviceName.Location = new Point(3, 126);
            labelDeviceName.Name = "labelDeviceName";
            labelDeviceName.Size = new Size(178, 43);
            labelDeviceName.TabIndex = 1;
            labelDeviceName.Text = "255.255.255.255";
            labelDeviceName.TextAlign = ContentAlignment.TopCenter;
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(labelDeviceName, 0, 1);
            tableLayoutPanelMain.Controls.Add(customButtonDevice, 0, 0);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 2;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 75F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanelMain.Size = new Size(184, 169);
            tableLayoutPanelMain.TabIndex = 2;
            // 
            // customButtonDevice
            // 
            customButtonDevice.BackColor = Color.FromArgb(192, 255, 255);
            customButtonDevice.BackgroundImage = Properties.Resources.DeviceButtonPcIcon;
            customButtonDevice.CornerRadius = 35;
            customButtonDevice.Dock = DockStyle.Fill;
            customButtonDevice.FlatAppearance.BorderSize = 0;
            customButtonDevice.FlatStyle = FlatStyle.Flat;
            customButtonDevice.Font = new Font("Segoe UI", 9F);
            customButtonDevice.ForeColor = Color.White;
            customButtonDevice.HoverBackColor = Color.FromArgb(132, 212, 212);
            customButtonDevice.Location = new Point(3, 3);
            customButtonDevice.Name = "customButtonDevice";
            customButtonDevice.Size = new Size(178, 120);
            customButtonDevice.TabIndex = 2;
            customButtonDevice.UseVisualStyleBackColor = false;
            // 
            // NetworkDeviceWidget
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(tableLayoutPanelMain);
            Font = new Font("Segoe UI Black", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 204);
            Name = "NetworkDeviceWidget";
            Size = new Size(184, 169);
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label labelDeviceName;
        private TableLayoutPanel tableLayoutPanelMain;
        private CustomButton customButtonDevice;
    }
}
