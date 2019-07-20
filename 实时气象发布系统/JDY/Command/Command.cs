using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using System.Diagnostics;
using System.Windows.Forms;

namespace mainView
{
    internal class Command
    {
        /// <summary>
        /// 放大鼠标模式
        /// </summary>
        public static void ZoomIn()
        {
            mainForm.m_mapControl.CurrentTool = null;
            ICommand pZoomIn = new ControlsMapZoomInToolClass();
            pZoomIn.OnCreate(mainForm.m_mapControl.Object);
            mainForm.m_mapControl.CurrentTool = pZoomIn as ITool;
        }

        /// <summary>
        /// 缩小鼠标模式
        /// </summary>
        public static void ZoomOut()
        {
            mainForm.m_mapControl.CurrentTool = null;
            ICommand pZoomOut = new ControlsMapZoomOutToolClass();
            pZoomOut.OnCreate(mainForm.m_mapControl.Object);
            mainForm.m_mapControl.CurrentTool = pZoomOut as ITool;
        }

        /// <summary>
        /// 全图鼠标模式
        /// </summary>
        public static void FullView()
        {
            mainForm.m_mapControl.CurrentTool = null;
            ICommand pFullExtent = new ControlsMapFullExtentCommandClass();
            pFullExtent.OnCreate(mainForm.m_mapControl.Object);
            pFullExtent.OnClick();
        }

        /// <summary>
        /// 漫游鼠标模式
        /// </summary>
        public static void Pan()
        {
            mainForm.m_mapControl.CurrentTool = null;
            ICommand command = new ControlsMapPanToolClass();
            command.OnCreate(mainForm.m_mapControl.Object);
            mainForm.m_mapControl.CurrentTool = command as ITool;
        }

        /// <summary>
        /// 默认鼠标模式
        /// </summary>
        public static void Default()
        {
            mainForm.m_mapControl.CurrentTool = null;
            ICommand command = new ControlsClearSelectionCommand();
            command.OnCreate(mainForm.m_mapControl.Object);
            command.OnClick();
        }

        /// <summary>
        /// 上一视图
        /// </summary>
        public static void BackView()
        {
            try
            {
                mainForm.pExtentStack = mainForm.m_mapControl.ActiveView.ExtentStack;
                mainForm.pExtentStack.Undo();
                mainForm.m_mapControl.ActiveView.Refresh();
            }
            catch
            {
                MessageBox.Show("已到达第一个视图！", "提示");
            }
        }

        /// <summary>
        /// 下一视图
        /// </summary>
        public static void NextView()
        {
            try
            {
                mainForm.pExtentStack = mainForm.m_mapControl.ActiveView.ExtentStack;
                mainForm.pExtentStack.Redo();
                mainForm.m_mapControl.ActiveView.Refresh();
            }
            catch
            {
                MessageBox.Show("已到达最后一个视图！", "提示");
            }
        }

        /// <summary>
        /// 转到GitHub
        /// </summary>
        public static void GoToGitHub()
        {
            try
            {
                if (MessageBox.Show("将打开浏览器，您确定要继续吗", "询问", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    System.Diagnostics.Process.Start(@"https://github.com/hehao1999");
                else
                    return;
            }
            catch
            {
                MessageBox.Show("浏览器不可用或链接已失效，请联系软件所有者！", "提示");
            }
        }

        /// <summary>
        /// 时钟显示
        /// 注：使用窗体，慢，有待修改
        /// </summary>
        /// <param name="mainForm"></param>
        public static void ShowTime(mainForm mainForm)
        {
            Clock.Clock frmClock = new Clock.Clock();
            frmClock.TopLevel = false;
            frmClock.Parent = mainForm.splitContainer1.Panel1;
            frmClock.WindowState = FormWindowState.Maximized;
            frmClock.Show();
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public static void DataBaseRefresh()
        {
            string sArguments = @"__init__.py";//这里是python的文件名字
            RunPythonScript(sArguments, "-u", null);//调用python核心代码
        }

        /// <summary>
        /// C#调用Python虚拟环境运行Python程序
        /// </summary>
        /// <param name="sArgName">需运行文件的名称</param>
        /// <param name="args">参数</param>
        /// <param name="teps">实参</param>
        public static void RunPythonScript(string sArgName, string args = "", params string[] teps)
        {
            Process p = new Process();
            string path = @".\py\" + sArgName;
            p.StartInfo.FileName = @".\py\venv\Scripts\python.exe";//虚拟环境
            string sArguments = path;
            if (teps != null)
            {
                foreach (string sigstr in teps)
                {
                    sArguments += " " + sigstr;//传递参数
                }
            }
            sArguments += " " + args;
            p.StartInfo.Arguments = sArguments;
            p.StartInfo.UseShellExecute = false;
            //p.StartInfo.RedirectStandardOutput = false;
            //p.StartInfo.RedirectStandardInput = false;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            MessageBox.Show("数据更新中，请稍后！", "提示");
            p.WaitForExit();
            p.Close();
        }

        public static void ExportMap()
        {
            IHookHelper map_hookHelper = new HookHelperClass();
            //参数赋值
            map_hookHelper.Hook = mainForm.m_mapControl.Object;
            export_file frmExportDlg = new export_file(map_hookHelper);
            frmExportDlg.Show();
        }
    }
}