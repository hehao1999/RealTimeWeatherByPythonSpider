namespace mainView.MapShows
{
    partial class FrmThemeGraduate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmThemeGraduate));
            this.fraColorRamp = new System.Windows.Forms.GroupBox();
            this.imgcboColorRamp = new System.Windows.Forms.ImageCombo();
            this.iltColorRamp = new System.Windows.Forms.ImageList(this.components);
            this.txtMinSize = new System.Windows.Forms.TextBox();
            this.Button2 = new System.Windows.Forms.Button();
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.Col3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnApply = new System.Windows.Forms.Button();
            this.fraBackground = new System.Windows.Forms.GroupBox();
            this.picBackground = new System.Windows.Forms.PictureBox();
            this.txtMaxSize = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.fraSymbol = new System.Windows.Forms.GroupBox();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Button1 = new System.Windows.Forms.Button();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbFields = new System.Windows.Forms.ComboBox();
            this.RadioButtonSize = new System.Windows.Forms.RadioButton();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.RadioButtonColor = new System.Windows.Forms.RadioButton();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.ListBoxLayers = new System.Windows.Forms.ListBox();
            this.GroupBox4 = new System.Windows.Forms.GroupBox();
            this.txtClassCount = new System.Windows.Forms.NumericUpDown();
            this.ComboBoxCls = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.picSample = new System.Windows.Forms.PictureBox();
            this.ColorDialog1 = new System.Windows.Forms.ColorDialog();
            this.fraColorRamp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            this.fraBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBackground)).BeginInit();
            this.fraSymbol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.GroupBox3.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.GroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtClassCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSample)).BeginInit();
            this.SuspendLayout();
            // 
            // fraColorRamp
            // 
            this.fraColorRamp.Controls.Add(this.imgcboColorRamp);
            this.fraColorRamp.Location = new System.Drawing.Point(195, 154);
            this.fraColorRamp.Margin = new System.Windows.Forms.Padding(4);
            this.fraColorRamp.Name = "fraColorRamp";
            this.fraColorRamp.Padding = new System.Windows.Forms.Padding(4);
            this.fraColorRamp.Size = new System.Drawing.Size(503, 64);
            this.fraColorRamp.TabIndex = 84;
            this.fraColorRamp.TabStop = false;
            this.fraColorRamp.Text = "颜色";
            // 
            // imgcboColorRamp
            // 
            this.imgcboColorRamp.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.imgcboColorRamp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.imgcboColorRamp.ImageList = this.iltColorRamp;
            this.imgcboColorRamp.ImeMode = System.Windows.Forms.ImeMode.On;
            this.imgcboColorRamp.Location = new System.Drawing.Point(8, 26);
            this.imgcboColorRamp.Margin = new System.Windows.Forms.Padding(4);
            this.imgcboColorRamp.Name = "imgcboColorRamp";
            this.imgcboColorRamp.Size = new System.Drawing.Size(306, 26);
            this.imgcboColorRamp.TabIndex = 1;
            this.imgcboColorRamp.Tag = "888";
            this.imgcboColorRamp.SelectedIndexChanged += new System.EventHandler(this.imgcboColorRamp_SelectedIndexChanged);
            // 
            // iltColorRamp
            // 
            this.iltColorRamp.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iltColorRamp.ImageStream")));
            this.iltColorRamp.TransparentColor = System.Drawing.Color.Transparent;
            this.iltColorRamp.Images.SetKeyName(0, "1.5.bmp");
            this.iltColorRamp.Images.SetKeyName(1, "YellowToRed.bmp");
            this.iltColorRamp.Images.SetKeyName(2, "GreenToBlue.bmp");
            this.iltColorRamp.Images.SetKeyName(3, "GreenToRed.bmp");
            // 
            // txtMinSize
            // 
            this.txtMinSize.Location = new System.Drawing.Point(176, 26);
            this.txtMinSize.Margin = new System.Windows.Forms.Padding(4);
            this.txtMinSize.Name = "txtMinSize";
            this.txtMinSize.Size = new System.Drawing.Size(63, 25);
            this.txtMinSize.TabIndex = 3;
            this.txtMinSize.TextChanged += new System.EventHandler(this.txtMinSize_TextChanged);
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(12, 22);
            this.Button2.Margin = new System.Windows.Forms.Padding(4);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(59, 32);
            this.Button2.TabIndex = 3;
            this.Button2.Text = "Button2";
            this.Button2.UseVisualStyleBackColor = true;
            // 
            // DataGridView
            // 
            this.DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col3,
            this.Col2});
            this.DataGridView.Location = new System.Drawing.Point(195, 224);
            this.DataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DataGridView.MultiSelect = false;
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.RowHeadersVisible = false;
            this.DataGridView.RowHeadersWidth = 51;
            this.DataGridView.RowTemplate.Height = 27;
            this.DataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DataGridView.Size = new System.Drawing.Size(503, 246);
            this.DataGridView.TabIndex = 83;
            // 
            // Col3
            // 
            this.Col3.FillWeight = 121.8274F;
            this.Col3.HeaderText = "序号";
            this.Col3.MinimumWidth = 60;
            this.Col3.Name = "Col3";
            this.Col3.ReadOnly = true;
            // 
            // Col2
            // 
            this.Col2.FillWeight = 128.4423F;
            this.Col2.HeaderText = "值";
            this.Col2.MinimumWidth = 150;
            this.Col2.Name = "Col2";
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(313, 496);
            this.btnApply.Margin = new System.Windows.Forms.Padding(4);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(100, 32);
            this.btnApply.TabIndex = 82;
            this.btnApply.Text = "应用";
            this.btnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // fraBackground
            // 
            this.fraBackground.Controls.Add(this.picBackground);
            this.fraBackground.Controls.Add(this.Button2);
            this.fraBackground.Location = new System.Drawing.Point(620, 135);
            this.fraBackground.Margin = new System.Windows.Forms.Padding(4);
            this.fraBackground.Name = "fraBackground";
            this.fraBackground.Padding = new System.Windows.Forms.Padding(4);
            this.fraBackground.Size = new System.Drawing.Size(79, 68);
            this.fraBackground.TabIndex = 79;
            this.fraBackground.TabStop = false;
            this.fraBackground.Text = "背景";
            // 
            // picBackground
            // 
            this.picBackground.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.picBackground.Location = new System.Drawing.Point(12, 21);
            this.picBackground.Margin = new System.Windows.Forms.Padding(4);
            this.picBackground.Name = "picBackground";
            this.picBackground.Size = new System.Drawing.Size(56, 31);
            this.picBackground.TabIndex = 4;
            this.picBackground.TabStop = false;
            this.picBackground.BackColorChanged += new System.EventHandler(this.picBackground_BackColorChanged);
            this.picBackground.Click += new System.EventHandler(this.picBackground_Click);
            // 
            // txtMaxSize
            // 
            this.txtMaxSize.Location = new System.Drawing.Point(343, 25);
            this.txtMaxSize.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaxSize.Name = "txtMaxSize";
            this.txtMaxSize.Size = new System.Drawing.Size(63, 25);
            this.txtMaxSize.TabIndex = 4;
            this.txtMaxSize.TextChanged += new System.EventHandler(this.txtMaxSize_TextChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(587, 496);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 32);
            this.buttonCancel.TabIndex = 81;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(461, 496);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(100, 32);
            this.buttonOk.TabIndex = 80;
            this.buttonOk.Text = "确定";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // fraSymbol
            // 
            this.fraSymbol.Controls.Add(this.txtMinSize);
            this.fraSymbol.Controls.Add(this.txtMaxSize);
            this.fraSymbol.Controls.Add(this.PictureBox1);
            this.fraSymbol.Controls.Add(this.Label4);
            this.fraSymbol.Controls.Add(this.Label3);
            this.fraSymbol.Controls.Add(this.Button1);
            this.fraSymbol.Location = new System.Drawing.Point(196, 128);
            this.fraSymbol.Margin = new System.Windows.Forms.Padding(4);
            this.fraSymbol.Name = "fraSymbol";
            this.fraSymbol.Padding = new System.Windows.Forms.Padding(4);
            this.fraSymbol.Size = new System.Drawing.Size(416, 75);
            this.fraSymbol.TabIndex = 78;
            this.fraSymbol.TabStop = false;
            this.fraSymbol.Text = "符号";
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.PictureBox1.Location = new System.Drawing.Point(21, 24);
            this.PictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(76, 31);
            this.PictureBox1.TabIndex = 1;
            this.PictureBox1.TabStop = false;
            this.PictureBox1.BackColorChanged += new System.EventHandler(this.PictureBox1_BackColorChanged);
            this.PictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(264, 29);
            this.Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(67, 15);
            this.Label4.TabIndex = 2;
            this.Label4.Text = "最大尺寸";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(105, 32);
            this.Label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(67, 15);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "最小尺寸";
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(17, 24);
            this.Button1.Margin = new System.Windows.Forms.Padding(4);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(80, 32);
            this.Button1.TabIndex = 0;
            this.Button1.Text = "Button1";
            this.Button1.UseVisualStyleBackColor = true;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.cmbFields);
            this.GroupBox3.Location = new System.Drawing.Point(195, 76);
            this.GroupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.GroupBox3.Size = new System.Drawing.Size(153, 51);
            this.GroupBox3.TabIndex = 76;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "字段";
            // 
            // cmbFields
            // 
            this.cmbFields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFields.FormattingEnabled = true;
            this.cmbFields.Location = new System.Drawing.Point(20, 18);
            this.cmbFields.Margin = new System.Windows.Forms.Padding(4);
            this.cmbFields.Name = "cmbFields";
            this.cmbFields.Size = new System.Drawing.Size(121, 23);
            this.cmbFields.TabIndex = 0;
            this.cmbFields.SelectedIndexChanged += new System.EventHandler(this.cmbFields_SelectedIndexChanged);
            // 
            // RadioButtonSize
            // 
            this.RadioButtonSize.AutoSize = true;
            this.RadioButtonSize.Location = new System.Drawing.Point(320, 26);
            this.RadioButtonSize.Margin = new System.Windows.Forms.Padding(4);
            this.RadioButtonSize.Name = "RadioButtonSize";
            this.RadioButtonSize.Size = new System.Drawing.Size(103, 19);
            this.RadioButtonSize.TabIndex = 0;
            this.RadioButtonSize.TabStop = true;
            this.RadioButtonSize.Text = "按尺寸分类";
            this.RadioButtonSize.UseVisualStyleBackColor = true;
            this.RadioButtonSize.Click += new System.EventHandler(this.RadioButtonSize_Click);
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.RadioButtonSize);
            this.GroupBox2.Controls.Add(this.RadioButtonColor);
            this.GroupBox2.Location = new System.Drawing.Point(189, 9);
            this.GroupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.GroupBox2.Size = new System.Drawing.Size(509, 60);
            this.GroupBox2.TabIndex = 75;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "类型";
            // 
            // RadioButtonColor
            // 
            this.RadioButtonColor.AutoSize = true;
            this.RadioButtonColor.Location = new System.Drawing.Point(76, 26);
            this.RadioButtonColor.Margin = new System.Windows.Forms.Padding(4);
            this.RadioButtonColor.Name = "RadioButtonColor";
            this.RadioButtonColor.Size = new System.Drawing.Size(103, 19);
            this.RadioButtonColor.TabIndex = 0;
            this.RadioButtonColor.TabStop = true;
            this.RadioButtonColor.Text = "按颜色分类";
            this.RadioButtonColor.UseVisualStyleBackColor = true;
            this.RadioButtonColor.Click += new System.EventHandler(this.RadioButtonColor_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.ListBoxLayers);
            this.GroupBox1.Location = new System.Drawing.Point(8, 9);
            this.GroupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.GroupBox1.Size = new System.Drawing.Size(172, 368);
            this.GroupBox1.TabIndex = 73;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Tag = "";
            this.GroupBox1.Text = "图层";
            // 
            // ListBoxLayers
            // 
            this.ListBoxLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListBoxLayers.FormattingEnabled = true;
            this.ListBoxLayers.ItemHeight = 15;
            this.ListBoxLayers.Location = new System.Drawing.Point(4, 22);
            this.ListBoxLayers.Margin = new System.Windows.Forms.Padding(4);
            this.ListBoxLayers.Name = "ListBoxLayers";
            this.ListBoxLayers.Size = new System.Drawing.Size(164, 342);
            this.ListBoxLayers.TabIndex = 0;
            this.ListBoxLayers.SelectedIndexChanged += new System.EventHandler(this.ListBoxLayers_SelectedIndexChanged);
            this.ListBoxLayers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListBoxLayers_MouseDoubleClick);
            // 
            // GroupBox4
            // 
            this.GroupBox4.Controls.Add(this.txtClassCount);
            this.GroupBox4.Controls.Add(this.ComboBoxCls);
            this.GroupBox4.Controls.Add(this.Label2);
            this.GroupBox4.Controls.Add(this.Label1);
            this.GroupBox4.Location = new System.Drawing.Point(356, 76);
            this.GroupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.GroupBox4.Size = new System.Drawing.Size(343, 51);
            this.GroupBox4.TabIndex = 77;
            this.GroupBox4.TabStop = false;
            this.GroupBox4.Text = "分类";
            // 
            // txtClassCount
            // 
            this.txtClassCount.Location = new System.Drawing.Point(271, 19);
            this.txtClassCount.Margin = new System.Windows.Forms.Padding(4);
            this.txtClassCount.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.txtClassCount.Name = "txtClassCount";
            this.txtClassCount.Size = new System.Drawing.Size(64, 25);
            this.txtClassCount.TabIndex = 2;
            this.txtClassCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.txtClassCount.ValueChanged += new System.EventHandler(this.txtClassCount_ValueChanged);
            this.txtClassCount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtClassCount_KeyUp);
            this.txtClassCount.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtClassCount_MouseUp);
            // 
            // ComboBoxCls
            // 
            this.ComboBoxCls.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxCls.FormattingEnabled = true;
            this.ComboBoxCls.Location = new System.Drawing.Point(88, 19);
            this.ComboBoxCls.Margin = new System.Windows.Forms.Padding(4);
            this.ComboBoxCls.Name = "ComboBoxCls";
            this.ComboBoxCls.Size = new System.Drawing.Size(117, 23);
            this.ComboBoxCls.TabIndex = 1;
            this.ComboBoxCls.SelectedIndexChanged += new System.EventHandler(this.ComboBoxCls_SelectedIndexChanged);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(216, 25);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(52, 15);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "等级数";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(15, 25);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(67, 15);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "分类方式";
            // 
            // picSample
            // 
            this.picSample.Image = ((System.Drawing.Image)(resources.GetObject("picSample.Image")));
            this.picSample.Location = new System.Drawing.Point(13, 385);
            this.picSample.Margin = new System.Windows.Forms.Padding(4);
            this.picSample.Name = "picSample";
            this.picSample.Size = new System.Drawing.Size(164, 134);
            this.picSample.TabIndex = 74;
            this.picSample.TabStop = false;
            // 
            // FrmThemeGraduate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 545);
            this.Controls.Add(this.fraColorRamp);
            this.Controls.Add(this.DataGridView);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.picSample);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.GroupBox4);
            this.Controls.Add(this.fraSymbol);
            this.Controls.Add(this.fraBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmThemeGraduate";
            this.Text = "等级分类图";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmThemeGraduate_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmThemeGraduate_FormClosed);
            this.Load += new System.EventHandler(this.FrmThemeGraduate_Load);
            this.fraColorRamp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            this.fraBackground.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBackground)).EndInit();
            this.fraSymbol.ResumeLayout(false);
            this.fraSymbol.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox4.ResumeLayout(false);
            this.GroupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtClassCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSample)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox fraColorRamp;
        internal System.Windows.Forms.ImageCombo imgcboColorRamp;
        internal System.Windows.Forms.ImageList iltColorRamp;
        internal System.Windows.Forms.TextBox txtMinSize;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.DataGridView DataGridView;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Col3;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Col2;
        private System.Windows.Forms.Button btnApply;
        internal System.Windows.Forms.PictureBox picBackground;
        internal System.Windows.Forms.GroupBox fraBackground;
        internal System.Windows.Forms.TextBox txtMaxSize;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        internal System.Windows.Forms.PictureBox picSample;
        internal System.Windows.Forms.GroupBox fraSymbol;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.PictureBox PictureBox1;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.ComboBox cmbFields;
        internal System.Windows.Forms.RadioButton RadioButtonSize;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.RadioButton RadioButtonColor;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.ListBox ListBoxLayers;
        internal System.Windows.Forms.GroupBox GroupBox4;
        internal System.Windows.Forms.NumericUpDown txtClassCount;
        internal System.Windows.Forms.ComboBox ComboBoxCls;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.ColorDialog ColorDialog1;
    }
}