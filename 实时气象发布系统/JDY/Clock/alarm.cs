using System;
using System.Windows.Forms;

namespace mainView.Clock
{
    public partial class alarm : Form
    {
        public alarm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否发送测试邮件到指定邮箱，是否继续？", "询问", MessageBoxButtons.YesNo) != DialogResult.Yes)
                ///这里将来写更改配置文件的函数
                ///
                ///
                return;
            else
            {
                try
                {
                    MessageBox.Show("已经发送测试邮件到指定邮箱", "提示");
                    Email.SendEmail(textBox1.Text, "RealTimeWeather测试预警邮件");
                }
                catch
                {
                    MessageBox.Show("请输入正确的邮箱地址", "提示");
                }
            }
        }
    }
}