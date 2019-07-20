using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace mainView.DataBaseClass
{
    public partial class dataexport : Form
    {
        public dataexport()
        {
            InitializeComponent();
        }

        private void ExcuteDosCommand(string cmd)
        {
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = "cmd";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                StreamWriter cmdWriter = p.StandardInput;
                p.BeginOutputReadLine();
                if (!String.IsNullOrEmpty(cmd))
                {
                    cmdWriter.WriteLine(cmd);
                }
                cmdWriter.Close();
                p.WaitForExit();
                p.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("执行命令失败，请检查输入的命令是否正确！");
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel文件（*.csv）|*.csv";
            sfd.RestoreDirectory = true;
            sfd.RestoreDirectory = true;
            sfd.Title = "保存";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string path = sfd.FileName.ToString();
                if (comboBox1.SelectedIndex == 0)
                    ExcuteDosCommand("mongoexport.exe --type=csv -d RealTimeWeather -c day7 -f city_code,day_num,TIMEPOINT,AQI,PRIMARYPOLLUTANT,QUALITY -o " + path);
                else if (comboBox1.SelectedIndex == 1)
                    ExcuteDosCommand("mongoexport.exe --type=csv -d RealTimeWeather -c hour24 -f city_code,TIMEPOINT,TIMEPOINT1,AQI,PRIMARYPOLLUTANT,QUALITY,ISO2,INO2,ICO,IO3,IPM10,IPM2_5,SO2,NO2,CO,O3,PM10,PM2_5  -o " + path);
                else
                    return;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}