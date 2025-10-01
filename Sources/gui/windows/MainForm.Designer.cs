using lan_scanner.gui.ui_objects;
using lan_scanner.gui.widgets;

namespace lan_scanner.gui.windows
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanelMain = new TableLayoutPanel();
            graphicsPanel = new GraphicsPanel();
            tableLayoutPanelMainLeft = new TableLayoutPanel();
            tableLayoutPanelScanButton = new TableLayoutPanel();
            customButtonScan = new CustomButton();
            lanInterfacesWidget = new LanInterfacesWidget();
            tableLayoutPanelMain.SuspendLayout();
            tableLayoutPanelMainLeft.SuspendLayout();
            tableLayoutPanelScanButton.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 2;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(graphicsPanel, 1, 0);
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelMainLeft, 0, 0);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 1;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Size = new Size(982, 508);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // graphicsPanel
            // 
            graphicsPanel.AutoSize = true;
            graphicsPanel.BackColor = Color.FromArgb(34, 30, 50);
            graphicsPanel.Dock = DockStyle.Fill;
            graphicsPanel.Location = new Point(300, 0);
            graphicsPanel.Margin = new Padding(0);
            graphicsPanel.Name = "graphicsPanel";
            graphicsPanel.Size = new Size(682, 508);
            graphicsPanel.TabIndex = 0;
            // 
            // tableLayoutPanelMainLeft
            // 
            tableLayoutPanelMainLeft.ColumnCount = 1;
            tableLayoutPanelMainLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMainLeft.Controls.Add(tableLayoutPanelScanButton, 0, 1);
            tableLayoutPanelMainLeft.Controls.Add(lanInterfacesWidget, 0, 0);
            tableLayoutPanelMainLeft.Dock = DockStyle.Fill;
            tableLayoutPanelMainLeft.Location = new Point(3, 3);
            tableLayoutPanelMainLeft.Name = "tableLayoutPanelMainLeft";
            tableLayoutPanelMainLeft.RowCount = 2;
            tableLayoutPanelMainLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMainLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tableLayoutPanelMainLeft.Size = new Size(294, 502);
            tableLayoutPanelMainLeft.TabIndex = 1;
            // 
            // tableLayoutPanelScanButton
            // 
            tableLayoutPanelScanButton.ColumnCount = 3;
            tableLayoutPanelScanButton.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelScanButton.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 225F));
            tableLayoutPanelScanButton.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelScanButton.Controls.Add(customButtonScan, 1, 0);
            tableLayoutPanelScanButton.Dock = DockStyle.Fill;
            tableLayoutPanelScanButton.ForeColor = Color.Black;
            tableLayoutPanelScanButton.Location = new Point(0, 402);
            tableLayoutPanelScanButton.Margin = new Padding(0);
            tableLayoutPanelScanButton.Name = "tableLayoutPanelScanButton";
            tableLayoutPanelScanButton.RowCount = 1;
            tableLayoutPanelScanButton.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelScanButton.Size = new Size(294, 100);
            tableLayoutPanelScanButton.TabIndex = 0;
            // 
            // customButtonScan
            // 
            customButtonScan.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            customButtonScan.BackColor = Color.FromArgb(64, 136, 243);
            customButtonScan.CornerRadius = 100;
            customButtonScan.FlatAppearance.BorderSize = 0;
            customButtonScan.FlatStyle = FlatStyle.Flat;
            customButtonScan.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            customButtonScan.ForeColor = Color.White;
            customButtonScan.HoverBackColor = Color.FromArgb(56, 113, 197);
            customButtonScan.Image = Properties.Resources.SearchIconWhite25;
            customButtonScan.Location = new Point(37, 22);
            customButtonScan.Name = "customButtonScan";
            customButtonScan.Size = new Size(219, 55);
            customButtonScan.TabIndex = 0;
            customButtonScan.Text = "Сканировать";
            customButtonScan.TextImageRelation = TextImageRelation.ImageBeforeText;
            customButtonScan.TextImageSpacing = 10;
            customButtonScan.UseVisualStyleBackColor = false;
            // 
            // lanInterfacesWidget
            // 
            lanInterfacesWidget.Dock = DockStyle.Fill;
            lanInterfacesWidget.Location = new Point(0, 0);
            lanInterfacesWidget.Margin = new Padding(0);
            lanInterfacesWidget.MaximumSize = new Size(0, 400);
            lanInterfacesWidget.Name = "lanInterfacesWidget";
            lanInterfacesWidget.Size = new Size(294, 400);
            lanInterfacesWidget.TabIndex = 1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(982, 508);
            Controls.Add(tableLayoutPanelMain);
            MinimumSize = new Size(1000, 555);
            Name = "MainForm";
            Text = "LAN Scanner with GUI";
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelMain.PerformLayout();
            tableLayoutPanelMainLeft.ResumeLayout(false);
            tableLayoutPanelScanButton.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelMain;
        private GraphicsPanel graphicsPanel;
        private TableLayoutPanel tableLayoutPanelMainLeft;
        private TableLayoutPanel tableLayoutPanelScanButton;
        private CustomButton customButtonScan;
        private LanInterfacesWidget lanInterfacesWidget;
    }
}
