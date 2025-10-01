namespace lan_scanner.gui.widgets
{
    partial class LanInterfacesWidget
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
            comboBoxMain = new ComboBox();
            tableLayoutPanelInfo = new TableLayoutPanel();
            labelTypeText = new Label();
            labelDescriptionText = new Label();
            labelDescription = new Label();
            labelName = new Label();
            labelNameText = new Label();
            labelType = new Label();
            labelSubnetMask = new Label();
            labelMainInterfaceText = new Label();
            labelSubnetMaskText = new Label();
            labelDefaultGateway = new Label();
            labelDefaultGatewayText = new Label();
            labelMainInterface = new Label();
            tableLayoutPanelMain.SuspendLayout();
            tableLayoutPanelInfo.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(comboBoxMain, 0, 0);
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelInfo, 0, 1);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 2;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Size = new Size(419, 400);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // comboBoxMain
            // 
            comboBoxMain.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            comboBoxMain.BackColor = Color.White;
            comboBoxMain.FormattingEnabled = true;
            comboBoxMain.Location = new Point(30, 36);
            comboBoxMain.Margin = new Padding(30, 0, 30, 0);
            comboBoxMain.Name = "comboBoxMain";
            comboBoxMain.Size = new Size(359, 28);
            comboBoxMain.TabIndex = 0;
            // 
            // tableLayoutPanelInfo
            // 
            tableLayoutPanelInfo.AutoScroll = true;
            tableLayoutPanelInfo.ColumnCount = 2;
            tableLayoutPanelInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125F));
            tableLayoutPanelInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelInfo.Controls.Add(labelTypeText, 1, 2);
            tableLayoutPanelInfo.Controls.Add(labelDescriptionText, 1, 1);
            tableLayoutPanelInfo.Controls.Add(labelDescription, 0, 1);
            tableLayoutPanelInfo.Controls.Add(labelName, 0, 0);
            tableLayoutPanelInfo.Controls.Add(labelNameText, 1, 0);
            tableLayoutPanelInfo.Controls.Add(labelType, 0, 2);
            tableLayoutPanelInfo.Controls.Add(labelSubnetMask, 0, 4);
            tableLayoutPanelInfo.Controls.Add(labelMainInterfaceText, 1, 3);
            tableLayoutPanelInfo.Controls.Add(labelSubnetMaskText, 1, 4);
            tableLayoutPanelInfo.Controls.Add(labelDefaultGateway, 0, 5);
            tableLayoutPanelInfo.Controls.Add(labelDefaultGatewayText, 1, 5);
            tableLayoutPanelInfo.Controls.Add(labelMainInterface, 0, 3);
            tableLayoutPanelInfo.Dock = DockStyle.Fill;
            tableLayoutPanelInfo.Location = new Point(0, 100);
            tableLayoutPanelInfo.Margin = new Padding(0);
            tableLayoutPanelInfo.Name = "tableLayoutPanelInfo";
            tableLayoutPanelInfo.RowCount = 7;
            tableLayoutPanelInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanelInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanelInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanelInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanelInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanelInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanelInfo.RowStyles.Add(new RowStyle());
            tableLayoutPanelInfo.Size = new Size(419, 300);
            tableLayoutPanelInfo.TabIndex = 1;
            // 
            // labelTypeText
            // 
            labelTypeText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            labelTypeText.AutoEllipsis = true;
            labelTypeText.AutoSize = true;
            labelTypeText.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelTypeText.Location = new Point(130, 115);
            labelTypeText.Margin = new Padding(5, 0, 5, 0);
            labelTypeText.MaximumSize = new Size(0, 20);
            labelTypeText.Name = "labelTypeText";
            labelTypeText.Size = new Size(284, 20);
            labelTypeText.TabIndex = 5;
            labelTypeText.Text = "Тип интерфейса";
            labelTypeText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelDescriptionText
            // 
            labelDescriptionText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            labelDescriptionText.AutoEllipsis = true;
            labelDescriptionText.AutoSize = true;
            labelDescriptionText.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelDescriptionText.Location = new Point(130, 65);
            labelDescriptionText.Margin = new Padding(5, 0, 5, 0);
            labelDescriptionText.MaximumSize = new Size(0, 20);
            labelDescriptionText.Name = "labelDescriptionText";
            labelDescriptionText.Size = new Size(284, 20);
            labelDescriptionText.TabIndex = 4;
            labelDescriptionText.Text = "Описание интерфейса";
            labelDescriptionText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelDescription
            // 
            labelDescription.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            labelDescription.AutoEllipsis = true;
            labelDescription.AutoSize = true;
            labelDescription.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelDescription.Location = new Point(3, 65);
            labelDescription.MinimumSize = new Size(100, 0);
            labelDescription.Name = "labelDescription";
            labelDescription.Size = new Size(119, 20);
            labelDescription.TabIndex = 2;
            labelDescription.Text = "Описание";
            // 
            // labelName
            // 
            labelName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            labelName.AutoSize = true;
            labelName.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelName.Location = new Point(3, 15);
            labelName.MinimumSize = new Size(100, 0);
            labelName.Name = "labelName";
            labelName.Size = new Size(119, 20);
            labelName.TabIndex = 0;
            labelName.Text = "Имя";
            // 
            // labelNameText
            // 
            labelNameText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            labelNameText.AutoEllipsis = true;
            labelNameText.AutoSize = true;
            labelNameText.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelNameText.Location = new Point(130, 15);
            labelNameText.Margin = new Padding(5, 0, 5, 0);
            labelNameText.MaximumSize = new Size(0, 20);
            labelNameText.Name = "labelNameText";
            labelNameText.Size = new Size(284, 20);
            labelNameText.TabIndex = 1;
            labelNameText.Text = "Имя интерфейса";
            labelNameText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelType
            // 
            labelType.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            labelType.AutoSize = true;
            labelType.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelType.Location = new Point(3, 115);
            labelType.MinimumSize = new Size(100, 0);
            labelType.Name = "labelType";
            labelType.Size = new Size(119, 20);
            labelType.TabIndex = 3;
            labelType.Text = "Тип";
            // 
            // labelSubnetMask
            // 
            labelSubnetMask.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            labelSubnetMask.AutoEllipsis = true;
            labelSubnetMask.AutoSize = true;
            labelSubnetMask.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelSubnetMask.Location = new Point(3, 215);
            labelSubnetMask.MaximumSize = new Size(0, 20);
            labelSubnetMask.MinimumSize = new Size(100, 0);
            labelSubnetMask.Name = "labelSubnetMask";
            labelSubnetMask.Size = new Size(119, 20);
            labelSubnetMask.TabIndex = 7;
            labelSubnetMask.Text = "Маска подсети";
            // 
            // labelMainInterfaceText
            // 
            labelMainInterfaceText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            labelMainInterfaceText.AutoEllipsis = true;
            labelMainInterfaceText.AutoSize = true;
            labelMainInterfaceText.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelMainInterfaceText.Location = new Point(130, 165);
            labelMainInterfaceText.Margin = new Padding(5, 0, 5, 0);
            labelMainInterfaceText.MaximumSize = new Size(0, 20);
            labelMainInterfaceText.Name = "labelMainInterfaceText";
            labelMainInterfaceText.Size = new Size(284, 20);
            labelMainInterfaceText.TabIndex = 8;
            labelMainInterfaceText.Text = "255.255.255.255";
            labelMainInterfaceText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelSubnetMaskText
            // 
            labelSubnetMaskText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            labelSubnetMaskText.AutoEllipsis = true;
            labelSubnetMaskText.AutoSize = true;
            labelSubnetMaskText.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelSubnetMaskText.Location = new Point(130, 215);
            labelSubnetMaskText.Margin = new Padding(5, 0, 5, 0);
            labelSubnetMaskText.MaximumSize = new Size(0, 20);
            labelSubnetMaskText.Name = "labelSubnetMaskText";
            labelSubnetMaskText.Size = new Size(284, 20);
            labelSubnetMaskText.TabIndex = 9;
            labelSubnetMaskText.Text = "255.255.255.255";
            labelSubnetMaskText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelDefaultGateway
            // 
            labelDefaultGateway.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            labelDefaultGateway.AutoEllipsis = true;
            labelDefaultGateway.AutoSize = true;
            labelDefaultGateway.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelDefaultGateway.Location = new Point(3, 265);
            labelDefaultGateway.MaximumSize = new Size(0, 20);
            labelDefaultGateway.MinimumSize = new Size(100, 0);
            labelDefaultGateway.Name = "labelDefaultGateway";
            labelDefaultGateway.Size = new Size(119, 20);
            labelDefaultGateway.TabIndex = 10;
            labelDefaultGateway.Text = "Шлюз по умол.";
            // 
            // labelDefaultGatewayText
            // 
            labelDefaultGatewayText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            labelDefaultGatewayText.AutoEllipsis = true;
            labelDefaultGatewayText.AutoSize = true;
            labelDefaultGatewayText.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelDefaultGatewayText.Location = new Point(130, 265);
            labelDefaultGatewayText.Margin = new Padding(5, 0, 5, 0);
            labelDefaultGatewayText.MaximumSize = new Size(0, 20);
            labelDefaultGatewayText.Name = "labelDefaultGatewayText";
            labelDefaultGatewayText.Size = new Size(284, 20);
            labelDefaultGatewayText.TabIndex = 11;
            labelDefaultGatewayText.Text = "255.255.255.255";
            labelDefaultGatewayText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelMainInterface
            // 
            labelMainInterface.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            labelMainInterface.AutoEllipsis = true;
            labelMainInterface.AutoSize = true;
            labelMainInterface.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelMainInterface.Location = new Point(3, 165);
            labelMainInterface.MaximumSize = new Size(0, 20);
            labelMainInterface.MinimumSize = new Size(100, 0);
            labelMainInterface.Name = "labelMainInterface";
            labelMainInterface.Size = new Size(119, 20);
            labelMainInterface.TabIndex = 6;
            labelMainInterface.Text = "IPv4 (основной)";
            // 
            // LanInterfacesWidget
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanelMain);
            Margin = new Padding(0);
            MaximumSize = new Size(0, 400);
            Name = "LanInterfacesWidget";
            Size = new Size(419, 400);
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelInfo.ResumeLayout(false);
            tableLayoutPanelInfo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelMain;
        private ComboBox comboBoxMain;
        private TableLayoutPanel tableLayoutPanelInfo;
        private Label labelTypeText;
        private Label labelDescriptionText;
        private Label labelDescription;
        private Label labelName;
        private Label labelNameText;
        private Label labelType;
        private Label labelMainInterface;
        private Label labelSubnetMask;
        private Label labelMainInterfaceText;
        private Label labelSubnetMaskText;
        private Label labelDefaultGateway;
        private Label labelDefaultGatewayText;
    }
}
