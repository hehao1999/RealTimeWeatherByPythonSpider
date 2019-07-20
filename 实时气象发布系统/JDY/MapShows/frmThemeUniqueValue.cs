using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace mainView.MapShows
{
    public partial class FrmThemeUniqueValue : Form
    {
        private mainForm _frmMain;

        #region 控制只打开一个界面

        public static FrmThemeUniqueValue TmpFrmThemeUniqueValue = null;

        #endregion 控制只打开一个界面

        public FrmThemeUniqueValue(mainForm TmpFrom)
        {
            InitializeComponent();
            _frmMain = TmpFrom;
            this.l_AxMapControl1 = _frmMain.pCurrentMap;
        }

        private void FrmThemeUniqueValue_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmThemeUniqueValue.TmpFrmThemeUniqueValue = null;
        }

        private void FrmThemeUniqueValue_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        #region "定义变量"

        //符号集合
        private IArray m_pSymbolsArray;

        //值集合
        private ArrayList m_colValues;

        //符号总数
        private int m_intSymbolsNum;

        //交替使用两个PictureBox
        private bool m_bChangeImageList;

        //是否更换符号
        private bool m_bChangeSymbol;

        //色带参数数组
        private int[,] m_intColorRampArray = new int[7, 7];

        private IRandomColorRamp pColorRamp;
        private IColor pNextUniqueColor;
        private IUniqueValueRenderer pUniqueValueRenderer;
        private IMap m_pMap;

        //要生成饼状图的地图
        public AxMapControl l_AxMapControl1;

        //当前地图图层
        private ArrayList m_colMapLayers;

        //要进行专题图渲染的图层
        private ILayer m_pLayer;

        //当前选择的图层
        private IGeoFeatureLayer m_pGeoFeatureLayer;

        //当前选中的图层符号类型
        private string m_strLyrSymbolType;

        //当前图层中适合进行饼状渲染的字段集合
        private ArrayList m_colValidFeildsArray = new ArrayList();

        //字段数组
        private IArray pLayerFieldsArray;

        //背景的面符号
        private ISimpleFillSymbol m_pDefaultFillSymbol;

        //背景的线符号
        private ISimpleLineSymbol m_pDefaultLineSymbol;

        //背景的点符号
        private ISimpleMarkerSymbol m_pDefaultMarkerSymbol;

        //字段个数及bars的个数
        private long m_FieldsCount;

        //图层原始渲染对像
        private IFeatureRenderer m_pFeatureRenderer;

        //定义饼状图渲染对象
        private IPieChartRenderer m_pPieChartRender;

        //定义统计字段值的对象
        private IStatisticsResults m_pStatisticsResults;

        //根据图层初始化窗体界面参数
        private bool m_bIsInitializing;

        //定义选中字段显示的表格对象
        private DataTable m_pDataTable = new DataTable();

        private DataColumn m_symbolCol = new DataColumn("颜色");

        #endregion "定义变量"

        private DataColumn m_valueCol = new DataColumn("字段");

        #region "初始化变量"

        private void InitVariables()
        {
            m_pMap = new Map();
            m_pMap = l_AxMapControl1.Map;
            m_pSymbolsArray = null;
            m_colValues = null;
            m_intSymbolsNum = -1;
            m_pGeoFeatureLayer = null;
            m_bChangeImageList = false;
            m_bChangeSymbol = false;
            m_strLyrSymbolType = "";
            m_colMapLayers = null;
            m_colMapLayers = new ArrayList();
        }

        #endregion "初始化变量"

        #region "设置色带"

        private void InitColorRamp()
        {
            //设置参数
            m_intColorRampArray[0, 0] = 0;
            m_intColorRampArray[0, 1] = 360;
            m_intColorRampArray[0, 2] = 70;
            m_intColorRampArray[0, 3] = 100;
            m_intColorRampArray[0, 4] = 10;
            m_intColorRampArray[0, 5] = 80;

            m_intColorRampArray[1, 0] = 40;
            m_intColorRampArray[1, 1] = 50;
            m_intColorRampArray[1, 2] = 0;
            m_intColorRampArray[1, 3] = 80;
            m_intColorRampArray[1, 4] = 10;
            m_intColorRampArray[1, 5] = 20;

            m_intColorRampArray[2, 0] = 140;
            m_intColorRampArray[2, 1] = 200;
            m_intColorRampArray[2, 2] = 50;
            m_intColorRampArray[2, 3] = 100;
            m_intColorRampArray[2, 4] = 30;
            m_intColorRampArray[2, 5] = 50;

            m_intColorRampArray[3, 0] = 20;
            m_intColorRampArray[3, 1] = 40;
            m_intColorRampArray[3, 2] = 90;
            m_intColorRampArray[3, 3] = 100;
            m_intColorRampArray[3, 4] = 70;
            m_intColorRampArray[3, 5] = 100;

            m_intColorRampArray[4, 0] = 70;
            m_intColorRampArray[4, 1] = 200;
            m_intColorRampArray[4, 2] = 90;
            m_intColorRampArray[4, 3] = 100;
            m_intColorRampArray[4, 4] = 70;
            m_intColorRampArray[4, 5] = 100;

            m_intColorRampArray[5, 0] = 20;
            m_intColorRampArray[5, 1] = 140;
            m_intColorRampArray[5, 2] = 90;
            m_intColorRampArray[5, 3] = 100;
            m_intColorRampArray[5, 4] = 70;
            m_intColorRampArray[5, 5] = 100;

            imgcboColorRamp.ImageList = iltColorRamp;
            //加载图例
            int i = 0;
            for (i = 0; i <= 5; i++)
            {
                imgcboColorRamp.Items.Add(new ImageComboItem("", i, false));
            }
            imgcboColorRamp.SelectedIndex = 0;
        }

        #endregion "设置色带"

        #region "得到地图中的featurelayer名字集合"

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
                if (m_pMap.LayerCount == 0)
                {
                    MessageBox.Show("当前地图中没有地图！", "提示");
                    return null;
                }

                pColFeatureLayers = new ArrayList();
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

        #endregion "得到地图中的featurelayer名字集合"

        #region "从指定的地图中获取指定名称的图层对象"

        public IFeatureLayer GetFeatureLayer(string strlayer, IMap m_pMap)
        {
            IFeatureLayer functionReturnValue = default(IFeatureLayer);
            functionReturnValue = null;
            IEnumLayer pEnumLayers = default(IEnumLayer);
            //枚举图层接口
            ILayer pLayer = default(ILayer);
            //图层接口
            try
            {
                if (m_pMap.LayerCount == 0)
                {
                    MessageBox.Show("当前地图中没有地图！", "提示");
                    return null;
                }

                pEnumLayers = m_pMap.get_Layers(null, true);
                pLayer = pEnumLayers.Next();
                while ((pLayer != null))
                {
                    //找到要素图层
                    if (pLayer is IFeatureLayer & strlayer.ToUpper() == pLayer.Name.ToUpper())
                    {
                        functionReturnValue = (IFeatureLayer)pLayer;
                    }
                    pLayer = pEnumLayers.Next();
                }
                pLayer = null;
                pEnumLayers = null;
            }
            catch (Exception ex)
            {
                functionReturnValue = null;
                MessageBox.Show(ex.ToString());
            }
            return functionReturnValue;
        }

        #endregion "从指定的地图中获取指定名称的图层对象"

        #region "获取图层中每一字段的，唯一值的集合"

        public IEnumerator GetUniqueValue(string strFldName, IMap m_pMap, string strLyrName, IFeatureLayer pfeaturelayer)
        {
            IEnumerator functionReturnValue = null;

            try
            {
                functionReturnValue = null;
                IGeoFeatureLayer pGeoFeaturelyr = default(IGeoFeatureLayer);
                //要素图层接口
                pGeoFeaturelyr = (IGeoFeatureLayer)pfeaturelayer;
                IQueryFilter pQueryFilter = new QueryFilter();
                IFeatureClass pFeatureClass = default(IFeatureClass);
                pFeatureClass = ((IFeatureLayer)pGeoFeaturelyr).FeatureClass;
                IFeatureCursor pFeatureCursor = default(IFeatureCursor);
                pQueryFilter.SubFields = strFldName;
                pFeatureCursor = pFeatureClass.Search(pQueryFilter, true);
                IDataStatistics pDastStat = new DataStatistics();
                pDastStat.Field = strFldName;
                pDastStat.Cursor = (ICursor)pFeatureCursor;
                functionReturnValue = pDastStat.UniqueValues;
                m_intSymbolsNum = pDastStat.UniqueValueCount;
                functionReturnValue.Reset();
                return functionReturnValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
            return functionReturnValue;
        }

        #endregion "获取图层中每一字段的，唯一值的集合"

        #region "将某一字段的唯一值显示在DataGridView中"

        private void DisplayValues()
        {
            IEnumerator pEnumVariantSimple = null;
            pEnumVariantSimple = GetUniqueValue(cmbFields.Items[cmbFields.SelectedIndex].ToString(), m_pMap, m_pGeoFeatureLayer.Name, m_pGeoFeatureLayer);
            object vntUniqueValue = null;
            int i = 0;
            if (m_intSymbolsNum > 4000)
            {
                MessageBox.Show("当前需要生成的符号总数太多，无法显示");
                btnApply.Enabled = false;
                return;
            }
            DataGridView.Rows.Clear();
            for (i = 0; i <= m_intSymbolsNum - 1; i++)
            {
                pEnumVariantSimple.MoveNext();
                vntUniqueValue = pEnumVariantSimple.Current;
                {
                    DataGridView.Rows.Add();
                    DataGridView.Rows[i].Cells[0].Value = (i + 1).ToString();
                    DataGridView.Rows[i].Cells[1].Value = vntUniqueValue;
                }
            }
            DataGridView.AllowUserToAddRows = false;
        }

        #endregion "将某一字段的唯一值显示在DataGridView中"

        #region "获取图层符号的类型：点、线、面"

        public string GetLayerSymbolType(IFeatureLayer pFeatureLayer)
        {
            string functionReturnValue = null;
            functionReturnValue = "";
            IFeatureClass pFeatureClass = default(IFeatureClass);
            esriGeometryType intShapeType = default(esriGeometryType);

            if (pFeatureLayer == null)
            {
                return null;
            }

            pFeatureClass = pFeatureLayer.FeatureClass;

            if (pFeatureClass == null)
            {
                return null;
            }

            intShapeType = pFeatureClass.ShapeType;

            //面符号
            if (intShapeType == esriGeometryType.esriGeometryPolygon | intShapeType == esriGeometryType.esriGeometryEnvelope)
            {
                functionReturnValue = "Fill Symbols";
            }
            return functionReturnValue;
        }

        #endregion "获取图层符号的类型：点、线、面"

        #region "获取指定，地图上，指定图层的字段集合，"

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
                        string ss = pField.Name;
                        string QQ = Convert.ToString(pField.VarType);
                        if (pField.Name.ToUpper() != "FID" & pField.Name.ToUpper() != "SHAPE" & pField.Name.ToUpper() != "SHAPE.LEN" & pField.Name.ToUpper() != "SHAPE.AREA" & pField.Name.ToUpper() != "OBJECTID")
                        {
                            pAryLayerField.Add(pField);
                        }
                    }
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

        #endregion "获取指定，地图上，指定图层的字段集合，"

        private void FrmThemeUniqueValue_Load(object sender, EventArgs e)
        {
            int i = 0;

            InitVariables();
            //初始化变量(含参数转换)
            Debug.Assert(!(m_pMap == null & m_pLayer == null));

            Debug.Assert((m_colMapLayers != null));
            if (m_colMapLayers == null)
                return;
            m_colMapLayers = GetMapFeatLayers(m_pMap);
            //获取地图中的图层
            if ((m_colMapLayers.Count < 1))
            {
                MessageBox.Show("当前地图中没有图层！");
                return;
            }

            for (i = 0; i < m_colMapLayers.Count; i++)
            {
                ListBoxLayers.Items.Add(m_colMapLayers[i]);
            }
            ListBoxLayers.SelectedIndex = 0;

            InitColorRamp();
            //设置色带
            if ((imgcboColorRamp.Items.Count > 0))
            {
                imgcboColorRamp.SelectedIndex = 0;
            }
        }

        private void cmbFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DisplayValues();
                imgcboColorRamp_SelectedIndexChanged(sender, e);
                btnApply.Enabled = true;
            }
            catch (Exception ex)
            {
                ;
            }
        }

        private void ListBoxLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_pMap == null)
            {
                MessageBox.Show("当前程序中没有地图文件！");
                return;
            }

            if (string.IsNullOrEmpty(ListBoxLayers.Text))
            {
                MessageBox.Show("当前程序中没有地图文件！");
                return;
            }

            m_pGeoFeatureLayer = (IGeoFeatureLayer)GetFeatureLayer(ListBoxLayers.Text, m_pMap);
            //得到需要渲染的图层
            m_pFeatureRenderer = m_pGeoFeatureLayer.Renderer;
            if (m_pGeoFeatureLayer == null)
                return;
            if (m_pGeoFeatureLayer.FeatureClass == null)
            {
                return;
            }

            m_strLyrSymbolType = GetLayerSymbolType(m_pGeoFeatureLayer);
            //确定图层符号类型
            Debug.Assert(!string.IsNullOrEmpty(m_strLyrSymbolType));
            if (string.IsNullOrEmpty(m_strLyrSymbolType))
                return;

            //设置字段
            IArray pLayerFieldsArray = default(IArray);
            Debug.Assert(!string.IsNullOrEmpty(ListBoxLayers.Text));
            if (string.IsNullOrEmpty(ListBoxLayers.Text))
                return;
            pLayerFieldsArray = GetLayerFields(m_pMap, ListBoxLayers.Text, null);
            //得到字段集合
            Debug.Assert((pLayerFieldsArray != null));
            if (pLayerFieldsArray == null)
                return;

            cmbFields.Items.Clear();
            IField pField = default(IField);
            int i = 0;
            for (i = 0; i <= pLayerFieldsArray.Count - 1; i++)
            {
                pField = (IField)pLayerFieldsArray.get_Element(i);
                cmbFields.Items.Add(pField.Name);
            }

            if (cmbFields.Items.Count != 0)
            {
                cmbFields.SelectedIndex = 0;
            }
        }

        private void imgcboColorRamp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_pGeoFeatureLayer == null)
                {
                    return;
                }
                if (m_pGeoFeatureLayer.FeatureClass == null)
                {
                    return;
                }

                pUniqueValueRenderer = new UniqueValueRenderer();
                Debug.Assert((pUniqueValueRenderer != null));
                if (pUniqueValueRenderer == null)
                    return;

                pUniqueValueRenderer.FieldCount = 1;
                pUniqueValueRenderer.set_Field(0, cmbFields.Text);

                pColorRamp = new RandomColorRamp();
                Debug.Assert((pColorRamp != null));
                if (pColorRamp == null)
                    return;
                //选择色带
                int i = 0;
                for (i = 0; i <= 5; i++)
                {
                    if (imgcboColorRamp.SelectedIndex == i)
                    {
                        pColorRamp.StartHue = m_intColorRampArray[i, 0];
                        pColorRamp.EndHue = m_intColorRampArray[i, 1];
                        pColorRamp.MinValue = m_intColorRampArray[i, 2];
                        pColorRamp.MaxValue = m_intColorRampArray[i, 3];
                        pColorRamp.MinSaturation = m_intColorRampArray[i, 4];
                        pColorRamp.MaxSaturation = m_intColorRampArray[i, 5];
                    }
                }
                pColorRamp.Size = 100;
                bool tmpbln = true;
                pColorRamp.CreateRamp(out tmpbln);

                IEnumColors pEnumRamp = default(IEnumColors);

                ITable pTable = default(ITable);
                long fieldNumber = 0;
                object vntCodeValue = null;
                vntCodeValue = null;
                object vntTempValue = null;
                vntTempValue = null;
                Debug.Assert((m_pMap != null));
                if (m_pMap == null)
                    return;
                if (string.IsNullOrEmpty(ListBoxLayers.Text))
                    return;
                if (string.IsNullOrEmpty(cmbFields.Text))
                    return;

                pTable = (ITable)m_pGeoFeatureLayer;
                fieldNumber = pTable.FindField(cmbFields.Text);
                if ((fieldNumber == -1))
                {
                    MessageBox.Show("您选择的图层没有属性字段，无法进行渲染！");
                    return;
                }

                pEnumRamp = pColorRamp.Colors;
                pNextUniqueColor = null;

                //面图层
                if (m_strLyrSymbolType == "Fill Symbols")
                {
                    IFillSymbol pFillSymbol = default(IFillSymbol);
                    int RowCount = 0;
                    RowCount = this.DataGridView.RowCount;
                    m_pSymbolsArray = null;
                    m_pSymbolsArray = new ESRI.ArcGIS.esriSystem.Array();
                    m_colValues = null;
                    m_colValues = new ArrayList();

                    for (i = 0; i <= RowCount - 1; i++)
                    {
                        Debug.Assert((m_pSymbolsArray != null));
                        if (m_pSymbolsArray == null)
                            return;
                        Debug.Assert((m_colValues != null));
                        if (m_colValues == null)
                            return;

                        pNextUniqueColor = pEnumRamp.Next();
                        pFillSymbol = new SimpleFillSymbol();
                        Debug.Assert((pFillSymbol != null));
                        if (pFillSymbol == null)
                            return;
                        IRgbColor pRGBColor = new RgbColor();
                        pRGBColor.RGB = pNextUniqueColor.RGB;
                        this.DataGridView.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.FromArgb(pRGBColor.Red, pRGBColor.Green, pRGBColor.Blue);
                        vntCodeValue = DataGridView.Rows[i].Cells[1].Value;
                        pFillSymbol.Color = pNextUniqueColor;
                        m_pSymbolsArray.Add(pFillSymbol);
                        m_colValues.Add(vntCodeValue);
                        pFillSymbol = null;
                    }
                }

                if (m_colValues == null | m_pSymbolsArray == null)
                    return;

                pUniqueValueRenderer = new UniqueValueRenderer();
                Debug.Assert((pUniqueValueRenderer != null));
                if (pUniqueValueRenderer == null)
                    return;

                pUniqueValueRenderer.FieldCount = 1;
                pUniqueValueRenderer.set_Field(0, cmbFields.Text);

                for (i = 0; i <= m_intSymbolsNum - 1; i++)
                {
                    ISymbol pSymbol = default(ISymbol);
                    pSymbol = (ISymbol)m_pSymbolsArray.get_Element(i);
                    pUniqueValueRenderer.AddValue((string)m_colValues[i], (string)m_colValues[i], pSymbol);
                }
                btnApply.Enabled = true;
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //刷新
            try
            {
                m_pGeoFeatureLayer.Renderer = (IFeatureRenderer)pUniqueValueRenderer;
                m_pGeoFeatureLayer.DisplayField = pUniqueValueRenderer.get_Field(0);
                _frmMain.pCurrentTOC.Update();
                _frmMain.pCurrentMap.Refresh();
                this.buttonOk.Enabled = true;
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                m_pGeoFeatureLayer.Renderer = this.m_pFeatureRenderer;
                _frmMain.pCurrentTOC.Update();
                this.Close();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (btnApply.Enabled == true)
            {
                btnApply_Click(null, null);
            }
            this.Close();
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}