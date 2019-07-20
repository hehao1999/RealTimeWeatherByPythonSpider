namespace mainView.MapShows
{
    partial class FrmThemeUniqueValue
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmThemeUniqueValue));
            this.ListBoxLayers = new System.Windows.Forms.ListBox();
            this.fraColorRamp = new System.Windows.Forms.GroupBox();
            this.imgcboColorRamp = new System.Windows.Forms.ImageCombo();
            this.btnApply = new System.Windows.Forms.Button();
            this.iltColorRamp = new System.Windows.Forms.ImageList(this.components);
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbFields = new System.Windows.Forms.ComboBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.picSample = new System.Windows.Forms.PictureBox();
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.Col4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fraColorRamp.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ListBoxLayers
            // 
            this.ListBoxLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListBoxLayers.FormattingEnabled = true;
            this.ListBoxLayers.ItemHeight = 15;
            this.ListBoxLayers.Location = new System.Drawing.Point(4, 22);
            this.ListBoxLayers.Margin = new System.Windows.Forms.Padding(4);
            this.ListBoxLayers.Name = "ListBoxLayers";
            this.ListBoxLayers.Size = new System.Drawing.Size(164, 220);
            this.ListBoxLayers.TabIndex = 0;
            this.ListBoxLayers.SelectedIndexChanged += new System.EventHandler(this.ListBoxLayers_SelectedIndexChanged);
            // 
            // fraColorRamp
            // 
            this.fraColorRamp.Controls.Add(this.imgcboColorRamp);
            this.fraColorRamp.Location = new System.Drawing.Point(191, 10);
            this.fraColorRamp.Margin = new System.Windows.Forms.Padding(4);
            this.fraColorRamp.Name = "fraColorRamp";
            this.fraColorRamp.Padding = new System.Windows.Forms.Padding(4);
            this.fraColorRamp.Size = new System.Drawing.Size(344, 62);
            this.fraColorRamp.TabIndex = 86;
            this.fraColorRamp.TabStop = false;
            this.fraColorRamp.Text = "颜色";
            // 
            // imgcboColorRamp
            // 
            this.imgcboColorRamp.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.imgcboColorRamp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.imgcboColorRamp.ImeMode = System.Windows.Forms.ImeMode.On;
            this.imgcboColorRamp.Location = new System.Drawing.Point(17, 21);
            this.imgcboColorRamp.Margin = new System.Windows.Forms.Padding(4);
            this.imgcboColorRamp.Name = "imgcboColorRamp";
            this.imgcboColorRamp.Size = new System.Drawing.Size(317, 26);
            this.imgcboColorRamp.TabIndex = 0;
            this.imgcboColorRamp.Tag = "888";
            this.imgcboColorRamp.SelectedIndexChanged += new System.EventHandler(this.imgcboColorRamp_SelectedIndexChanged);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(239, 386);
            this.btnApply.Margin = new System.Windows.Forms.Padding(4);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(100, 32);
            this.btnApply.TabIndex = 85;
            this.btnApply.Text = "应用";
            this.btnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // iltColorRamp
            // 
            this.iltColorRamp.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iltColorRamp.ImageStream")));
            this.iltColorRamp.TransparentColor = System.Drawing.Color.Transparent;
            this.iltColorRamp.Images.SetKeyName(0, "1.2.bmp");
            this.iltColorRamp.Images.SetKeyName(1, "1.3.bmp");
            this.iltColorRamp.Images.SetKeyName(2, "1.5.bmp");
            this.iltColorRamp.Images.SetKeyName(3, "YellowToRed.bmp");
            this.iltColorRamp.Images.SetKeyName(4, "GreenToBlue.bmp");
            this.iltColorRamp.Images.SetKeyName(5, "GreenToRed.bmp");
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(569, 386);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 32);
            this.buttonCancel.TabIndex = 84;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Enabled = false;
            this.buttonOk.Location = new System.Drawing.Point(404, 386);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(100, 32);
            this.buttonOk.TabIndex = 83;
            this.buttonOk.Text = "确定";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.cmbFields);
            this.GroupBox3.Location = new System.Drawing.Point(543, 10);
            this.GroupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.GroupBox3.Size = new System.Drawing.Size(153, 62);
            this.GroupBox3.TabIndex = 82;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "字段";
            // 
            // cmbFields
            // 
            this.cmbFields.FormattingEnabled = true;
            this.cmbFields.Location = new System.Drawing.Point(13, 19);
            this.cmbFields.Margin = new System.Windows.Forms.Padding(4);
            this.cmbFields.Name = "cmbFields";
            this.cmbFields.Size = new System.Drawing.Size(131, 23);
            this.cmbFields.TabIndex = 0;
            this.cmbFields.SelectedIndexChanged += new System.EventHandler(this.cmbFields_SelectedIndexChanged);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.ListBoxLayers);
            this.GroupBox1.Location = new System.Drawing.Point(11, 10);
            this.GroupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.GroupBox1.Size = new System.Drawing.Size(172, 246);
            this.GroupBox1.TabIndex = 80;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Tag = "";
            this.GroupBox1.Text = "图层";
            // 
            // picSample
            // 
            this.picSample.Image = ((System.Drawing.Image)(resources.GetObject("picSample.Image")));
            this.picSample.Location = new System.Drawing.Point(11, 266);
            this.picSample.Margin = new System.Windows.Forms.Padding(4);
            this.picSample.Name = "picSample";
            this.picSample.Size = new System.Drawing.Size(171, 100);
            this.picSample.TabIndex = 81;
            this.picSample.TabStop = false;
            // 
            // DataGridView
            // 
            this.DataGridView.AllowUserToAddRows = false;
            this.DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col4,
            this.Col5,
            this.Col6});
            this.DataGridView.Location = new System.Drawing.Point(193, 84);
            this.DataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DataGridView.MultiSelect = false;
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.ReadOnly = true;
            this.DataGridView.RowHeadersVisible = false;
            this.DataGridView.RowHeadersWidth = 51;
            this.DataGridView.RowTemplate.Height = 27;
            this.DataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DataGridView.Size = new System.Drawing.Size(507, 285);
            this.DataGridView.TabIndex = 87;
            this.DataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellContentClick);
            // 
            // Col4
            // 
            this.Col4.HeaderText = "序号";
            this.Col4.MinimumWidth = 6;
            this.Col4.Name = "Col4";
            this.Col4.ReadOnly = true;
            // 
            // Col5
            // 
            this.Col5.HeaderText = "值";
            this.Col5.MinimumWidth = 6;
            this.Col5.Name = "Col5";
            this.Col5.ReadOnly = true;
            // 
            // Col6
            // 
            this.Col6.HeaderText = "符号颜色";
            this.Col6.MinimumWidth = 6;
            this.Col6.Name = "Col6";
            this.Col6.ReadOnly = true;
            // 
            // FrmThemeUniqueValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 429);
            this.Controls.Add(this.DataGridView);
            this.Controls.Add(this.fraColorRamp);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.picSample);
            this.Controls.Add(this.GroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmThemeUniqueValue";
            this.Text = "唯一值显示";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmThemeUniqueValue_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmThemeUniqueValue_FormClosed);
            this.Load += new System.EventHandler(this.FrmThemeUniqueValue_Load);
            this.fraColorRamp.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        internal System.Windows.Forms.ListBox ListBoxLayers;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Col3;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Col2;
        internal System.Windows.Forms.GroupBox fraColorRamp;
        internal System.Windows.Forms.ImageCombo imgcboColorRamp;
        private System.Windows.Forms.Button btnApply;
        internal System.Windows.Forms.ImageList iltColorRamp;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.ComboBox cmbFields;
        internal System.Windows.Forms.PictureBox picSample;
        internal System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        internal System.Windows.Forms.DataGridView DataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col6;
    }
}