using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using mainView.AttrFrm;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace mainView
{
    public partial class mainForm : Form
    {
        /// <summary>
        /// 初始化全局变量，便于访问
        /// </summary>
        public static IMapControl3 m_mapControl = null;

        public AxTOCControl pCurrentTOC = null;
        public AxMapControl pCurrentMap = null;
        public static IExtentStack pExtentStack;
        private Sunisoft.IrisSkin.SkinEngine se = null;
        private string city = "西安";

        #region 天气数据显示

        public void ShowWF(string col, string city_name)
        {
            Dictionary<string, string> myDictionary = new Dictionary<string, string>();
            myDictionary = DataBaseClass.DBFuncs.GetWF(col, city_name);
            城市labelItem8.Text = "城市名称：" + myDictionary["城市名称"];
            天气labelItem1.Text = "天气概要：" + myDictionary["天气概要"];
            平均气温labelItem2.Text = "平均气温：" + myDictionary["平均气温"];
            最高气温labelItem3.Text = "最高气温：" + myDictionary["最高气温"];
            最低气温labelItem4.Text = "最低气温：" + myDictionary["最低气温"];
            空气湿度labelItem5.Text = "空气湿度：" + myDictionary["空气湿度"];
            风力信息labelItem1.Text = "风力信息：" + myDictionary["风力信息"];
            紫外线labelItem8.Text = "紫外线强度：" + myDictionary["紫外线强度"].Split(' ')[0];
            预警信息labelItem3.Text = "预警信息：" + myDictionary["预警信息"];
            labelItem1.Text = "*" + myDictionary["2"];
            labelItem2.Text = "*" + myDictionary["3"];
            labelItem3.Text = "*" + myDictionary["4"];
            labelItem4.Text = "*" + myDictionary["5"];
        }

        public void ShowZhishu(string col, string city_name)
        {
            Dictionary<string, string> myDictionary = new Dictionary<string, string>();
            myDictionary = DataBaseClass.DBFuncs.GetWF(col, city_name);
            string[] strs = { "舒适度", "感冒指数", "旅游指数", "穿衣指数", "雨伞指数", "运动指数", "晨练指数", "晾晒指数", "洗车指数", "约会指数" };

            foreach (string str in strs)
            {
                if (comboBoxItem1.SelectedItem.ToString() == str)
                {
                    labelItem7.Text = myDictionary[str].Split(' ')[0];
                    labelItem8.Text = myDictionary[str].Split(' ')[1];
                }
            }
        }

        #endregion 天气数据显示

        public mainForm()
        {
            se = new Sunisoft.IrisSkin.SkinEngine();
            //combobox和滚动条重绘存在问题，需要删除
            se.DisableTag = 888;
            se.SkinFile = @"skin.ssk";
            se.SkinScrollBar = false;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //打开地图文档
            axMapControl1.LoadMxFile(@".\dataMap\shanxi.mxd");
            //axTOCControl加到容器中后必须使用代码绑定axMapControl
            axTOCControl1.SetBuddyControl(axMapControl1);
            //初始化全局变量对象
            m_mapControl = (IMapControl3)axMapControl1.Object;
            pCurrentTOC = this.axTOCControl1;
            pCurrentMap = this.axMapControl1;
            Command.FullView();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            attr.RefreshAttrTable();
            comboBoxItem1.SelectedItem = comboBoxItem1.Items[0];
            ShowWF("weather", city);
            this.textBox1.Text = DataBaseClass.DBFuncs.GetAQF(@"./py/AQF.txt");
        }

        private void SplitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
            Command.ShowTime(this);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if ((DateTime.Now.Minute.ToString() == "0" || DateTime.Now.Minute.ToString() == "00") &&
                (DateTime.Now.Second.ToString() == "0" || DateTime.Now.Second.ToString() == "00"))
            {
                attr.RefreshAttrTable();
                comboBoxItem1.SelectedItem = comboBoxItem1.Items[0];
                ShowWF("weather", city);
                this.textBox1.Text = DataBaseClass.DBFuncs.GetAQF(@"./py/AQF.txt");
                this.Refresh();
                if (DataBaseClass.DBFuncs.GetWF("weather", "西安")["预警信息"] != "0")
                {
                    Clock.Email.SendEmail("ahuang_86@163.com", DataBaseClass.DBFuncs.GetWF("weather", "西安")["预警信息"]);
                }
            }
        }

        private void 属性查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAttriQuery frm = new frmAttriQuery();
            frm.Show();
        }

        private void 柱状图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pCurrentMap.Map.LayerCount == 0)
            {
                MessageBox.Show("当前系统中没有地图数据，无法进行柱状专题图渲染!");
                return;
            }
            if (MapShows.FrmThemeBar.TmpFrmThemeBar == null)
            {
                MapShows.FrmThemeBar.TmpFrmThemeBar = new MapShows.FrmThemeBar(this);
                MapShows.FrmThemeBar.TmpFrmThemeBar.Show();
            }
            else
            {
                MapShows.FrmThemeBar.TmpFrmThemeBar.Activate();
            }
        }

        private void 分级显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pCurrentMap.Map.LayerCount == 0)
            {
                MessageBox.Show("当前系统中没有地图数据，无法进行等级专题图渲染!");
                return;
            }

            if (MapShows.FrmThemeGraduate.TmpFrmThemeGraduate == null)
            {
                MapShows.FrmThemeGraduate.TmpFrmThemeGraduate = new MapShows.FrmThemeGraduate(this);
                MapShows.FrmThemeGraduate.TmpFrmThemeGraduate.Show();
            }
            else
            {
                MapShows.FrmThemeGraduate.TmpFrmThemeGraduate.Activate();
            }
        }

        private void 邮件测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clock.Email.SendEmail("haoh_gis@163.com", "RealTimeWeather测试预警邮件");
        }

        private void 退出系统ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要关闭系统吗？", "询问", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            else
                this.Close();
        }

        private void 放大toolStripButton_Click(object sender, EventArgs e)
        {
            Command.ZoomIn();
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            Command.ZoomOut();
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            Command.Pan();
        }

        private void ToolStripButton6_Click(object sender, EventArgs e)
        {
            Command.FullView();
        }

        private void ToolStripButton8_Click(object sender, EventArgs e)
        {
            Command.Default();
        }

        private void 上一视图toolStripButton_Click(object sender, EventArgs e)
        {
            Command.BackView();
        }

        private void 下一视图toolStripButton_Click(object sender, EventArgs e)
        {
            Command.NextView();
        }

        private void 源码获取ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Command.GoToGitHub();
        }

        private void ComboBoxItem1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowZhishu("weather", city);
        }

        private void 预警设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clock.alarm alarm = new Clock.alarm();
            alarm.Show();
        }

        private void AQI小时ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            table1 table = new table1();
            table.Show();
        }

        private void 饼状图ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MapShows.FrmThemePie frmpie = new MapShows.FrmThemePie(this);
            frmpie.Show();
        }

        private void 唯一值显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapShows.FrmThemeUniqueValue uniqfrm = new MapShows.FrmThemeUniqueValue(this);
            uniqfrm.Show();
        }

        private void 空气质量表天ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void 导出属性表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainView.DataBaseClass.dataexport frm = new DataBaseClass.dataexport();
            frm.Show();
        }

        private void 导出当前地图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Command.ExportMap();
        }

        private void AxMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {//鼠标移上去时展示空气质量指标
            IFeatureLayer pFeatureLayer = axMapControl1.Map.get_Layer(0) as IFeatureLayer;
            pFeatureLayer.DisplayField = "Name";
            pFeatureLayer.ShowTips = true;
            string pTip1, pTip2;
            pTip1 = pFeatureLayer.get_TipText(e.mapX, e.mapY, axMapControl1.ActiveView.FullExtent.Width / 10000);
            if (pTip1 == null)
                return;
            if (pTip1.Length > 2)
            {
                pFeatureLayer.DisplayField = "AQI";
                pTip2 = pTip1 + "\n" + "AQI:       " + pFeatureLayer.get_TipText(e.mapX, e.mapY, axMapControl1.ActiveView.FullExtent.Width / 10000);
                pFeatureLayer.DisplayField = "PRIMARYPOLLUTANT";
                pTip1 = pTip2 + "\n" + "主要污染物: " + pFeatureLayer.get_TipText(e.mapX, e.mapY, axMapControl1.ActiveView.FullExtent.Width / 10000);
                pFeatureLayer.DisplayField = "QUALITY";
                pTip2 = pTip1 + "\n" + "空气质量：  " + pFeatureLayer.get_TipText(e.mapX, e.mapY, axMapControl1.ActiveView.FullExtent.Width / 10000) + "\n";
            }
            else
            {
                pTip2 = "";
            }
            toolTip1.SetToolTip(axMapControl1, pTip2);
        }

        private void AxMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
        }

        private void AxMapControl1_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            IFeatureLayer pFeatureLayer = axMapControl1.Map.get_Layer(0) as IFeatureLayer;
            pFeatureLayer.DisplayField = "Name";
            string tmp = city;
            try
            {
                city = pFeatureLayer.get_TipText(e.mapX, e.mapY, axMapControl1.ActiveView.FullExtent.Width / 10000);
                city = city.Split('市')[0];
                ShowWF("weather", city);
                ShowZhishu("weather", city);
            }
            catch
            {
                city = tmp;
                ShowWF("weather", city);
                ShowZhishu("weather", city);
            }
        }
    }
}