using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections;
using System.Diagnostics;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
//using ESRI.ArcGIS.SpatialAnalyst;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
//using ESRI.ArcGIS.GeoAnalyst;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.esriSystem;
using Microsoft.VisualBasic;


namespace GISDemo
{
    public partial class FrmThemeDot : Form
    {
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
        //当前图层中适合进行点密度渲染的字段集合
        private ArrayList m_colValidFeildsArray = new ArrayList();
        //当前渲染图层的属性表对象
        private IDisplayTable m_pDisplayTable;
        //当前渲染图层的要素对象
        private IFeatureClass m_pFeatureClass;
        //字段数组
        private IArray m_pLayerFieldsArray;
        //渲染符号集合
        private ISymbolArray m_pSymbolsArray;


        //背景的面符号
        private ISimpleFillSymbol m_pDefaultFillSymbol;
        //背景的线符号
        private ISimpleLineSymbol m_pDefaultLineSymbol;
        //背景的点符号
        private ISimpleMarkerSymbol m_pDefaultMarkerSymbol;

        //定义点密度图渲染组件
        private IDotDensityRenderer m_pDotDensityRenderer;
        //定义点密度图渲染组件对象的渲染字段对象
        private IRendererFields m_pRendererField;
        //定义点密度图渲染得符号对象
        private IDotDensityFillSymbol m_pDotDensityFillSymbol;
        //根据图层初始化窗体界面参数
        private bool m_bIsInitializing;

        //定义选中字段显示的表格对象
        DataTable m_pDataTable = new DataTable();
        //图层原始渲染对像
        private IFeatureRenderer m_pFeatureRenderer;
        #endregion

        #region "'初始化变量"
        private void InitVariables()
        {
            m_colMapLayers = null;

            m_pSymbolsArray = null;
            m_pGeoFeatureLayer = null;
            m_pDefaultFillSymbol = null;
            m_pDefaultLineSymbol = null;
            m_pDefaultMarkerSymbol = null;


            m_bIsInitializing = false;

            m_pDotDensityRenderer = new DotDensityRenderer() as IDotDensityRenderer;
            m_pRendererField = null;
            m_pDotDensityFillSymbol = new DotDensityFillSymbol();


            if (m_pMap == null)
            {
                return;
            }
        }
        #endregion

        #region "窗体需要设置的参数之一（可选）"
        public IMap Map
        {
            get { return m_pMap; }
            set { m_pMap = value; }
        }
        #endregion

        #region "得到地图中的featurelayer的名字集合"
        //得到地图中的featurelayer名字集合
        //时间：2009.4.26
        //源人：rxp
        //更新：2009.4.26
        public ArrayList GetMapFeatLayers(IMap m_pMap)
        {
            ArrayList functionReturnValue = null;

            functionReturnValue = null;
            try
            {
                ArrayList pColFeatureLayers = null;
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
                pEnumLayer = m_pMap.get_Layers(null,true);
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
                //return;
            }
            catch (Exception ex)
            {
                functionReturnValue = null;
                MessageBox.Show(ex.ToString());
                //return;
            }
            return functionReturnValue;
        }
        #endregion

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

            //线符号
            ISimpleLineSymbol pLineSymbol = default(ISimpleLineSymbol);
            pLineSymbol = new SimpleLineSymbol();
            if (pLineSymbol == null)
                return;

            pRgbColor = new RgbColor();



            {
                pRgbColor.Red = PanelBackGroudColor.BackColor.R;
                pRgbColor.Green = PanelBackGroudColor.BackColor.G;
                pRgbColor.Blue = PanelBackGroudColor.BackColor.B;
                pRgbColor.UseWindowsDithering = true;
            }

            pLineSymbol.Color = pRgbColor;

            m_pDefaultLineSymbol = pLineSymbol;
            pLineSymbol = null;
            pRgbColor = null;

            //点符号
            ISimpleMarkerSymbol pMarkerSymbol = default(ISimpleMarkerSymbol);
            pMarkerSymbol = new SimpleMarkerSymbol();



            pRgbColor = new RgbColor();
            if (pRgbColor == null)
            {
                return;
            }

            {
                pRgbColor.Red = PanelBackGroudColor.BackColor.R;
                pRgbColor.Green = PanelBackGroudColor.BackColor.G;
                pRgbColor.Blue = PanelBackGroudColor.BackColor.B;
                pRgbColor.UseWindowsDithering = true;
            }

            pMarkerSymbol.Color = pRgbColor;
            pMarkerSymbol.Size = 4;

            m_pDefaultMarkerSymbol = pMarkerSymbol;
            pMarkerSymbol = null;
            pRgbColor = null;
        }
        #endregion

        #region "加载背景符号"

        private void LoadDefaultSymbols()
        {
            Debug.Assert(!string.IsNullOrEmpty(m_strShapeType));
            if (string.IsNullOrEmpty(m_strShapeType))
                return;



            //面符号
            //当前选中的图层类型为面状
            if (m_strShapeType == "Fill Symbols")
            {

                Debug.Assert((m_pDefaultMarkerSymbol != null));
                if (m_pDefaultMarkerSymbol == null)
                    return;

                // Dim bResult As Boolean



                Debug.Assert((m_pDefaultFillSymbol != null));
                if (m_pDefaultFillSymbol == null)
                    return;


            }

            //'线符号

            if (m_strShapeType == "Line Symbols")
            {
                Debug.Assert((m_pDefaultLineSymbol != null));
                if (m_pDefaultLineSymbol == null)
                    return;

            }

            //'点符号

            if (m_strShapeType == "Marker Symbols")
            {
                Debug.Assert((m_pDefaultMarkerSymbol != null));
                if (m_pDefaultMarkerSymbol == null)
                    return;
            }

        }
        #endregion

        #region "判断图层类型"
        public string GetLayerShapeType(IGeoFeatureLayer pGeoFeatureLayer)
        {
            string functionReturnValue = null;
            functionReturnValue = "";
            IFeatureClass pFeatureClass = default(IFeatureClass);
            esriGeometryType intShapeType = default(esriGeometryType);

            if (pGeoFeatureLayer == null)
                return "";

            pFeatureClass = pGeoFeatureLayer.FeatureClass;

            if (pFeatureClass == null)
            {
                return "";
                //return;
            }



            intShapeType = pFeatureClass.ShapeType;

            //面符号
            if (intShapeType == esriGeometryType.esriGeometryPolygon | intShapeType == esriGeometryType.esriGeometryEnvelope)
            {
                functionReturnValue = "Fill Symbols";

            }

            //线符号
            if (intShapeType == esriGeometryType.esriGeometryPolyline | intShapeType == esriGeometryType.esriGeometryLine)
            {
                functionReturnValue = "Line Symbols";

            }

            //点符号
            if (intShapeType == esriGeometryType.esriGeometryPoint)
            {
                functionReturnValue = "Marker Symbols";

            }

            //错误
            return functionReturnValue;
            //return functionReturnValue;

        }
        #endregion

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

                pLayers = m_pMap.get_Layers(null,true);
                pLayer = pLayers.Next();


                while ((pLayer != null))
                {
                    //找到要素图层
                    if (pLayer is IFeatureLayer & slayer.ToUpper() == pLayer.Name.ToUpper())
                    {
                        functionReturnValue = (IFeatureLayer)pLayer;
                        //return;
                    }

                    pLayer = pLayers.Next();
                }

                pLayer = null;
                pLayers = null;

                //return;
            }
            catch (Exception ex)
            {
                functionReturnValue = null;
                MessageBox.Show(ex.ToString());
            }
            return functionReturnValue;

        }
        #endregion

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
                //return;
            }
            catch (Exception ex)
            {
                functionReturnValue = null;
                MessageBox.Show(ex.ToString());
            }
            return functionReturnValue;
        }
        #endregion

        #region "获得渲染字段的属性值"

        private void GetFieldValues()
        {
            m_pDataTable.Clear();
            m_pDataTable.Columns.Remove(m_pDataTable.Columns[1]);

            DataColumn m_valueCol = new DataColumn(cmbFields.Items[cmbFields.SelectedIndex].ToString().Trim());

            m_pDataTable.Columns.Add(m_valueCol);

            // Dim m_pFeatureCursor As IFeatureCursor              '要素查找对象 
            m_pLayer = GetFeatureLayer(ListBoxLayers.SelectedItem.ToString(), m_pMap);

            m_pDisplayTable =(IDisplayTable) m_pLayer;
            m_pFeatureClass = (IFeatureClass)m_pDisplayTable.DisplayTable;
            m_pDisplayTable = (IDisplayTable)m_pLayer;
            m_pFeatureClass = (IFeatureClass)m_pDisplayTable.DisplayTable;

            IFields pFields = m_pFeatureClass.Fields;
            IFeatureCursor pFeatureCursor = default(IFeatureCursor);
            IQueryFilter pQueryFilter = default(IQueryFilter);
            //查询过滤器
            pQueryFilter = new QueryFilter();
            //创建一个新的查询过滤器
            pQueryFilter.WhereClause = "";
            pFeatureCursor = m_pFeatureClass.Search(pQueryFilter, false);

            IFeature pFeature = default(IFeature);
            pFeature = pFeatureCursor.NextFeature();


            while (((pFeature != null)))
            {
                Int32 j = default(Int32);
                int i = 0;
                string fldValue = null;
                DataRow dr = m_pDataTable.NewRow();


                for (j = 0; j <= pFields.FieldCount - 1; j++)
                {
                    string fldName = null;
                    fldName = pFields.get_Field(j).AliasName;

                    if (fldName == "FID")
                    {
                        i = 0;
                        fldValue = pFeature.get_Value(j).ToString();
                        dr[i] = fldValue;
                    }
                    else if (fldName == cmbFields.Items[cmbFields.SelectedIndex].ToString().Trim())
                    {
                        i = 1;
                        fldValue = pFeature.get_Value(j).ToString();
                        dr[i] = fldValue;
                    }
                }

                m_pDataTable.Rows.Add(dr);

                pFeature = pFeatureCursor.NextFeature();

            }
            m_pDataTable.AcceptChanges();
            DataGridView1.DataSource = m_pDataTable;
            DataGridView1.Refresh();
            DataGridView1.AllowUserToAddRows = false;

        }
        #endregion

        private IColor GetRGBColor(byte r, byte g, byte b)
        {
            RgbColor RGB = new RgbColor();
            RGB.Red = r;
            RGB.Green = g;
            RGB.Blue = b;
            return RGB;
        }

        FrmGISMain _frmMain;

        #region 控制只打开一个界面
        public static FrmThemeDot TmpFrmThemeDot = null;
        #endregion

        public FrmThemeDot()
        {
            InitializeComponent();
        }

        public FrmThemeDot(FrmGISMain TmpFrom)
        {
            InitializeComponent();
            _frmMain = TmpFrom;
            this.Map = _frmMain.pCurrentMap.Map;
        }

        private void FrmThemeDot_Load(object sender, EventArgs e)
        {
            DataColumn m_symbolCol = new DataColumn("FID");
            DataColumn m_valueCol = new DataColumn("属性值");
            m_pDataTable.Columns.Add(m_symbolCol);
            m_pDataTable.Columns.Add(m_valueCol);


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
                MessageBox.Show("图层参数没有!");
                //Interaction.MsgBox("图层参数没有!");
                return;
            }

            if (m_colMapLayers == null)
            {
                MessageBox.Show("没有图层!");
                //Interaction.MsgBox("没有图层!");
                return;
            }

            int i = 0;
            for (i = 0; i < m_colMapLayers.Count; i++)
            {
                ListBoxLayers.Items.Add(m_colMapLayers[i]);
            }
            if ((ListBoxLayers.Items.Count > 0))
            {
                ListBoxLayers.SelectedIndex = 0;
            }

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

        private void PanelDotColor_Click(object sender, EventArgs e)
        {
            ColorDialog1.AllowFullOpen = true;
            ColorDialog1.FullOpen = true;
            ColorDialog1.ShowHelp = true;
            ColorDialog1.Color = Color.Black;
            ColorDialog1.ShowDialog();
            PanelDotColor.BackColor = ColorDialog1.Color;
        }

        private void ListBoxLayers_DoubleClick(object sender, EventArgs e)
        {

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //  Dim i As Integer
            //实例化变量
            InitVariables();

            CreateDefaultSymbols();
            //生成背景符号

            LoadDefaultSymbols();
            //加载背景符号符号


            m_pGeoFeatureLayer =(IGeoFeatureLayer) GetFeatureLayer(ListBoxLayers.SelectedItem.ToString(), m_pMap);


            m_pRendererField =(IRendererFields) m_pDotDensityRenderer;


            m_pRendererField.AddField(cmbFields.Items[cmbFields.SelectedIndex].ToString().Trim(), cmbFields.Items[cmbFields.SelectedIndex].ToString().Trim());

            //设置符号的颜色
            IRgbColor SymbolColor = new RgbColor();
            byte dR = PanelDotColor.BackColor.R;
            byte dB = PanelDotColor.BackColor.G;
            byte dG = PanelDotColor.BackColor.B;
            SymbolColor =(IRgbColor) GetRGBColor(dR, dB, dG);

            //设置背景颜色
            IRgbColor BackColor = new RgbColor();
            byte bR = PanelBackGroudColor.BackColor.R;
            byte bB = PanelBackGroudColor.BackColor.G;
            byte bG = PanelBackGroudColor.BackColor.B;
            BackColor = (IRgbColor)GetRGBColor(bR, bB, bG);
            m_pDotDensityFillSymbol.BackgroundColor = BackColor;

            m_pDotDensityFillSymbol.FixedPlacement = true;
            //什么作用？
            //设置轮廓线
            ILineSymbol pLineSymbol = new CartographicLineSymbol();
            m_pDotDensityFillSymbol.Outline = pLineSymbol;

            //添加点密度图渲染的点符号到符号数组中去
            ISimpleMarkerSymbol pMarkerSymbol = new SimpleMarkerSymbol();
            pMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;

            pMarkerSymbol.Size = (double)UpDownDotSize.Value;
            pMarkerSymbol.Color = SymbolColor;

            m_pSymbolsArray = (ISymbolArray)m_pDotDensityFillSymbol;
            m_pSymbolsArray.AddSymbol((ISymbol)pMarkerSymbol);


            //设置点密度图渲染的点符号
            m_pDotDensityRenderer.DotDensitySymbol =(IDotDensityFillSymbol) m_pSymbolsArray;
            //m_pDotDensityRenderer.DotDensitySymbol = m_pDotDensityFillSymbol
            //确定一个点代表多少值
            m_pDotDensityRenderer.DotValue = (double)UpDownDotValue.Value;

            m_pDotDensityRenderer.ColorScheme = "Custom";
            m_pDotDensityRenderer.MaintainSize = true;
            m_pDotDensityRenderer.CreateLegend();


            //将饼状图渲染对象与渲染图层挂钩
            m_pGeoFeatureLayer.Renderer = (IFeatureRenderer)m_pDotDensityRenderer;
            //点密度渲染(TOOCotrol
            _frmMain.pCurrentMap.Refresh();
            _frmMain.pCurrentTOC.Update();
            //FrmMain.m_mapControl.Refresh();
            //FrmMain.m_pTocControl.Update();
            m_pDotDensityRenderer = null;
            btnApply.Enabled = true;
        }

        private void cmbFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetFieldValues();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (btnApply.Enabled == true)
            {
                btnApply_Click(null, null);
            }
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                m_pGeoFeatureLayer.Renderer = this.m_pFeatureRenderer;

                _frmMain.pCurrentMap.Refresh(esriViewDrawPhase.esriViewGeography,null,null);
                _frmMain.pCurrentTOC.Update();
                //FrmMain.m_mapControl.Refresh(esriViewDrawPhase.esriViewGeography);
                //FrmMain.m_pTocControl.Update();
                this.Close();
            }
            catch (Exception ex)
            {
                return;
            }

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

            m_pGeoFeatureLayer =(IGeoFeatureLayer) GetFeatureLayer(ListBoxLayers.SelectedItem.ToString(), m_pMap);
            this.m_pFeatureRenderer = m_pGeoFeatureLayer.Renderer;
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

            cmbFields.Items.Clear();
            int i = 0;
            IField pField = default(IField);

            //过滤字段

            for (i = 0; i <= m_pLayerFieldsArray.Count - 1; i++)
            {
                pField = (IField)m_pLayerFieldsArray.get_Element(i);
                if ((!(pField.Name == "FID")) & (!(pField.Name == "ID")) & (pField.VarType == 2 | pField.VarType == 3 | pField.VarType == 4 | pField.VarType == 5))
                {
                    ITable pTable = default(ITable);
                    //Dim pClassify As IClassify
                    ITableHistogram pTableHistogram = default(ITableHistogram);
                    IBasicHistogram pHistogram = default(IBasicHistogram);
                    object vntDataFrequency = null;
                    object vntDataValues = null;

                    //统计图层中每个字段的数据
                    if (m_pGeoFeatureLayer == null)
                        return;

                    pTable = (ITable)m_pGeoFeatureLayer;
                    //pTableHistogram = new BasicTableHistogram();
                    pTableHistogram = new BasicTableHistogram() as ITableHistogram;

                    Debug.Assert((pTableHistogram != null));
                    if (pTableHistogram == null)
                        return;
                    pHistogram = (IBasicHistogram)pTableHistogram;

                    pTableHistogram.Field = pField.Name;
                    pTableHistogram.Table = pTable;
                    pHistogram.GetHistogram(out vntDataValues,out vntDataFrequency);

                    //数据类型必须为 “数据”
                    string strDataType = null;

                    strDataType = Information.TypeName(vntDataValues);

                    Debug.Assert(strDataType == "Integer()" | strDataType == "Long()" | strDataType == "Double()");
                    //该字段符合分类渲染的要求，加入之
                    m_colValidFeildsArray.Add(pField.Name);
                    pTableHistogram = null;
                }
            }

            if (m_colValidFeildsArray.Count > 0)
            {
                //初始化字段下拉框
                for (i = 0; i < m_colValidFeildsArray.Count; i++)
                {
                    cmbFields.Items.Add(m_colValidFeildsArray[i].ToString());
                }
                cmbFields.SelectedIndex = 0;
            }
            
            //初始化界窗体完毕
            m_bIsInitializing = false;
            btnApply.Enabled = true;
        }

        private void FrmThemeDot_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmThemeDot.TmpFrmThemeDot = null;
        }

        private void FrmThemeDot_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }
    }
}
