namespace GISDemo
{
    partial class FrmThemeDot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmThemeDot));
            this.PanelDotColor = new System.Windows.Forms.Panel();
            this.PanelBackGroudColor = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.UpDownDotValue = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.UpDownDotSize = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.ListBoxLayers = new System.Windows.Forms.ListBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.ColorDialog1 = new System.Windows.Forms.ColorDialog();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmbFields = new System.Windows.Forms.ComboBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.picSample = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownDotValue)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownDotSize)).BeginInit();
            this.GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSample)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelDotColor
            // 
            this.PanelDotColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.PanelDotColor.Location = new System.Drawing.Point(89, 96);
            this.PanelDotColor.Name = "PanelDotColor";
            this.PanelDotColor.Size = new System.Drawing.Size(65, 21);
            this.PanelDotColor.TabIndex = 15;
            this.PanelDotColor.Click += new System.EventHandler(this.PanelDotColor_Click);
            // 
            // PanelBackGroudColor
            // 
            this.PanelBackGroudColor.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.PanelBackGroudColor.Location = new System.Drawing.Point(89, 134);
            this.PanelBackGroudColor.Name = "PanelBackGroudColor";
            this.PanelBackGroudColor.Size = new System.Drawing.Size(65, 21);
            this.PanelBackGroudColor.TabIndex = 15;
            this.PanelBackGroudColor.Click += new System.EventHandler(this.PanelBackGroudColor_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "地图背景色：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "单个点的值：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "符号的颜色：";
            // 
            // UpDownDotValue
            // 
            this.UpDownDotValue.Location = new System.Drawing.Point(89, 57);
            this.UpDownDotValue.Name = "UpDownDotValue";
            this.UpDownDotValue.Size = new System.Drawing.Size(65, 21);
            this.UpDownDotValue.TabIndex = 9;
            this.UpDownDotValue.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.PanelDotColor);
            this.groupBox2.Controls.Add(this.PanelBackGroudColor);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.UpDownDotValue);
            this.groupBox2.Controls.Add(this.UpDownDotSize);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(368, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(174, 170);
            this.groupBox2.TabIndex = 92;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "渲染属性设置";
            // 
            // UpDownDotSize
            // 
            this.UpDownDotSize.Location = new System.Drawing.Point(89, 20);
            this.UpDownDotSize.Name = "UpDownDotSize";
            this.UpDownDotSize.Size = new System.Drawing.Size(65, 21);
            this.UpDownDotSize.TabIndex = 9;
            this.UpDownDotSize.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "点符号尺寸：";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(447, 306);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 26);
            this.buttonCancel.TabIndex = 90;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // ListBoxLayers
            // 
            this.ListBoxLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListBoxLayers.FormattingEnabled = true;
            this.ListBoxLayers.ItemHeight = 12;
            this.ListBoxLayers.Location = new System.Drawing.Point(3, 17);
            this.ListBoxLayers.Name = "ListBoxLayers";
            this.ListBoxLayers.Size = new System.Drawing.Size(153, 256);
            this.ListBoxLayers.TabIndex = 0;
            this.ListBoxLayers.SelectedIndexChanged += new System.EventHandler(this.ListBoxLayers_SelectedIndexChanged);
            this.ListBoxLayers.DoubleClick += new System.EventHandler(this.ListBoxLayers_DoubleClick);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(340, 306);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 26);
            this.buttonOk.TabIndex = 89;
            this.buttonOk.Text = "确定";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.DataGridView1);
            this.GroupBox3.Controls.Add(this.cmbFields);
            this.GroupBox3.Location = new System.Drawing.Point(167, 6);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(195, 280);
            this.GroupBox3.TabIndex = 88;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "字段";
            // 
            // DataGridView1
            // 
            this.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Location = new System.Drawing.Point(6, 45);
            this.DataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.DataGridView1.MultiSelect = false;
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.RowHeadersVisible = false;
            this.DataGridView1.RowTemplate.Height = 27;
            this.DataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DataGridView1.Size = new System.Drawing.Size(184, 228);
            this.DataGridView1.TabIndex = 85;
            // 
            // cmbFields
            // 
            this.cmbFields.FormattingEnabled = true;
            this.cmbFields.Location = new System.Drawing.Point(6, 20);
            this.cmbFields.Name = "cmbFields";
            this.cmbFields.Size = new System.Drawing.Size(183, 20);
            this.cmbFields.TabIndex = 0;
            this.cmbFields.SelectedIndexChanged += new System.EventHandler(this.cmbFields_SelectedIndexChanged);
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(233, 306);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 26);
            this.btnApply.TabIndex = 91;
            this.btnApply.Text = "应用";
            this.btnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.ListBoxLayers);
            this.GroupBox1.Location = new System.Drawing.Point(2, 6);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(159, 280);
            this.GroupBox1.TabIndex = 86;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Tag = "";
            this.GroupBox1.Text = "图层";
            // 
            // picSample
            // 
            this.picSample.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picSample.Image = ((System.Drawing.Image)(resources.GetObject("picSample.Image")));
            this.picSample.Location = new System.Drawing.Point(368, 182);
            this.picSample.Name = "picSample";
            this.picSample.Size = new System.Drawing.Size(174, 104);
            this.picSample.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSample.TabIndex = 87;
            this.picSample.TabStop = false;
            // 
            // FrmThemeDot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 338);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.picSample);
            this.Controls.Add(this.GroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmThemeDot";
            this.Text = "点密度图";
            this.Load += new System.EventHandler(this.FrmThemeDot_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmThemeDot_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmThemeDot_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.UpDownDotValue)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownDotSize)).EndInit();
            this.GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSample)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelDotColor;
        private System.Windows.Forms.Panel PanelBackGroudColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown UpDownDotValue;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown UpDownDotSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonCancel;
        internal System.Windows.Forms.ListBox ListBoxLayers;
        private System.Windows.Forms.Button buttonOk;
        internal System.Windows.Forms.ColorDialog ColorDialog1;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.DataGridView DataGridView1;
        internal System.Windows.Forms.ComboBox cmbFields;
        private System.Windows.Forms.Button btnApply;
        internal System.Windows.Forms.PictureBox picSample;
        internal System.Windows.Forms.GroupBox GroupBox1;
    }
}