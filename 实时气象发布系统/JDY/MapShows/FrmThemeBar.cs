﻿using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace mainView.MapShows
{
    public partial class FrmThemeBar : Form
    {
        private mainForm _frmMain;

        #region 控制只打开一个界面

        public static FrmThemeBar TmpFrmThemeBar = null;

        #endregion 控制只打开一个界面

        public FrmThemeBar()
        {
            InitializeComponent();
        }

        public FrmThemeBar(mainForm TmpFrom)
        {
            InitializeComponent();
            _frmMain = TmpFrom;
            this.Map = _frmMain.pCurrentMap.Map;
        }

        #region "定义变量"

        //要生成饼状图的地图
        public IMap m_pMap;

        //当前地图图层
        private ArrayList m_colMapLayers;

        //要生成饼状图的图层
        private ILayer m_pLayer;

        //当前选择的图层
        private IGeoFeatureLayer m_pGeoFeatureLayer;

        //当前选中的图层类型
        private string m_strShapeType;

        //当前图层中适合进行饼状渲染的字段集合
        private ArrayList m_colValidFeildsArray = new ArrayList();

        //字段数组
        private IArray m_pLayerFieldsArray;

        //渲染符号集合
        private IArray m_pSymbolsArray;

        //背景的面符号
        private ISimpleFillSymbol m_pDefaultFillSymbol;

        //背景的线符号
        private ISimpleLineSymbol m_pDefaultLineSymbol;

        //背景的点符号
        private ISimpleMarkerSymbol m_pDefaultMarkerSymbol;

        //字段个数及bars的个数
        private int m_FieldsCount;

        //定义图表渲染 对象
        private IChartRenderer m_pChartRenderer;

        //定义饼状图渲染对象
        private IPieChartRenderer m_pBarChartRender;

        //定义统计字段值的对象
        private IStatisticsResults m_pStatisticsResults;

        //根据图层初始化窗体界面参数
        private bool m_bIsInitializing;

        //定义选中字段显示的表格对象
        private DataTable m_pDataTable = new DataTable();

        private DataColumn m_symbolCol = new DataColumn("颜色");
        private DataColumn m_valueCol = new DataColumn("字段");

        //图层原始渲染对像
        private IFeatureRenderer m_pFeatureRenderer;

        #endregion "定义变量"

        #region "'初始化变量"

        private void InitVariables()
        {
            m_colMapLayers = null;
            m_pSymbolsArray = null;
            m_pGeoFeatureLayer = null;
            m_pDefaultFillSymbol = null;
            m_pDefaultLineSymbol = null;
            m_pDefaultMarkerSymbol = null;
            m_pStatisticsResults = null;
            m_bIsInitializing = false;
            m_FieldsCount = 0;

            if (m_pMap == null)
            {
                return;
            }

            m_pSymbolsArray = new ESRI.ArcGIS.esriSystem.Array();

            if (m_pSymbolsArray == null)
            {
                MessageBox.Show("初始化分类对话框失败。");
                return;
            }
        }

        #endregion "'初始化变量"

        #region "窗体需要设置的参数之一（可选）"

        public IMap Map
        {
            get { return m_pMap; }
            set { m_pMap = value; }
        }

        #endregion "窗体需要设置的参数之一（可选）"

        #region "得到地图中的featurelayer的名字集合"

        public ArrayList GetMapFeatLayers(IMap m_pMap)
        {
            ArrayList functionReturnValue = default(ArrayList);

            functionReturnValue = null;
            try
            {
                ArrayList pColFeatureLayers = default(ArrayList);
                //要素图层集合接口
                ILayer pLayer = default(ILayer);
                //图层接口
                IEnumLayer pEnumLayer = default(IEnumLayer);
                //枚举图层接口
                //判断地图中是否有图层
                if (m_pMap.LayerCount == 0)
                {
                    return null;
                }
                pColFeatureLayers = new ArrayList();
                //实例化图层集合对象
                pEnumLayer = m_pMap.get_Layers(null, true);
                pLayer = pEnumLayer.Next();
                while ((pLayer != null))
                {
                    if (pLayer is IFeatureLayer)
                    {
                        pColFeatureLayers.Add(pLayer.Name);
                    }
                    pLayer = pEnumLayer.Next();
                }
                functionReturnValue = pColFeatureLayers;
                pLayer = null;
                pEnumLayer = null;
                pColFeatureLayers = null;
            }
            catch (Exception ex)
            {
                functionReturnValue = null;
                MessageBox.Show(ex.ToString());
            }
            return functionReturnValue;
        }

        #endregion "得到地图中的featurelayer的名字集合"

        #region "生成默认的面符号、线符号和点符号"

        private void CreateDefaultSymbols()
        {
            //面符号
            ISimpleFillSymbol pFillSymbol = default(ISimpleFillSymbol);
            pFillSymbol = new SimpleFillSymbol();

            IRgbColor pRgbColor = default(IRgbColor);
            pRgbColor = new RgbColor();

            {
                pRgbColor.Red = PanelBackGroudColor.BackColor.R;
                pRgbColor.Green = PanelBackGroudColor.BackColor.G;
                pRgbColor.Blue = PanelBackGroudColor.BackColor.B;
                pRgbColor.UseWindowsDithering = true;
            }

            pFillSymbol.Color = pRgbColor;
            pRgbColor = null;
            pRgbColor = new RgbColor();

            {
                pRgbColor.Red = 0;
                pRgbColor.Green = 0;
                pRgbColor.Blue = 0;
                pRgbColor.UseWindowsDithering = true;
            }

            pFillSymbol.Outline.Color = pRgbColor;

            m_pDefaultFillSymbol = pFillSymbol;
            pFillSymbol = null;
            pRgbColor = null;

            pRgbColor = new RgbColor();
            {
                pRgbColor.Red = PanelBackGroudColor.BackColor.R;
                pRgbColor.Green = PanelBackGroudColor.BackColor.G;
                pRgbColor.Blue = PanelBackGroudColor.BackColor.B;
                pRgbColor.UseWindowsDithering = true;
            }
        }

        #endregion "生成默认的面符号、线符号和点符号"

        #region "加载背景符号"

        private void LoadDefaultSymbols()
        {
            Debug.Assert(!string.IsNullOrEmpty(m_strShapeType));
            if (string.IsNullOrEmpty(m_strShapeType))
                return;

            //当前选中的图层类型为面状
            if (m_strShapeType == "Fill Symbols")
            {
                // Debug.Assert((m_pDefaultMarkerSymbol != null));
                if (m_pDefaultMarkerSymbol == null)
                    return;

                Debug.Assert((m_pDefaultFillSymbol != null));
                if (m_pDefaultFillSymbol == null)
                    return;
            }
        }

        #endregion "加载背景符号"

        #region "得到当前图层的渲染符号"

        public IArray GetLayerSymbols(ILayer pLayer)
        {
            IArray functionReturnValue = default(IArray);
            functionReturnValue = null;
            if (pLayer == null)
            {
                return null;
            }
            int i = 0;
            IGeoFeatureLayer pGeoFeatureLayer = default(IGeoFeatureLayer);
            pGeoFeatureLayer = (IGeoFeatureLayer)pLayer;
            if (pGeoFeatureLayer == null)
                return null;
            ISymbolArray pMySymbolArray = default(ISymbolArray);

            IArray pSymbolArray = default(IArray);
            pSymbolArray = new ESRI.ArcGIS.esriSystem.Array();
            Debug.Assert((pSymbolArray != null));
            if (pSymbolArray == null)
            {
                return null;
            }

            //简单渲染图层
            if (pGeoFeatureLayer.Renderer is ISimpleRenderer)
            {
                ISimpleRenderer pSimpleRender = default(ISimpleRenderer);
                pSimpleRender = (ISimpleRenderer)pGeoFeatureLayer.Renderer;
                pSymbolArray.Add(pSimpleRender.Symbol);
            }

            //单值渲染图层
            if (pGeoFeatureLayer.Renderer is IUniqueValueRenderer)
            {
                IUniqueValueRenderer pUniqueValueRenderer = default(IUniqueValueRenderer);
                pUniqueValueRenderer = (IUniqueValueRenderer)pGeoFeatureLayer.Renderer;
                ISymbol pSymbol = default(ISymbol);
                pSymbol = pUniqueValueRenderer.get_Symbol(Convert.ToString(pUniqueValueRenderer.get_Value(0)));
                pSymbolArray.Add(pSymbol);
            }

            //分类渲染图层
            if (pGeoFeatureLayer.Renderer is IClassBreaksRenderer)
            {
                IClassBreaksRenderer pClassRenderer = default(IClassBreaksRenderer);
                pClassRenderer = (IClassBreaksRenderer)pGeoFeatureLayer.Renderer;

                for (i = 0; i <= pClassRenderer.BreakCount - 1; i++)
                {
                    pSymbolArray.Add(pClassRenderer.get_Symbol(i));
                }
            }

            //图表渲染图层
            if (pGeoFeatureLayer.Renderer is IChartRenderer)
            {
                IChartRenderer pChartRenderer = default(IChartRenderer);
                pChartRenderer = (IChartRenderer)pGeoFeatureLayer.Renderer;
                IChartSymbol pChartSymbol = default(IChartSymbol);
                pChartSymbol = pChartRenderer.ChartSymbol;

                pMySymbolArray = (ISymbolArray)pChartSymbol;
                Debug.Assert((pMySymbolArray != null));
                if (pMySymbolArray == null)
                    return null;
                for (i = 0; i <= pMySymbolArray.SymbolCount - 1; i++)
                {
                    pSymbolArray.Add(pMySymbolArray.get_Symbol(i));
                }

                Debug.Assert(!(pSymbolArray.Count < 1));
            }
            //点密度渲染图层
            if (pGeoFeatureLayer.Renderer is IDotDensityRenderer)
            {
                IDotDensityRenderer pDotDensityRenderer = default(IDotDensityRenderer);
                pDotDensityRenderer = (IDotDensityRenderer)pGeoFeatureLayer.Renderer;
                IDotDensityFillSymbol pDotDensityFillSymbol = default(IDotDensityFillSymbol);
                pDotDensityFillSymbol = pDotDensityRenderer.DotDensitySymbol;

                pMySymbolArray = (ISymbolArray)pDotDensityFillSymbol;

                for (i = 0; i <= pMySymbolArray.SymbolCount - 1; i++)
                {
                    pSymbolArray.Add(pMySymbolArray.get_Symbol(i));
                }
            }

            if (!(pSymbolArray.Count < 1))
            {
                functionReturnValue = pSymbolArray;
            }
            pSymbolArray = null;
            return functionReturnValue;
        }

        #endregion "得到当前图层的渲染符号"

        #region "判断图层类型"

        public string GetLayerShapeType(IGeoFeatureLayer pGeoFeatureLayer)
        {
            string functionReturnValue = null;
            functionReturnValue = "";
            IFeatureClass pFeatureClass = default(IFeatureClass);
            esriGeometryType intShapeType = default(esriGeometryType);

            if (pGeoFeatureLayer == null)
            {
                return "";
            }

            pFeatureClass = pGeoFeatureLayer.FeatureClass;

            if (pFeatureClass == null)
            {
                return "";
            }
            intShapeType = pFeatureClass.ShapeType;
            //面符号
            if (intShapeType == esriGeometryType.esriGeometryPolygon | intShapeType == esriGeometryType.esriGeometryEnvelope)
            {
                functionReturnValue = "Fill Symbols";
            }

            if (intShapeType == esriGeometryType.esriGeometryPolyline | intShapeType == esriGeometryType.esriGeometryLine)
            {
                functionReturnValue = "Line Symbols";
            }
            if (intShapeType == esriGeometryType.esriGeometryPoint)
            {
                functionReturnValue = "Marker Symbols";
            }
            return functionReturnValue;
        }

        #endregion "判断图层类型"

        #region "根据图层类型、字段数目、当前字段渲染颜色创建饼状符号 " '有待修改

        private void CreatePieSymbolsWithColor()
        {
            int i = 0;
            m_FieldsCount = this.dataGridView1.Rows.Count;
            //当前渲染字段的个数
            if (m_FieldsCount == 0)
                return;
            m_pSymbolsArray.RemoveAll();
            //清空符号数组
            IColor pColor = default(IColor);
            //定义获取字段渲染颜色的对象
            Debug.Assert(!string.IsNullOrEmpty(m_strShapeType));
            if (string.IsNullOrEmpty(m_strShapeType))
                return;
            ISimpleFillSymbol pFillSymbol = default(ISimpleFillSymbol);

            for (i = 0; i <= Convert.ToInt32(m_FieldsCount - 1); i++)
            {
                byte rC = dataGridView1.Rows[i].Cells[0].Style.BackColor.R;
                byte gC = dataGridView1.Rows[i].Cells[0].Style.BackColor.G;
                byte bC = dataGridView1.Rows[i].Cells[0].Style.BackColor.B;
                pColor = GetRGBColor(rC, gC, bC);
                pFillSymbol = new SimpleFillSymbol();
                Debug.Assert((pFillSymbol != null));
                if (pFillSymbol == null)
                    return;
                pFillSymbol.Color = pColor;
                pFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
                m_pSymbolsArray.Add(pFillSymbol);
                pFillSymbol = null;
            }
        }

        #endregion "根据图层类型、字段数目、当前字段渲染颜色创建饼状符号 " '有待修改

        #region "查找要素图层"

        public IFeatureLayer GetFeatureLayer(string slayer, IMap m_pMap)
        {
            IFeatureLayer functionReturnValue = default(IFeatureLayer);
            functionReturnValue = null;
            IEnumLayer pLayers = default(IEnumLayer);
            //枚举图层接口
            ILayer pLayer = default(ILayer);
            //图层接口
            try
            {
                if (m_pMap.LayerCount == 0)
                    return null;

                pLayers = m_pMap.get_Layers(null, true);
                pLayer = pLayers.Next();

                while ((pLayer != null))
                {
                    //找到要素图层
                    if (pLayer is IFeatureLayer & slayer.ToUpper() == pLayer.Name.ToUpper())
                    {
                        functionReturnValue = (IFeatureLayer)pLayer;
                        break;
                    }
                    pLayer = pLayers.Next();
                }
                pLayer = null;
                pLayers = null;
            }
            catch (Exception ex)
            {
                functionReturnValue = null;
                MessageBox.Show(ex.ToString());
            }
            return functionReturnValue;
        }

        #endregion "查找要素图层"

        private IColor GetRGBColor(byte r, byte g, byte b)
        {
            RgbColor RGB = new RgbColor();
            RGB.Red = r;
            RGB.Green = g;
            RGB.Blue = b;
            return RGB;
        }

        #region "DataGridView样式结构体"

        public struct GridViewCellStyle
        {
            public DataGridViewCellStyle cellStyle;
        }

        #endregion "DataGridView样式结构体"

        #region "获取选中图层的字段"

        public IArray GetLayerFields(IMap m_pMap, string lyrname, IFeatureLayer pfeaturelayer)
        {
            IArray functionReturnValue = default(IArray);

            functionReturnValue = null;
            IArray pAryLayerField = default(IArray);
            //图层字段队列接口
            IGeoFeatureLayer pFeaturelyr = default(IGeoFeatureLayer);
            IFeatureClass pFeatureClass = default(IFeatureClass);
            //要素类接口
            IFields pFields = default(IFields);
            //字段集合接口
            IField pField = default(IField);
            //字段接口
            int intFieldIndex = 0;
            //字段索引号

            try
            {
                pAryLayerField = new ESRI.ArcGIS.esriSystem.Array();
                pFeaturelyr = (IGeoFeatureLayer)pfeaturelayer;

                //传递参数pFeatureLayer为空，则调用GetFeatureLayer函数，找到要素图层
                if (pFeaturelyr == null)
                {
                    pFeaturelyr = (IGeoFeatureLayer)GetFeatureLayer(lyrname, m_pMap);
                }
                //找到的要素图层为空，则退出函数
                if (pFeaturelyr == null)
                {
                    pAryLayerField = null;
                    return null;
                }
                else
                {
                    pFeatureClass = pFeaturelyr.DisplayFeatureClass;

                    if (pFeatureClass == null)
                        return null;

                    pFields = pFeatureClass.Fields;

                    for (intFieldIndex = 0; intFieldIndex <= (pFields.FieldCount - 1); intFieldIndex++)
                    {
                        pField = pFields.get_Field(intFieldIndex);

                        if (pField.Name.ToUpper() != "SHAPE" & pField.Name.ToUpper() != "SHAPE.LEN")
                        {
                            pAryLayerField.Add(pField);
                        }
                    }
                    pFeatureClass = null;
                    pFields = null;
                    pField = null;
                }

                functionReturnValue = pAryLayerField;
                pAryLayerField = null;
            }
            catch (Exception ex)
            {
                functionReturnValue = null;
                MessageBox.Show(ex.ToString());
            }
            return functionReturnValue;
        }

        #endregion "获取选中图层的字段"

        private void FrmThemeBar_Load(object sender, EventArgs e)
        {
            //实例化变量
            InitVariables();
            m_pDataTable.Columns.Add(m_symbolCol);
            m_pDataTable.Columns.Add(m_valueCol);
            dataGridView1.DataSource = m_pDataTable;
            dataGridView1.AllowUserToAddRows = false;
            btnAllRemove.Enabled = false;

            //得到 m_pMap 中的所有图层
            m_colMapLayers = new ArrayList();
            if (m_colMapLayers == null)
            {
                return;
            }
            if ((m_pMap != null))
            {
                m_colMapLayers = GetMapFeatLayers(m_pMap);
            }
            else
            {
                MessageBox.Show("图层没有参数!");
                return;
            }

            if (m_colMapLayers == null)
            {
                MessageBox.Show("没有图层!");
                return;
            }

            int i = 0;
            for (i = 0; i < m_colMapLayers.Count; i++)
            {
                ListBoxLayers.Items.Add(m_colMapLayers[i]);
            }

            ListBoxLayers.SelectedIndex = 0;
            m_colMapLayers = null;
        }

        private void PanelBackGroudColor_Click(object sender, EventArgs e)
        {
            ColorDialog1.AllowFullOpen = true;
            ColorDialog1.FullOpen = true;
            ColorDialog1.ShowHelp = true;
            ColorDialog1.Color = Color.Black;
            ColorDialog1.ShowDialog();
            PanelBackGroudColor.BackColor = ColorDialog1.Color;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            int i = 0;
            CreateDefaultSymbols();
            //生成背景符号
            LoadDefaultSymbols();
            //加载背景符号符号
            m_pLayer = GetFeatureLayer(ListBoxLayers.SelectedItem.ToString(), m_pMap);
            m_pGeoFeatureLayer = (IGeoFeatureLayer)GetFeatureLayer(ListBoxLayers.SelectedItem.ToString(), m_pMap);
            m_FieldsCount = this.dataGridView1.Rows.Count;

            if (m_FieldsCount < 2)
                return;
            //创建饼状符号
            CreatePieSymbolsWithColor();
            //创建饼状图标渲染对象
            IChartRenderer pChartRenderer = default(IChartRenderer);
            pChartRenderer = new ChartRenderer();
            Debug.Assert((pChartRenderer != null));
            if (pChartRenderer == null)
                return;
            IRendererFields pRendererFields = default(IRendererFields);
            pRendererFields = (IRendererFields)pChartRenderer;
            //向渲染字段对象中添加选中的字段
            for (i = 0; i <= m_FieldsCount - 1; i++)
            {
                pRendererFields.AddField(dataGridView1.Rows[i].Cells[1].Value.ToString(), dataGridView1.Rows[i].Cells[1].Value.ToString());
            }

            IBarChartSymbol pBarChartSymbol = default(IBarChartSymbol);
            pBarChartSymbol = new BarChartSymbolClass();
            pBarChartSymbol.Width = Convert.ToDouble(numUpDownWidth.Value);

            IChartSymbol pChartSymbol = default(IChartSymbol);
            pChartSymbol = (IChartSymbol)pBarChartSymbol;

            //获取图层上的feature
            IDisplayTable pDisplayTable = default(IDisplayTable);
            pDisplayTable = (IDisplayTable)m_pLayer;

            //通过IDataStatistics计算出所有渲染字段的最大值，作为设置饼状图的比例大小的依据
            double maxValue = 0;
            double minValue = 0;
            bool firstValue = true;
            for (i = 0; i <= Convert.ToInt32(m_FieldsCount) - 1; i++)
            {
                ICursor pCursor = pDisplayTable.SearchDisplayTable(null, false);
                IDataStatistics pDataStaticstics = new DataStatistics();
                pDataStaticstics.Cursor = pCursor;
                pDataStaticstics.Field = pRendererFields.get_Field(i);
                m_pStatisticsResults = pDataStaticstics.Statistics;
                double value1 = Convert.ToDouble(m_pStatisticsResults.Minimum);
                double value2 = Convert.ToDouble(m_pStatisticsResults.Maximum);
                if ((firstValue))
                {
                    maxValue = value2;
                    firstValue = false;
                }
                else if ((value2 > maxValue))
                {
                    maxValue = value2;
                }
                if ((firstValue))
                {
                    minValue = value1;
                    firstValue = false;
                }
                else if ((value1 < maxValue))
                {
                    minValue = value1;
                }
            }

            if ((maxValue <= 0))
            {
                MessageBox.Show("最小值是零或小于零0.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //设置背景,字段的最大值和最小值
            pChartRenderer.BaseSymbol = (ISymbol)m_pDefaultFillSymbol;
            pChartRenderer.UseOverposter = false;
            pChartSymbol.MaxValue = maxValue;
            //设置符号
            IMarkerSymbol pMarkerSymbol = default(IMarkerSymbol);
            pMarkerSymbol = (IMarkerSymbol)pBarChartSymbol;
            pMarkerSymbol.Size = Convert.ToDouble(numUpDownHeight.Value);
            ISymbolArray pSymbolArray = default(ISymbolArray);
            //定义符号数组
            pSymbolArray = (ISymbolArray)pBarChartSymbol;

            for (i = 0; i <= Convert.ToInt32(m_FieldsCount) - 1; i++)
            {
                pSymbolArray.AddSymbol((ISymbol)m_pSymbolsArray.get_Element(i));
            }

            pChartRenderer.ChartSymbol = pChartSymbol;
            pChartRenderer.CreateLegend();
            m_pGeoFeatureLayer.Renderer = (IFeatureRenderer)pChartRenderer;
            _frmMain.pCurrentMap.Refresh();
            _frmMain.pCurrentTOC.Update();
            pChartRenderer = null;
            this.btnOk.Enabled = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (btnApply.Enabled == true)
            {
                btnApply_Click(null, null);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                m_pGeoFeatureLayer.Renderer = this.m_pFeatureRenderer;
                _frmMain.pCurrentMap.Refresh(esriViewDrawPhase.esriViewGeography, null, null);
                _frmMain.pCurrentTOC.Update();
                this.Close();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void btnSingleAdd_Click(object sender, EventArgs e)
        {
            if ((listBoxField.SelectedIndex == -1))
            {
                MessageBox.Show("请选择要添加的字段！", "提示");
                return;
            }

            btnAllRemove.Enabled = true;
            DataRow pDatarow = null;
            dataGridView1.AllowUserToAddRows = true;

            pDatarow = m_pDataTable.NewRow();
            pDatarow[1] = listBoxField.SelectedItem.ToString();
            listBoxField.Items.RemoveAt(listBoxField.SelectedIndex);
            m_pDataTable.Rows.Add(pDatarow);
            dataGridView1.DataSource = m_pDataTable;

            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            Random numRandom = new Random();
            int r = Convert.ToInt32(Math.Floor(255 * numRandom.NextDouble()));
            int g = Convert.ToInt32(Math.Floor(255 * numRandom.NextDouble()));
            int b = Convert.ToInt32(Math.Floor(255 * numRandom.NextDouble()));
            cellStyle.BackColor = Color.FromArgb(r, g, b);

            if ((dataGridView1.Rows.Count >= 1))
            {
                dataGridView1.Rows[m_pDataTable.Rows.Count - 1].Cells[0].Style = cellStyle;
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[1];
            }
            dataGridView1.AllowUserToAddRows = false;
            this.btnApply.Enabled = true;
        }

        private void btnSingleRemove_Click(object sender, EventArgs e)
        {
            if ((dataGridView1.SelectedCells.Count == 0))
            {
                MessageBox.Show("请选择要移除的字段！", "提示");
                return;
            }
            listBoxField.Items.Add(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString());
            dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
            if ((this.dataGridView1.Rows.Count == 0))
            {
                this.btnApply.Enabled = false;
            }
        }

        private void btnAllAdd_Click(object sender, EventArgs e)
        {
            btnAllRemove.Enabled = true;
            DataRow pDatarow = null;
            dataGridView1.AllowUserToAddRows = true;
            int i = 0;

            GridViewCellStyle[] cellStyle = new GridViewCellStyle[listBoxField.Items.Count];
            Random numRandom = new Random();
            int r = 0;
            int g = 0;
            int b = 0;
            for (i = 0; i <= listBoxField.Items.Count - 1; i++)
            {
                pDatarow = m_pDataTable.NewRow();
                pDatarow[1] = listBoxField.Items[i].ToString();
                m_pDataTable.Rows.Add(pDatarow);
                r = Convert.ToInt32(Math.Floor(255 * numRandom.NextDouble()));
                g = Convert.ToInt32(Math.Floor(255 * numRandom.NextDouble()));
                b = Convert.ToInt32(Math.Floor(255 * numRandom.NextDouble()));
                cellStyle[i].cellStyle = new DataGridViewCellStyle();
                cellStyle[i].cellStyle.BackColor = Color.FromArgb(r, g, b);
                dataGridView1.Rows[i].Cells[0].Style = cellStyle[i].cellStyle;
                dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[1];
            }

            dataGridView1.DataSource = m_pDataTable;
            dataGridView1.AllowUserToAddRows = false;
            listBoxField.Items.Clear();
            this.btnApply.Enabled = true;
        }

        private void btnAllRemove_Click(object sender, EventArgs e)
        {
            int i = 0;
            btnAllRemove.Enabled = false;
            btnAllAdd.Enabled = true;
            listBoxField.Items.Clear();
            if ((dataGridView1.Rows.Count <= 0))
            {
                MessageBox.Show("无可移植的字段！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            for (i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            {
                listBoxField.Items.Add(dataGridView1.Rows[i].Cells[1].Value.ToString());
            }
            m_pDataTable.Clear();
            dataGridView1.DataSource = m_pDataTable;
            this.btnApply.Enabled = false;
        }

        private void ListBoxLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_colValidFeildsArray = null;
            m_colValidFeildsArray = new ArrayList();

            if (m_colValidFeildsArray == null)
            {
                return;
            }
            //开始初始化窗体界面（根据图层）
            m_bIsInitializing = true;

            //得到选中的图层
            if (ListBoxLayers.SelectedIndex == -1)
                return;

            m_pGeoFeatureLayer = (IGeoFeatureLayer)GetFeatureLayer(ListBoxLayers.SelectedItem.ToString(), m_pMap);
            m_pFeatureRenderer = m_pGeoFeatureLayer.Renderer;
            Debug.Assert((m_pGeoFeatureLayer != null));

            if (m_pGeoFeatureLayer == null)
                return;

            if (m_pGeoFeatureLayer.FeatureClass == null)
            {
                return;
            }

            //确定图层类型
            m_strShapeType = GetLayerShapeType(m_pGeoFeatureLayer);
            if (string.IsNullOrEmpty(m_strShapeType))
            {
                return;
            }

            if (string.IsNullOrEmpty(ListBoxLayers.SelectedItem.ToString()))
            {
                return;
            }

            m_pLayerFieldsArray = GetLayerFields(m_pMap, ListBoxLayers.SelectedItem.ToString(), null);

            if (m_pLayerFieldsArray == null)
            {
                return;
            }

            this.listBoxField.Items.Clear();
            int i = 0;
            IField pField = default(IField);

            //过滤字段

            for (i = 0; i <= m_pLayerFieldsArray.Count - 1; i++)
            {
                pField = (IField)m_pLayerFieldsArray.get_Element(i);

                if ((!(pField.Name == "FID")) & (!(pField.Name == "ID")) & (pField.VarType == 2 | pField.VarType == 3 | pField.VarType == 4 | pField.VarType == 5))
                {
                    ITable pTable = default(ITable);
                    ITableHistogram pTableHistogram = default(ITableHistogram);
                    IBasicHistogram pHistogram = default(IBasicHistogram);
                    object vntDataFrequency = null;
                    object vntDataValues = null;

                    //统计图层中每个字段的数据
                    if (m_pGeoFeatureLayer == null)
                        return;

                    pTable = (ITable)m_pGeoFeatureLayer;
                    pTableHistogram = new BasicTableHistogram() as ITableHistogram;
                    Debug.Assert((pTableHistogram != null));
                    if (pTableHistogram == null)
                        return;
                    pHistogram = (IBasicHistogram)pTableHistogram;

                    pTableHistogram.Field = pField.Name;
                    pTableHistogram.Table = pTable;
                    pHistogram.GetHistogram(out vntDataValues, out vntDataFrequency);
                    string strDataType = null;
                    strDataType = Information.TypeName(vntDataValues);
                    Debug.Assert(strDataType == "Integer()" | strDataType == "Long()" | strDataType == "Double()");
                    //该字段符合分类渲染的要求，加入之
                    m_colValidFeildsArray.Add(pField.Name);
                    pTableHistogram = null;
                }
            }

            //初始化字段下拉框
            for (i = 0; i < m_colValidFeildsArray.Count; i++)
            {
                listBoxField.Items.Add(m_colValidFeildsArray[i].ToString());
            }
            //初始化界窗体完毕
            m_bIsInitializing = false;
        }

        private void FrmThemeBar_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmThemeBar.TmpFrmThemeBar = null;
        }

        private void FrmThemeBar_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }
    }
}