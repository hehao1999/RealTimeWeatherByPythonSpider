namespace mainView.MapShows
{
    partial class FrmThemePie
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmThemePie));
            this.panelLineColor = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxField = new System.Windows.Forms.ListBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.numUpDownOutline = new System.Windows.Forms.NumericUpDown();
            this.ColorDialog1 = new System.Windows.Forms.ColorDialog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.GroupBox6 = new System.Windows.Forms.GroupBox();
            this.btnAllAdd = new System.Windows.Forms.Button();
            this.btnAllRemove = new System.Windows.Forms.Button();
            this.btnSingleRemove = new System.Windows.Forms.Button();
            this.btnSingleAdd = new System.Windows.Forms.Button();
            this.PanelBackGroudColor = new System.Windows.Forms.Panel();
            this.iltColorRamp = new System.Windows.Forms.ImageList(this.components);
            this.ListBoxLayers = new System.Windows.Forms.ListBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.GroupBox5 = new System.Windows.Forms.GroupBox();
            this.numUpDownSize = new System.Windows.Forms.NumericUpDown();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.iltSecond = new System.Windows.Forms.ImageList(this.components);
            this.btnApply = new System.Windows.Forms.Button();
            this.iltFirst = new System.Windows.Forms.ImageList(this.components);
            this.picSample = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownOutline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.GroupBox6.SuspendLayout();
            this.GroupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownSize)).BeginInit();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSample)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLineColor
            // 
            this.panelLineColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.panelLineColor.Location = new System.Drawing.Point(136, 70);
            this.panelLineColor.Margin = new System.Windows.Forms.Padding(4);
            this.panelLineColor.Name = "panelLineColor";
            this.panelLineColor.Size = new System.Drawing.Size(91, 26);
            this.panelLineColor.TabIndex = 5;
            this.panelLineColor.Click += new System.EventHandler(this.panelLineColor_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 70);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "轮廓线颜色:";
            // 
            // listBoxField
            // 
            this.listBoxField.FormattingEnabled = true;
            this.listBoxField.ItemHeight = 15;
            this.listBoxField.Location = new System.Drawing.Point(8, 21);
            this.listBoxField.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxField.Name = "listBoxField";
            this.listBoxField.Size = new System.Drawing.Size(145, 214);
            this.listBoxField.TabIndex = 1;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(249, 70);
            this.Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(75, 15);
            this.Label4.TabIndex = 6;
            this.Label4.Text = "背景颜色:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(8, 28);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(90, 15);
            this.Label1.TabIndex = 3;
            this.Label1.Text = "轮廓线宽度:";
            // 
            // numUpDownOutline
            // 
            this.numUpDownOutline.DecimalPlaces = 1;
            this.numUpDownOutline.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numUpDownOutline.Location = new System.Drawing.Point(136, 25);
            this.numUpDownOutline.Margin = new System.Windows.Forms.Padding(4);
            this.numUpDownOutline.Name = "numUpDownOutline";
            this.numUpDownOutline.Size = new System.Drawing.Size(91, 25);
            this.numUpDownOutline.TabIndex = 2;
            this.numUpDownOutline.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(229, 21);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(217, 215);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.Validated += new System.EventHandler(this.dataGridView1_Validated);
            // 
            // GroupBox6
            // 
            this.GroupBox6.Controls.Add(this.dataGridView1);
            this.GroupBox6.Controls.Add(this.btnAllAdd);
            this.GroupBox6.Controls.Add(this.btnAllRemove);
            this.GroupBox6.Controls.Add(this.btnSingleRemove);
            this.GroupBox6.Controls.Add(this.btnSingleAdd);
            this.GroupBox6.Controls.Add(this.listBoxField);
            this.GroupBox6.Location = new System.Drawing.Point(200, 5);
            this.GroupBox6.Margin = new System.Windows.Forms.Padding(4);
            this.GroupBox6.Name = "GroupBox6";
            this.GroupBox6.Padding = new System.Windows.Forms.Padding(4);
            this.GroupBox6.Size = new System.Drawing.Size(459, 251);
            this.GroupBox6.TabIndex = 98;
            this.GroupBox6.TabStop = false;
            this.GroupBox6.Text = "字段选择";
            // 
            // btnAllAdd
            // 
            this.btnAllAdd.Location = new System.Drawing.Point(163, 140);
            this.btnAllAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAllAdd.Name = "btnAllAdd";
            this.btnAllAdd.Size = new System.Drawing.Size(61, 29);
            this.btnAllAdd.TabIndex = 5;
            this.btnAllAdd.Text = ">>";
            this.btnAllAdd.UseVisualStyleBackColor = true;
            this.btnAllAdd.Click += new System.EventHandler(this.btnAllAdd_Click);
            // 
            // btnAllRemove
            // 
            this.btnAllRemove.Location = new System.Drawing.Point(163, 192);
            this.btnAllRemove.Margin = new System.Windows.Forms.Padding(4);
            this.btnAllRemove.Name = "btnAllRemove";
            this.btnAllRemove.Size = new System.Drawing.Size(61, 29);
            this.btnAllRemove.TabIndex = 4;
            this.btnAllRemove.Text = "<<";
            this.btnAllRemove.UseVisualStyleBackColor = true;
            this.btnAllRemove.Click += new System.EventHandler(this.btnAllRemove_Click);
            // 
            // btnSingleRemove
            // 
            this.btnSingleRemove.Location = new System.Drawing.Point(163, 88);
            this.btnSingleRemove.Margin = new System.Windows.Forms.Padding(4);
            this.btnSingleRemove.Name = "btnSingleRemove";
            this.btnSingleRemove.Size = new System.Drawing.Size(61, 29);
            this.btnSingleRemove.TabIndex = 3;
            this.btnSingleRemove.Text = "<";
            this.btnSingleRemove.UseVisualStyleBackColor = true;
            this.btnSingleRemove.Click += new System.EventHandler(this.btnSingleRemove_Click);
            // 
            // btnSingleAdd
            // 
            this.btnSingleAdd.Location = new System.Drawing.Point(161, 35);
            this.btnSingleAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnSingleAdd.Name = "btnSingleAdd";
            this.btnSingleAdd.Size = new System.Drawing.Size(63, 29);
            this.btnSingleAdd.TabIndex = 2;
            this.btnSingleAdd.Text = ">";
            this.btnSingleAdd.UseVisualStyleBackColor = true;
            this.btnSingleAdd.Click += new System.EventHandler(this.btnSingleAdd_Click);
            // 
            // PanelBackGroudColor
            // 
            this.PanelBackGroudColor.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.PanelBackGroudColor.Location = new System.Drawing.Point(356, 70);
            this.PanelBackGroudColor.Margin = new System.Windows.Forms.Padding(4);
            this.PanelBackGroudColor.Name = "PanelBackGroudColor";
            this.PanelBackGroudColor.Size = new System.Drawing.Size(91, 26);
            this.PanelBackGroudColor.TabIndex = 7;
            this.PanelBackGroudColor.Click += new System.EventHandler(this.PanelBackGroudColor_Click);
            // 
            // iltColorRamp
            // 
            this.iltColorRamp.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iltColorRamp.ImageStream")));
            this.iltColorRamp.TransparentColor = System.Drawing.Color.Transparent;
            this.iltColorRamp.Images.SetKeyName(0, "YellowToRed.bmp");
            this.iltColorRamp.Images.SetKeyName(1, "GreenToBlue.bmp");
            this.iltColorRamp.Images.SetKeyName(2, "GreenToRed.bmp");
            // 
            // ListBoxLayers
            // 
            this.ListBoxLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListBoxLayers.FormattingEnabled = true;
            this.ListBoxLayers.ItemHeight = 15;
            this.ListBoxLayers.Location = new System.Drawing.Point(4, 22);
            this.ListBoxLayers.Margin = new System.Windows.Forms.Padding(4);
            this.ListBoxLayers.Name = "ListBoxLayers";
            this.ListBoxLayers.Size = new System.Drawing.Size(175, 225);
            this.ListBoxLayers.TabIndex = 0;
            this.ListBoxLayers.SelectedIndexChanged += new System.EventHandler(this.ListBoxLayers_SelectedIndexChanged);
            this.ListBoxLayers.DoubleClick += new System.EventHandler(this.ListBoxLayers_DoubleClick);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(281, 28);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(45, 15);
            this.Label2.TabIndex = 3;
            this.Label2.Text = "大小:";
            // 
            // GroupBox5
            // 
            this.GroupBox5.Controls.Add(this.PanelBackGroudColor);
            this.GroupBox5.Controls.Add(this.Label4);
            this.GroupBox5.Controls.Add(this.numUpDownSize);
            this.GroupBox5.Controls.Add(this.Label2);
            this.GroupBox5.Controls.Add(this.panelLineColor);
            this.GroupBox5.Controls.Add(this.label3);
            this.GroupBox5.Controls.Add(this.Label1);
            this.GroupBox5.Controls.Add(this.numUpDownOutline);
            this.GroupBox5.Location = new System.Drawing.Point(200, 264);
            this.GroupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.GroupBox5.Name = "GroupBox5";
            this.GroupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.GroupBox5.Size = new System.Drawing.Size(459, 104);
            this.GroupBox5.TabIndex = 97;
            this.GroupBox5.TabStop = false;
            this.GroupBox5.Text = "样式";
            // 
            // numUpDownSize
            // 
            this.numUpDownSize.Location = new System.Drawing.Point(356, 25);
            this.numUpDownSize.Margin = new System.Windows.Forms.Padding(4);
            this.numUpDownSize.Name = "numUpDownSize";
            this.numUpDownSize.Size = new System.Drawing.Size(91, 25);
            this.numUpDownSize.TabIndex = 2;
            this.numUpDownSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(453, 389);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 32);
            this.btnCancel.TabIndex = 95;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(280, 389);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 32);
            this.btnOk.TabIndex = 94;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.ListBoxLayers);
            this.GroupBox1.Location = new System.Drawing.Point(9, 5);
            this.GroupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.GroupBox1.Size = new System.Drawing.Size(183, 251);
            this.GroupBox1.TabIndex = 92;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Tag = "";
            this.GroupBox1.Text = "图层";
            // 
            // iltSecond
            // 
            this.iltSecond.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.iltSecond.ImageSize = new System.Drawing.Size(16, 16);
            this.iltSecond.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(107, 389);
            this.btnApply.Margin = new System.Windows.Forms.Padding(4);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(100, 32);
            this.btnApply.TabIndex = 96;
            this.btnApply.Text = "应用";
            this.btnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // iltFirst
            // 
            this.iltFirst.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.iltFirst.ImageSize = new System.Drawing.Size(16, 16);
            this.iltFirst.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // picSample
            // 
            this.picSample.Image = ((System.Drawing.Image)(resources.GetObject("picSample.Image")));
            this.picSample.Location = new System.Drawing.Point(9, 264);
            this.picSample.Margin = new System.Windows.Forms.Padding(4);
            this.picSample.Name = "picSample";
            this.picSample.Size = new System.Drawing.Size(183, 104);
            this.picSample.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSample.TabIndex = 93;
            this.picSample.TabStop = false;
            // 
            // FrmThemePie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 426);
            this.Controls.Add(this.GroupBox6);
            this.Controls.Add(this.GroupBox5);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.picSample);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmThemePie";
            this.Text = "饼状统计图图";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmThemePie_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmThemePie_FormClosed);
            this.Load += new System.EventHandler(this.FrmThemePie_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownOutline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.GroupBox6.ResumeLayout(false);
            this.GroupBox5.ResumeLayout(false);
            this.GroupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownSize)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSample)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLineColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxField;
        private System.Windows.Forms.Label Label4;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.NumericUpDown numUpDownOutline;
        internal System.Windows.Forms.ColorDialog ColorDialog1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox GroupBox6;
        private System.Windows.Forms.Button btnAllAdd;
        private System.Windows.Forms.Button btnAllRemove;
        private System.Windows.Forms.Button btnSingleRemove;
        private System.Windows.Forms.Button btnSingleAdd;
        private System.Windows.Forms.Panel PanelBackGroudColor;
        internal System.Windows.Forms.ImageList iltColorRamp;
        internal System.Windows.Forms.ListBox ListBoxLayers;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.GroupBox GroupBox5;
        private System.Windows.Forms.NumericUpDown numUpDownSize;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.ImageList iltSecond;
        private System.Windows.Forms.Button btnApply;
        internal System.Windows.Forms.PictureBox picSample;
        internal System.Windows.Forms.ImageList iltFirst;
    }
}