using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Output;
using System;
using System.Windows.Forms;

namespace mainView
{
    public partial class export_file : Form
    {
        private IActiveView pActiveView = null;
        private string filename;

        public export_file(IHookHelper map_hookHelper)
        {
            InitializeComponent();
            pActiveView = map_hookHelper.ActiveView;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog pSaveFileDialog = new SaveFileDialog
            {
                Title = "请选择保存路径",
                OverwritePrompt = true,
                RestoreDirectory = true,
                Filter = "图像格式（*.emf）|*.emf"
            };

            if (pSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                filename = pSaveFileDialog.FileName;
                textBox1.Text = filename;
                return;
            }
            else
            {
                return;
            }
        }

        private void ExportEMF(string filename, IActiveView activeView, int resol)
        {
            IActiveView pActiveView = activeView;
            IExport pExport = new ExportEMFClass();
            pExport.ExportFileName = filename;
            pExport.Resolution = resol;
            tagRECT exportRECT = pActiveView.ExportFrame;
            IEnvelope pPixelBoundsEnv; pPixelBoundsEnv = new EnvelopeClass();
            pPixelBoundsEnv.PutCoords(exportRECT.left, exportRECT.top, exportRECT.right, exportRECT.bottom);
            pExport.PixelBounds = pPixelBoundsEnv;
            int hDC = pExport.StartExporting();
            pActiveView.Output(hDC, (int)pExport.Resolution, ref exportRECT, null, null);
            pExport.FinishExporting();
            pExport.Cleanup();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ExportEMF(filename, pActiveView, Convert.ToInt32(numericUpDown1.Value));
            this.Close();
        }
    }
}