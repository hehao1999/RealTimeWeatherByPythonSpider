using System;
using System.Windows.Forms;

namespace mainView.Clock
{
    public partial class Clock : Form
    {
        public Clock()
        {
            //使用双缓冲减少延迟
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            InitializeComponent();
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            //UTC 时间
            DateTime dt = DateTime.Now;
            string[] day = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = day[Convert.ToInt32(dt.DayOfWeek.ToString("d"))].ToString();
            lblDay.Text = dt.ToString("yyyy-MM-dd") + " " + week;
            lblTime.Text = dt.ToString("HH:mm");
            lblTitle.Text = "（北京时间）";
        }
    }
}