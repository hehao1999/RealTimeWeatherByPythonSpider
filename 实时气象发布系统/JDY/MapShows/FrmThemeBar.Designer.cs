namespace mainView.MapShows
{
    partial class FrmThemeBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmThemeBar));
            this.Label5 = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.ListBoxLayers = new System.Windows.Forms.ListBox();
            this.iltColorRamp = new System.Windows.Forms.ImageList(this.components);
            this.PanelBackGroudColor = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.iltSecond = new System.Windows.Forms.ImageList(this.components);
            this.iltFirst = new System.Windows.Forms.ImageList(this.components);
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnAllRemove = new System.Windows.Forms.Button();
            this.btnAllAdd = new System.Windows.Forms.Button();
            this.Label4 = new System.Windows.Forms.Label();
            this.GroupBox6 = new System.Windows.Forms.GroupBox();
            this.btnSingleRemove = new System.Windows.Forms.Button();
            this.btnSingleAdd = new System.Windows.Forms.Button();
            this.listBoxField = new System.Windows.Forms.ListBox();
            this.numUpDownHeight = new System.Windows.Forms.NumericUpDown();
            this.numUpDownWidth = new System.Windows.Forms.NumericUpDown();
            this.GroupBox5 = new System.Windows.Forms.GroupBox();
            this.ColorDialog1 = new System.Windows.Forms.ColorDialog();
            this.picSample = new System.Windows.Forms.PictureBox();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.GroupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownWidth)).BeginInit();
            this.GroupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSample)).BeginInit();
            this.SuspendLayout();
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(221, 30);
            this.Label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(45, 15);
            this.Label5.TabIndex = 3;
            this.Label5.Text = "宽度:";
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(276, 376);
            this.btnApply.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(100, 39);
            this.btnApply.TabIndex = 103;
            this.btnApply.Text = "应用";
            this.btnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // ListBoxLayers
            // 
            this.ListBoxLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListBoxLayers.FormattingEnabled = true;
            this.ListBoxLayers.ItemHeight = 15;
            this.ListBoxLayers.Location = new System.Drawing.Point(4, 22);
            this.ListBoxLayers.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ListBoxLayers.Name = "ListBoxLayers";
            this.ListBoxLayers.Size = new System.Drawing.Size(161, 236);
            this.ListBoxLayers.TabIndex = 0;
            this.ListBoxLayers.SelectedIndexChanged += new System.EventHandler(this.ListBoxLayers_SelectedIndexChanged);
            // 
            // iltColorRamp
            // 
            this.iltColorRamp.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iltColorRamp.ImageStream")));
            this.iltColorRamp.TransparentColor = System.Drawing.Color.Transparent;
            this.iltColorRamp.Images.SetKeyName(0, "YellowToRed.bmp");
            this.iltColorRamp.Images.SetKeyName(1, "GreenToBlue.bmp");
            this.iltColorRamp.Images.SetKeyName(2, "GreenToRed.bmp");
            // 
            // PanelBackGroudColor
            // 
            this.PanelBackGroudColor.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.PanelBackGroudColor.Location = new System.Drawing.Point(517, 24);
            this.PanelBackGroudColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PanelBackGroudColor.Name = "PanelBackGroudColor";
            this.PanelBackGroudColor.Size = new System.Drawing.Size(96, 26);
            this.PanelBackGroudColor.TabIndex = 7;
            this.PanelBackGroudColor.Click += new System.EventHandler(this.PanelBackGroudColor_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(492, 376);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 39);
            this.btnCancel.TabIndex = 102;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(384, 376);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 39);
            this.btnOk.TabIndex = 101;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // iltSecond
            // 
            this.iltSecond.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.iltSecond.ImageSize = new System.Drawing.Size(16, 16);
            this.iltSecond.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // iltFirst
            // 
            this.iltFirst.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.iltFirst.ImageSize = new System.Drawing.Size(16, 16);
            this.iltFirst.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.ListBoxLayers);
            this.GroupBox1.Location = new System.Drawing.Point(0, 2);
            this.GroupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GroupBox1.Size = new System.Drawing.Size(169, 262);
            this.GroupBox1.TabIndex = 99;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Tag = "";
            this.GroupBox1.Text = "图层";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(24, 32);
            this.Label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(45, 15);
            this.Label6.TabIndex = 1;
            this.Label6.Text = "高度:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(241, 21);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(220, 230);
            this.dataGridView1.TabIndex = 6;
            // 
            // btnAllRemove
            // 
            this.btnAllRemove.Location = new System.Drawing.Point(183, 204);
            this.btnAllRemove.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAllRemove.Name = "btnAllRemove";
            this.btnAllRemove.Size = new System.Drawing.Size(44, 29);
            this.btnAllRemove.TabIndex = 4;
            this.btnAllRemove.Text = "<<";
            this.btnAllRemove.UseVisualStyleBackColor = true;
            this.btnAllRemove.Click += new System.EventHandler(this.btnAllRemove_Click);
            // 
            // btnAllAdd
            // 
            this.btnAllAdd.Location = new System.Drawing.Point(183, 151);
            this.btnAllAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAllAdd.Name = "btnAllAdd";
            this.btnAllAdd.Size = new System.Drawing.Size(44, 29);
            this.btnAllAdd.TabIndex = 5;
            this.btnAllAdd.Text = ">>";
            this.btnAllAdd.UseVisualStyleBackColor = true;
            this.btnAllAdd.Click += new System.EventHandler(this.btnAllAdd_Click);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(412, 32);
            this.Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(75, 15);
            this.Label4.TabIndex = 6;
            this.Label4.Text = "背景颜色:";
            // 
            // GroupBox6
            // 
            this.GroupBox6.Controls.Add(this.dataGridView1);
            this.GroupBox6.Controls.Add(this.btnAllAdd);
            this.GroupBox6.Controls.Add(this.btnAllRemove);
            this.GroupBox6.Controls.Add(this.btnSingleRemove);
            this.GroupBox6.Controls.Add(this.btnSingleAdd);
            this.GroupBox6.Controls.Add(this.listBoxField);
            this.GroupBox6.Location = new System.Drawing.Point(177, 2);
            this.GroupBox6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GroupBox6.Name = "GroupBox6";
            this.GroupBox6.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GroupBox6.Size = new System.Drawing.Size(469, 262);
            this.GroupBox6.TabIndex = 105;
            this.GroupBox6.TabStop = false;
            this.GroupBox6.Text = "字段选择";
            // 
            // btnSingleRemove
            // 
            this.btnSingleRemove.Location = new System.Drawing.Point(183, 99);
            this.btnSingleRemove.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSingleRemove.Name = "btnSingleRemove";
            this.btnSingleRemove.Size = new System.Drawing.Size(44, 29);
            this.btnSingleRemove.TabIndex = 3;
            this.btnSingleRemove.Text = "<";
            this.btnSingleRemove.UseVisualStyleBackColor = true;
            this.btnSingleRemove.Click += new System.EventHandler(this.btnSingleRemove_Click);
            // 
            // btnSingleAdd
            // 
            this.btnSingleAdd.Location = new System.Drawing.Point(181, 46);
            this.btnSingleAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSingleAdd.Name = "btnSingleAdd";
            this.btnSingleAdd.Size = new System.Drawing.Size(45, 29);
            this.btnSingleAdd.TabIndex = 2;
            this.btnSingleAdd.Text = ">";
            this.btnSingleAdd.UseVisualStyleBackColor = true;
            this.btnSingleAdd.Click += new System.EventHandler(this.btnSingleAdd_Click);
            // 
            // listBoxField
            // 
            this.listBoxField.FormattingEnabled = true;
            this.listBoxField.ItemHeight = 15;
            this.listBoxField.Location = new System.Drawing.Point(8, 21);
            this.listBoxField.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listBoxField.Name = "listBoxField";
            this.listBoxField.Size = new System.Drawing.Size(164, 229);
            this.listBoxField.TabIndex = 1;
            // 
            // numUpDownHeight
            // 
            this.numUpDownHeight.Location = new System.Drawing.Point(99, 24);
            this.numUpDownHeight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numUpDownHeight.Name = "numUpDownHeight";
            this.numUpDownHeight.Size = new System.Drawing.Size(91, 25);
            this.numUpDownHeight.TabIndex = 0;
            this.numUpDownHeight.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // numUpDownWidth
            // 
            this.numUpDownWidth.Location = new System.Drawing.Point(291, 24);
            this.numUpDownWidth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numUpDownWidth.Name = "numUpDownWidth";
            this.numUpDownWidth.Size = new System.Drawing.Size(91, 25);
            this.numUpDownWidth.TabIndex = 2;
            this.numUpDownWidth.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // GroupBox5
            // 
            this.GroupBox5.Controls.Add(this.Label5);
            this.GroupBox5.Controls.Add(this.PanelBackGroudColor);
            this.GroupBox5.Controls.Add(this.Label6);
            this.GroupBox5.Controls.Add(this.Label4);
            this.GroupBox5.Controls.Add(this.numUpDownHeight);
            this.GroupBox5.Controls.Add(this.numUpDownWidth);
            this.GroupBox5.Location = new System.Drawing.Point(4, 275);
            this.GroupBox5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GroupBox5.Name = "GroupBox5";
            this.GroupBox5.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GroupBox5.Size = new System.Drawing.Size(643, 64);
            this.GroupBox5.TabIndex = 104;
            this.GroupBox5.TabStop = false;
            this.GroupBox5.Text = "样式";
            // 
            // picSample
            // 
            this.picSample.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picSample.Image = ((System.Drawing.Image)(resources.GetObject("picSample.Image")));
            this.picSample.Location = new System.Drawing.Point(15, 346);
            this.picSample.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picSample.Name = "picSample";
            this.picSample.Size = new System.Drawing.Size(163, 106);
            this.picSample.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSample.TabIndex = 100;
            this.picSample.TabStop = false;
            // 
            // FrmThemeBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 456);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.picSample);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.GroupBox6);
            this.Controls.Add(this.GroupBox5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmThemeBar";
            this.Text = "柱状图";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmThemeBar_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmThemeBar_FormClosed);
            this.Load += new System.EventHandler(this.FrmThemeBar_Load);
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.GroupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownWidth)).EndInit();
            this.GroupBox5.ResumeLayout(false);
            this.GroupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSample)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Button btnApply;
        internal System.Windows.Forms.ListBox ListBoxLayers;
        internal System.Windows.Forms.ImageList iltColorRamp;
        private System.Windows.Forms.Panel PanelBackGroudColor;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        internal System.Windows.Forms.PictureBox picSample;
        internal System.Windows.Forms.ImageList iltSecond;
        internal System.Windows.Forms.ImageList iltFirst;
        internal System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnAllRemove;
        private System.Windows.Forms.Button btnAllAdd;
        private System.Windows.Forms.Label Label4;
        private System.Windows.Forms.GroupBox GroupBox6;
        private System.Windows.Forms.Button btnSingleRemove;
        private System.Windows.Forms.Button btnSingleAdd;
        private System.Windows.Forms.ListBox listBoxField;
        private System.Windows.Forms.NumericUpDown numUpDownHeight;
        private System.Windows.Forms.NumericUpDown numUpDownWidth;
        private System.Windows.Forms.GroupBox GroupBox5;
        internal System.Windows.Forms.ColorDialog ColorDialog1;
    }
}