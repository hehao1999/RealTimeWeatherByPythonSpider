using ESRI.ArcGIS.Carto;

//using ESRI.ArcGIS.GeoAnalyst;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

//using ESRI.ArcGIS.SpatialAnalyst;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace mainView.MapShows
{
    public partial class FrmThemeGraduate : Form
    {
        private mainForm _frmMain;

        #region 控制只打开一个界面

        public static FrmThemeGraduate TmpFrmThemeGraduate = null;

        #endregion 控制只打开一个界面

        public FrmThemeGraduate()
        {
            InitializeComponent();
        }

        public FrmThemeGraduate(mainForm TmpFrom)
        {
            InitializeComponent();
            _frmMain = TmpFrom;
            this.Map = _frmMain.pCurrentMap.Map;
        }

        private void FrmThemeGraduate_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_pSymbolsArray = null;
            m_colMapLayers = null;
            m_pDefaultFillSymbol = null;
            m_pDefaultLineSymbol = null;
            m_pDefaultMarkerSymbol = null;
            m_colValidFeildsArray = null;

            FrmThemeGraduate.TmpFrmThemeGraduate = null;
        }

        private void FrmThemeGraduate_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        //分类渲染方式
        private enum tagClassRenderMode
        {
            enumClassRenderWithColor = 1,

            //颜色渐变
            enumClassRenderWithSize = 2,

            //尺寸渐变
            enumClassRenderWithOnlyOneSymbol = 3,

            //属于分类渲染，但只有一个符号（分类无意义）
            enumClassRenderShapeTypeInvalid = 4

            //图层类型参数错误
        }

        //要生成分类图的地图
        private IMap m_pMap;

        //要生成分类图的图层
        private ILayer m_pLayer;

        //当前地图图层
        private ArrayList m_colMapLayers;

        //当前选择的图层
        private IGeoFeatureLayer m_pGeoFeatureLayer;

        //当前选中的图层类型
        private string m_strShapeType;

        //色带参数数组
        private int[,] m_intColorRampArray = new int[7, 7];

        //符号集合
        private IArray m_pSymbolsArray;

        //分类值的区段集合
        private ArrayList m_colValueRanges;

        //位图句柄
        private long m_hNewBmp;

        //当前色带索引
        private int m_intColorRampIndex;

        //分类值
        private double[] m_dblClassesValues;

        //改变符号
        private bool m_bChangeSymbol;

        //默认的面符号
        private ISimpleFillSymbol m_pDefaultFillSymbol;

        //默认的线符号
        private ISimpleLineSymbol m_pDefaultLineSymbol;

        //默认的点符号
        private ISimpleMarkerSymbol m_pDefaultMarkerSymbol;

        //新生成的符号（线符号和点符号）
        private ISymbol m_pNewSymbol;

        //新生成的线符号
        private ISymbol m_pNewLineSymbol;

        //新生成的背景符号
        private ISymbol m_pNewBackgroundSymbol;

        //分类数目
        private string m_strClassCount;

        //符号最小尺寸
        private string m_strMinSize;

        //符号最大尺寸
        private string m_strMaxSize;

        //当前键盘的ascii代码
        private int m_intKeyAscii;

        //图层已经分类渲染过
        private bool m_bHasBeenRendered;

        //根据图层初始化窗体界面参数
        private bool m_bIsInitializing;

        //当前图层中适合进行分类渲染的字段集合
        private ArrayList m_colValidFeildsArray = new ArrayList();

        //字段的统计值区段个数
        private int m_intDataValuesNum;

        //当前的分类渲染模式（颜色渐变还是尺寸渐变）
        private tagClassRenderMode m_enumClassRenderMode;

        //自定义等间距之默认间距
        private double m_dblCurrentIntervalRange;

        private IStatisticsResults m_pStatisticsResults;
        private IRandomColorRamp pColorRamp;

        //图层原始渲染对像
        private IFeatureRenderer m_pFeatureRenderer;

        //other
        private bool pbMark1 = false;

        private bool pbBackground = false;

        //窗体需要设置的参数之一（可选）

        public IMap Map
        {
            get { return m_pMap; }
            set { m_pMap = value; }
        }

        private void FrmThemeGraduate_Load(object sender, EventArgs e)
        {
            try
            {
                InitVariables();

                //初始化控件
                InitControls();

                //初始化色带
                InitColorRamp();

                //生成默认符号
                CreateDefaultSymbols();

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

                ListBoxLayers.SelectedIndex = 0;
                m_colMapLayers = null;

                RadioButtonColor.Checked = true;
                RadioButtonSize.Checked = false;
                fraSymbol.Visible = false;
                fraBackground.Visible = false;
                fraColorRamp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //初始化空件

        private void InitControls()
        {
            ComboBoxCls.Items.Add("等间距");
            ComboBoxCls.Items.Add("标准方差");
            ComboBoxCls.Items.Add("柔和分割");
            //ComboBoxCls.Items.Add("自定义等间距");
            ComboBoxCls.Items.Add("分位");
        }

        //初始化变量

        private void InitVariables()
        {
            m_colMapLayers = null;
            m_colValueRanges = null;
            m_pSymbolsArray = null;
            m_pGeoFeatureLayer = null;
            m_pDefaultFillSymbol = null;
            m_pDefaultLineSymbol = null;
            m_pDefaultMarkerSymbol = null;
            m_pNewSymbol = null;
            m_pNewLineSymbol = null;
            m_pNewBackgroundSymbol = null;
            m_pStatisticsResults = null;
            m_bChangeSymbol = false;
            m_bHasBeenRendered = false;
            m_bIsInitializing = false;
            m_intColorRampIndex = 1;
            m_strClassCount = "2";
            m_strMaxSize = "10";
            m_strMinSize = "2";
            m_intKeyAscii = 47;
            m_intDataValuesNum = -1;
            m_dblCurrentIntervalRange = -1;

            if (m_pMap == null)
            {
                return;
            }

            m_pSymbolsArray = new ESRI.ArcGIS.esriSystem.Array();

            if (m_pSymbolsArray == null)
            {
                MessageBox.Show("初始化分类对话框失败。");
                //Interaction.MsgBox("初始化分类对话框失败。");
                return;
            }

            m_colValueRanges = new ArrayList();
            if (m_colValueRanges == null)
            {
                MessageBox.Show("初始化分类对话框失败。");
                //Interaction.MsgBox("初始化分类对话框失败。");
                return;
            }
        }

        //生成默认的线符号和点符号

        private void CreateDefaultSymbols()
        {
            //面符号
            ISimpleFillSymbol pFillSymbol = default(ISimpleFillSymbol);
            pFillSymbol = new SimpleFillSymbol();

            if (pFillSymbol == null)
            {
                return;
            }

            IRgbColor pRgbColor = default(IRgbColor);
            pRgbColor = new RgbColor();
            if (pRgbColor == null)
            {
                return;
            }

            {
                pRgbColor.Red = 239;
                pRgbColor.Green = 228;
                pRgbColor.Blue = 190;
                pRgbColor.UseWindowsDithering = true;
            }

            pFillSymbol.Color = pRgbColor;
            pRgbColor = null;
            pRgbColor = new RgbColor();

            if (pRgbColor == null)
            {
                return;
            }

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
            if (pRgbColor == null)
            {
                return;
            }

            {
                pRgbColor.Red = 239;
                pRgbColor.Green = 0;
                pRgbColor.Blue = 0;
                pRgbColor.UseWindowsDithering = true;
            }

            //点符号
            ISimpleMarkerSymbol pMarkerSymbol = default(ISimpleMarkerSymbol);
            pMarkerSymbol = new SimpleMarkerSymbol();

            if (pMarkerSymbol == null)
            {
                return;
            }

            pRgbColor = new RgbColor();
            if (pRgbColor == null)
            {
                return;
            }

            {
                pRgbColor.Red = 255;
                pRgbColor.Green = 0;
                pRgbColor.Blue = 0;
                pRgbColor.UseWindowsDithering = true;
            }

            pMarkerSymbol.Color = pRgbColor;
            pMarkerSymbol.Size = 4;

            m_pDefaultMarkerSymbol = pMarkerSymbol;
            pMarkerSymbol = null;
            pRgbColor = null;
        }

        private void InitColorRamp()
        {
            //设置参数

            m_intColorRampArray[0, 0] = 40;
            m_intColorRampArray[0, 1] = 200;
            m_intColorRampArray[0, 2] = 60;
            m_intColorRampArray[0, 3] = 75;
            m_intColorRampArray[0, 4] = 55;
            m_intColorRampArray[0, 5] = 75;

            m_intColorRampArray[1, 0] = 0;
            m_intColorRampArray[1, 1] = 60;
            m_intColorRampArray[1, 2] = 90;
            m_intColorRampArray[1, 3] = 100;
            m_intColorRampArray[1, 4] = 70;
            m_intColorRampArray[1, 5] = 85;

            m_intColorRampArray[2, 0] = 120;
            m_intColorRampArray[2, 1] = 220;
            m_intColorRampArray[2, 2] = 90;
            m_intColorRampArray[2, 3] = 100;
            m_intColorRampArray[2, 4] = 70;
            m_intColorRampArray[2, 5] = 85;

            m_intColorRampArray[3, 0] = 0;
            m_intColorRampArray[3, 1] = 120;
            m_intColorRampArray[3, 2] = 90;
            m_intColorRampArray[3, 3] = 100;
            m_intColorRampArray[3, 4] = 70;
            m_intColorRampArray[3, 5] = 85;

            //加载图例
            int i = 0;
            for (i = 0; i <= 3; i++)
            {
                imgcboColorRamp.Items.Add(new ImageComboItem("", i, false));
            }
            imgcboColorRamp.SelectedIndex = 0;
        }

        //得到地图中的featurelayer名字集合
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
                if (m_pMap.LayerCount == 0)
                {
                    return null;
                }

                pColFeatureLayers = new ArrayList();
                pEnumLayer = m_pMap.get_Layers(null, true);
                pLayer = pEnumLayer.Next();

                while ((pLayer != null))
                {
                    if (pLayer is IFeatureLayer)
                    {
                        if (pLayer.Name == "城市" || pLayer.Name == "区县")
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

        private void buttonCancel_Click(object sender, EventArgs e)
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

        //根据图层初始化分类渲染窗体
        private void Init()
        {
            int i = 0;

            //图层已经进行过分类渲染,获取图层参数初始化

            if (m_pGeoFeatureLayer.Renderer is IClassBreaksRenderer)
            {
                Debug.Assert((m_pGeoFeatureLayer != null));
                Debug.Assert(m_pGeoFeatureLayer.Renderer is IClassBreaksRenderer);
                if (!(m_pGeoFeatureLayer.Renderer is IClassBreaksRenderer))
                    return;

                m_bHasBeenRendered = true;

                IClassBreaksRenderer pClassRenderer = default(IClassBreaksRenderer);

                pClassRenderer = (IClassBreaksRenderer)m_pGeoFeatureLayer.Renderer;
                Debug.Assert((pClassRenderer != null));

                //得到图层当前的分类符号（不含背景符号）
                m_pSymbolsArray.RemoveAll();
                m_pSymbolsArray = GetLayerSymbols(m_pGeoFeatureLayer);
                Debug.Assert(!(m_pSymbolsArray.Count < 1));

                //得到图层当前的分类符号的值的分类区段
                for (i = 0; i < m_colValueRanges.Count; i++)
                {
                    m_colValueRanges.Remove(0);
                }

                for (i = 1; i <= pClassRenderer.BreakCount; i++)
                {
                    if (i == 1)
                    {
                        m_colValueRanges.Add("0" + " - " + pClassRenderer.get_Break(0));
                    }
                    else
                    {
                        m_colValueRanges.Add(pClassRenderer.get_Break(i - 2) + " - " + pClassRenderer.get_Break(i - 1));
                    }
                }

                //初始化分类类型单选框
                m_enumClassRenderMode = GetClassRenderMode(m_pGeoFeatureLayer, m_strShapeType);

                //初始化字段下拉框
                for (i = 0; i < m_colValidFeildsArray.Count; i++)
                {
                    cmbFields.Items.Add(m_colValidFeildsArray[i]);
                }

                if (cmbFields.Items.Count > 1)
                {
                    cmbFields.SelectedIndex = 1;
                }
                else if (cmbFields.Items.Count == 1)
                {
                    cmbFields.SelectedIndex = 0;
                }

                //初始化分类方式下拉框
                ComboBoxCls.SelectedIndex = 0;

                //初始化分类等级数文本框
                Debug.Assert(pClassRenderer.BreakCount > 1);
                txtClassCount.Text = Convert.ToString(pClassRenderer.BreakCount);

                //初始化点或线最大、最小尺寸文本框

                if (m_enumClassRenderMode == tagClassRenderMode.enumClassRenderWithSize)
                {
                    Debug.Assert(!string.IsNullOrEmpty(m_strShapeType));

                    if (m_strShapeType == "Fill Symbols" | m_strShapeType == "Marker Symbols")
                    {
                        try
                        {
                            IMarkerSymbol pMarkerSymbol = default(IMarkerSymbol);
                            pMarkerSymbol = (IMarkerSymbol)pClassRenderer.get_Symbol(0);
                            txtMinSize.Text = Convert.ToString(pMarkerSymbol.Size);
                            pMarkerSymbol = (IMarkerSymbol)pClassRenderer.get_Symbol(pClassRenderer.BreakCount - 1);
                            txtMaxSize.Text = Convert.ToString(pMarkerSymbol.Size);
                        }
                        catch
                        {
                            ;
                        }
                    }
                }

                //初始化示例PictureBox
                if (RadioButtonColor.Checked == true)
                    picSample.ImageLocation = Application.StartupPath + "\\Bitmaps\\GraduatedColor.bmp";
                if (RadioButtonSize.Checked == true)
                    picSample.ImageLocation = Application.StartupPath + "\\Bitmaps\\GraduatedSymbol.bmp";
                //初始化色带下拉框(设置默认值即可)
                imgcboColorRamp.SelectedIndex = 0;
                //初始化点线面PictureBox(2个)及动态界面
                if (RadioButtonSize.Checked == true)
                {
                    LoadLayerSymbols();
                }
                else
                {
                    fraBackground.Visible = false;
                    fraSymbol.Visible = false;
                    fraColorRamp.Visible = true;
                }
            }

            //图层未进行分类渲染，设置默认参数初始化
            if (!(m_pGeoFeatureLayer.Renderer is IClassBreaksRenderer))
            {
                //初始化分类类型单选框
                RadioButtonColor.Checked = Convert.ToBoolean((RadioButtonSize.Checked == true ? false : true));
                RadioButtonSize.Checked = Convert.ToBoolean((RadioButtonColor.Checked == false ? true : false));

                //初始化字段下拉框
                for (i = 0; i < m_colValidFeildsArray.Count; i++)
                {
                    cmbFields.Items.Add(m_colValidFeildsArray[i]);
                }

                if (cmbFields.Items.Count > 1)
                {
                    cmbFields.SelectedIndex = 1;
                }
                else if (cmbFields.Items.Count == 1)
                {
                    cmbFields.SelectedIndex = 0;
                }

                //初始化分类方式下拉框
                ComboBoxCls.SelectedIndex = 0;
                //初始化分类等级数文本框
                if (m_intDataValuesNum < 0)
                    return;
                if (m_intDataValuesNum == 0)
                    txtClassCount.Text = "1";
                if (m_intDataValuesNum == 1)
                    txtClassCount.Text = "2";
                if (m_intDataValuesNum < 5)
                    txtClassCount.Text = "3";
                if (m_intDataValuesNum > 4)
                    txtClassCount.Text = "5";

                //初始化最大、最小尺寸文本框
                txtMinSize.Text = "2";
                txtMaxSize.Text = "10";

                //初始化示例PictureBox
                if (RadioButtonColor.Checked)
                {
                    picSample.ImageLocation = Application.StartupPath + "\\Bitmaps\\GraduatedColor.bmp";
                    picSample.Load();
                }
                if (RadioButtonSize.Checked)
                {
                    picSample.ImageLocation = Application.StartupPath + "\\Bitmaps\\GraduatedSymbol.bmp";
                    picSample.Load();
                }

                //初始化色带下拉框(设置默认值即可)
                if (imgcboColorRamp.SelectedIndex == -1)
                    imgcboColorRamp.SelectedIndex = 0;

                //初始化面PictureBox(2个)及动态界面
                LoadDefaultSymbols();
            }
        }

        //加载默认符号（包括背景）
        private void LoadDefaultSymbols()
        {
            Debug.Assert(!string.IsNullOrEmpty(m_strShapeType));
            if (string.IsNullOrEmpty(m_strShapeType))
                return;

            ISymbol pSymbol = default(ISymbol);

            //面符号

            if (m_strShapeType == "Fill Symbols")
            {
                Debug.Assert((m_pDefaultMarkerSymbol != null));
                if (m_pDefaultMarkerSymbol == null)
                    return;

                pSymbol = (ISymbol)m_pDefaultMarkerSymbol;

                Debug.Assert((m_pDefaultFillSymbol != null));
                if (m_pDefaultFillSymbol == null)
                    return;

                if (m_pNewBackgroundSymbol == null)
                {
                    pSymbol = (ISymbol)m_pDefaultFillSymbol;
                }
                else
                {
                    pSymbol = m_pNewBackgroundSymbol;
                }

                if (RadioButtonColor.Checked == true)
                {
                    fraBackground.Visible = false;
                    fraSymbol.Visible = false;
                    fraColorRamp.Visible = true;
                }
                else
                {
                    fraBackground.Visible = true;
                    fraSymbol.Visible = true;
                    fraColorRamp.Visible = false;
                }

                picBackground.Refresh();
            }
        }

        //图层已经进行过分类渲染时，提取若干符号初始化PictureBox(2个)及更新动态界面
        private void LoadLayerSymbols()
        {
            Debug.Assert(m_pSymbolsArray.Count > 1);
            Debug.Assert(!string.IsNullOrEmpty(m_strShapeType));
            if (string.IsNullOrEmpty(m_strShapeType))
                return;
            if (RadioButtonSize.Checked)
            {
                if (m_strShapeType == "Fill Symbols" | m_strShapeType == "Marker Symbols")
                {
                    IMarkerSymbol pMarkerSymbol = default(IMarkerSymbol);
                    pMarkerSymbol = (IMarkerSymbol)m_pSymbolsArray.get_Element(0);
                    Debug.Assert((pMarkerSymbol != null));
                    pMarkerSymbol.Size = 2;
                    if (m_strShapeType == "Fill Symbols")
                    {
                        IClassBreaksRenderer pClassRenderer = default(IClassBreaksRenderer);
                        pClassRenderer = (IClassBreaksRenderer)m_pGeoFeatureLayer.Renderer;
                        if (pClassRenderer == null)
                            return;
                        if (pClassRenderer.BackgroundSymbol == null)
                            return;
                        m_pNewBackgroundSymbol = (ISymbol)pClassRenderer.BackgroundSymbol;
                        fraBackground.Visible = true;
                        picBackground.Refresh();
                    }
                    fraColorRamp.Visible = false;
                    fraSymbol.Visible = true;
                }

                if (m_strShapeType == "Line Symbols")
                {
                    ILineSymbol pLineSymbol = default(ILineSymbol);
                    pLineSymbol = (ILineSymbol)m_pSymbolsArray.get_Element(0);
                    Debug.Assert((pLineSymbol != null));
                    pLineSymbol.Width = 2;
                    fraBackground.Visible = false;
                    fraColorRamp.Visible = false;
                    fraSymbol.Visible = true;
                }
            }
            else
            {
                fraBackground.Visible = false;
                fraColorRamp.Visible = true;
                fraSymbol.Visible = false;
            }
        }

        //判断图层按类型渲染时的渲染方式（尺寸渐变还是颜色渐变）
        private tagClassRenderMode GetClassRenderMode(IGeoFeatureLayer pGeoFeatureLayer, string strShapeType)
        {
            Debug.Assert(!string.IsNullOrEmpty(strShapeType));
            Debug.Assert((pGeoFeatureLayer.Renderer != null));
            Debug.Assert(pGeoFeatureLayer.Renderer is IClassBreaksRenderer);
            if (string.IsNullOrEmpty(strShapeType) | (pGeoFeatureLayer.Renderer == null) | (!(pGeoFeatureLayer.Renderer is IClassBreaksRenderer)))
            {
                return tagClassRenderMode.enumClassRenderShapeTypeInvalid;
            }

            IClassBreaksRenderer pClassRenderer = default(IClassBreaksRenderer);
            pClassRenderer = (IClassBreaksRenderer)pGeoFeatureLayer.Renderer;

            //只有一个符号不予考虑
            if (pClassRenderer.BreakCount < 2)
            {
                return tagClassRenderMode.enumClassRenderWithOnlyOneSymbol;
            }

            //面符号
            if (strShapeType == "Fill Symbols")
            {
                ISymbol pBackgroundSymbol = default(ISymbol);
                pBackgroundSymbol = (ISymbol)pClassRenderer.BackgroundSymbol;
                return (tagClassRenderMode)(pBackgroundSymbol == null ? tagClassRenderMode.enumClassRenderWithColor : tagClassRenderMode.enumClassRenderWithSize);
            }

            return tagClassRenderMode.enumClassRenderShapeTypeInvalid;
        }

        public IArray GetLayerSymbols(ILayer pLayer)
        {
            if (pLayer == null)
            {
                return null;
            }
            int i = 0;
            IGeoFeatureLayer pGeoFeatureLayer = default(IGeoFeatureLayer);
            pGeoFeatureLayer = (IGeoFeatureLayer)pLayer;
            if (pGeoFeatureLayer == null)
            {
                return null;
            }
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
                try
                {
                    for (i = 0; i <= pClassRenderer.BreakCount - 1; i++)
                    {
                        pSymbolArray.Add(pClassRenderer.get_Symbol(i));
                    }
                }
                catch
                {
                    ;
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
                {
                    return null;
                }

                for (i = 0; i <= pMySymbolArray.SymbolCount - 1; i++)
                {
                    pSymbolArray.Add(pMySymbolArray.get_Symbol(i));
                }

                Debug.Assert(!(pSymbolArray.Count < 1));
            }

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
            return pSymbolArray;
        }

        private void DisplayClassesValue()
        {
            if (m_colValueRanges.Count < 1)
                return;
            if (Convert.ToInt64(txtClassCount.Text) > m_colValueRanges.Count)
            {
                txtClassCount.Text = Convert.ToString(m_colValueRanges.Count);
            }

            int i = 0;
            DataGridView.Rows.Clear();

            for (i = 0; i <= Convert.ToInt32(txtClassCount.Text) - 1; i++)
            {
                {
                    DataGridView.Rows.Add();
                    DataGridView.Rows[i].Cells[0].Value = (i + 1).ToString();
                    DataGridView.Rows[i].Cells[1].Value = m_colValueRanges[i];
                }
            }
            DataGridView.AllowUserToAddRows = false;
        }

        //根据图层类型、字段、分级数目、当前色带创建符号(颜色渐变)
        private void CreateGraduatedSymbolsWithColor()
        {
            if (string.IsNullOrEmpty(txtClassCount.Text))
                return;
            int i = 0;

            //清空符号数组
            m_pSymbolsArray.RemoveAll();

            //得到色带颜色
            IColor pColor = default(IColor);
            IEnumColors pEnumColors = default(IEnumColors);
            pEnumColors = CreateColorRamp();
            Debug.Assert((pEnumColors != null));
            if (pEnumColors == null)
                return;

            //生成符号
            Debug.Assert(!string.IsNullOrEmpty(m_strShapeType));
            if (string.IsNullOrEmpty(m_strShapeType))
                return;

            //面图层

            if (m_strShapeType == "Fill Symbols")
            {
                ISimpleFillSymbol pFillSymbol = default(ISimpleFillSymbol);

                for (i = 0; i <= Convert.ToInt32(txtClassCount.Text) - 1; i++)
                {
                    pColor = pEnumColors.Next();
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
        }

        //创建色带
        private IEnumColors CreateColorRamp()
        {
            IEnumColors functionReturnValue = default(IEnumColors);

            //起点颜色
            IHsvColor pFromColor = default(IHsvColor);

            pFromColor = new HsvColor();
            Debug.Assert((pFromColor != null));
            if (pFromColor == null)
            {
                return null;
            }

            pFromColor.Hue = m_intColorRampArray[m_intColorRampIndex, 0];
            pFromColor.Value = m_intColorRampArray[m_intColorRampIndex, 2];
            pFromColor.Saturation = m_intColorRampArray[m_intColorRampIndex, 4];
            //终点颜色
            IHsvColor pToColor = default(IHsvColor);

            pToColor = new HsvColor();
            Debug.Assert((pToColor != null));
            if (pToColor == null)
            {
                return null;
            }
            pToColor.Hue = m_intColorRampArray[m_intColorRampIndex, 1];
            pToColor.Value = m_intColorRampArray[m_intColorRampIndex, 3];
            pToColor.Saturation = m_intColorRampArray[m_intColorRampIndex, 5];
            //创建色带
            IAlgorithmicColorRamp pRamp = default(IAlgorithmicColorRamp);
            bool ok = false;

            pRamp = new AlgorithmicColorRamp();
            Debug.Assert((pRamp != null));
            if (pRamp == null)
            {
                return null;
            }

            pRamp.Algorithm = esriColorRampAlgorithm.esriHSVAlgorithm;
            pRamp.FromColor = pFromColor;
            pRamp.ToColor = pToColor;
            pRamp.Size = Convert.ToInt32(txtClassCount.Text);
            if (pRamp.Size < 1)
            {
                return null;
            }

            pRamp.CreateRamp(out ok);
            functionReturnValue = pRamp.Colors;

            pFromColor = null;
            pToColor = null;
            pRamp = null;
            return functionReturnValue;
        }

        private IColor ConvertColorToIColor(Color pColor)
        {
            IColor pESRIColor = new RgbColor();
            pESRIColor.RGB = pColor.B * 65536 + pColor.G * 256 + pColor.R;
            return pESRIColor;
        }

        //根据图层类型、字段、分级数目创建符号(符号渐变)
        private void CreateGraduatedSymbolsWithSize()
        {
            IClone pSourceSymbol = default(IClone);
            int i = 0;

            //清空符号数组
            m_pSymbolsArray.RemoveAll();

            Debug.Assert(!string.IsNullOrEmpty(m_strShapeType));
            if (string.IsNullOrEmpty(m_strShapeType))
                return;

            double dblTemp = 0;
            if (string.IsNullOrEmpty(txtMaxSize.Text) | string.IsNullOrEmpty(txtMinSize.Text) | string.IsNullOrEmpty(txtClassCount.Text))
                return;
            dblTemp = (Convert.ToDouble(txtMaxSize.Text) - Convert.ToDouble(txtMinSize.Text)) / Convert.ToDouble((Convert.ToInt64(txtClassCount.Text) - 1));

            //面图层和点图层

            if (m_strShapeType == "Fill Symbols" | m_strShapeType == "Marker Symbols")
            {
                IMarkerSymbol pMarkerSymbol = null;
                IMarkerSymbol pNewMarkerSymbol = null;

                if (((m_pNewSymbol != null)))
                {
                    if (m_pNewSymbol is IMarkerSymbol)
                        pMarkerSymbol = (IMarkerSymbol)m_pNewSymbol;
                }
                else
                {
                    pMarkerSymbol = m_pDefaultMarkerSymbol;
                }

                Debug.Assert((pMarkerSymbol != null));
                if (pMarkerSymbol == null)
                {
                    return;
                }
                if (pbMark1)
                {
                    pMarkerSymbol.Color = ConvertColorToIColor(PictureBox1.BackColor);
                    pbMark1 = false;
                }
                pSourceSymbol = (IClone)pMarkerSymbol;

                //最小尺寸符号
                pNewMarkerSymbol = null;
                pNewMarkerSymbol = (IMarkerSymbol)pSourceSymbol.Clone();
                Debug.Assert((pNewMarkerSymbol != null));
                if (string.IsNullOrEmpty(txtMinSize.Text))
                    return;
                pNewMarkerSymbol.Size = Convert.ToDouble(txtMinSize.Text);
                m_pSymbolsArray.Add(pNewMarkerSymbol);

                //中间尺寸符号

                for (i = 1; i <= Convert.ToInt32(txtClassCount.Text) - 2; i++)
                {
                    pNewMarkerSymbol = null;
                    pNewMarkerSymbol = (IMarkerSymbol)pSourceSymbol.Clone();
                    Debug.Assert((pNewMarkerSymbol != null));
                    pNewMarkerSymbol.Size = Convert.ToDouble(txtMinSize.Text) + i * dblTemp;
                    m_pSymbolsArray.Add(pNewMarkerSymbol);
                }

                //最大尺寸符号
                pNewMarkerSymbol = null;
                pNewMarkerSymbol = (IMarkerSymbol)pSourceSymbol.Clone();
                Debug.Assert((pNewMarkerSymbol != null));
                pNewMarkerSymbol.Size = Convert.ToDouble(txtMaxSize.Text);
                m_pSymbolsArray.Add(pNewMarkerSymbol);

                pNewMarkerSymbol = null;
            }
        }

        //计算分类值
        private double[] CalculateClassedValue()
        {
            double[] functionReturnValue = null;

            int i = 0;
            if ((m_colValueRanges != null))
            {
                m_colValueRanges.Clear();
            }

            ITable pTable = default(ITable);
            ITableHistogram pTableHistogram = default(ITableHistogram);
            IBasicHistogram pHistogram = default(IBasicHistogram);

            object vntDataFrequency = null;
            object vntDataValues = null;

            //统计图层中选中字段的数据
            Debug.Assert((m_pGeoFeatureLayer != null));
            if (m_pGeoFeatureLayer == null)
            {
                return null;
            }

            pTable = (ITable)m_pGeoFeatureLayer;
            pTableHistogram = new BasicTableHistogram() as ITableHistogram;

            if (pTableHistogram == null)
            {
                return null;
            }

            pHistogram = (IBasicHistogram)pTableHistogram;

            IStatisticsResults pStatisticsResults = default(IStatisticsResults);
            pStatisticsResults = (IStatisticsResults)pTableHistogram;

            if (string.IsNullOrEmpty(cmbFields.Text))
            {
                return null;
            }

            pTableHistogram.Field = cmbFields.Text;
            pTableHistogram.Table = pTable;
            pHistogram.GetHistogram(out vntDataValues, out vntDataFrequency);

            //数据类型必须为 “数据”
            string strDataType = null;
            strDataType = Information.TypeName(vntDataValues);
            if ((!(strDataType == "Integer()")) & (!(strDataType == "Long()")) & (!(strDataType == "Double()")))
            {
                MessageBox.Show("分类的数据类型错误，不是数据（可能是字符串）");
                return null;
            }

            IClassifyGEN pClassify;

            #region 定义分类方式

            if (ComboBoxCls.Items[ComboBoxCls.SelectedIndex].ToString() == "等间距")
            {
                pClassify = new EqualInterval();
            }
            else if (ComboBoxCls.Items[ComboBoxCls.SelectedIndex].ToString() == "分位")
            {
                pClassify = new Quantile();
            }
            else if (ComboBoxCls.Items[ComboBoxCls.SelectedIndex].ToString() == "柔和分割")
            {
                pClassify = new NaturalBreaks();
            }
            else if (ComboBoxCls.Items[ComboBoxCls.SelectedIndex].ToString() == "标准方差")
            {
                pClassify = new StandardDeviation();

                IStatisticsResults pStatRes = default(IStatisticsResults);
                pStatRes = (IStatisticsResults)pHistogram;
                IDeviationInterval pStdDev = default(IDeviationInterval);
                pStdDev = (IDeviationInterval)pClassify;

                pStdDev.Mean = pStatRes.Mean;
                pStdDev.StandardDev = pStatRes.StandardDeviation;
                pStdDev.DeviationInterval = 1;
            }
            else if (ComboBoxCls.Items[ComboBoxCls.SelectedIndex].ToString() == "自定义等间距")
            {
                pClassify = new DefinedInterval();

                IIntervalRange pIntervalRange = default(IIntervalRange);
                pIntervalRange = (IIntervalRange)pClassify;

                Debug.Assert((pStatisticsResults != null));
                if (pStatisticsResults == null)
                {
                    return null;
                }
                //当前图层没有进行自定义等间距的分类渲染
                if (m_dblCurrentIntervalRange == -1)
                {
                    //计算默认等间距
                    if (pStatisticsResults.Minimum > 0 & (pStatisticsResults.Minimum * 2 < pStatisticsResults.Maximum))
                    {
                        if (pStatisticsResults.Minimum < 100 & pStatisticsResults.Maximum > 1000000)
                        {
                            pIntervalRange.IntervalRange = 100000;
                        }
                        else
                        {
                            pIntervalRange.IntervalRange = pStatisticsResults.Minimum;
                        }
                    }
                    if (pStatisticsResults.Minimum > 0 & !(pStatisticsResults.Minimum * 2 < pStatisticsResults.Maximum))
                    {
                        pIntervalRange.IntervalRange = 0.1;
                    }
                }
                else
                {
                    pIntervalRange.IntervalRange = m_dblCurrentIntervalRange;
                }
                m_pStatisticsResults = pStatisticsResults;
            }
            else
            {
                pClassify = new EqualInterval();
            }

            #endregion 定义分类方式

            //得到分类等级
            if (string.IsNullOrEmpty(txtClassCount.Text))
                txtClassCount.Text = "3";
            int ddd;
            ddd = Convert.ToInt32(txtClassCount.Text);
            pClassify.Classify(vntDataValues, vntDataFrequency, ref ddd);
            functionReturnValue = (double[])pClassify.ClassBreaks;

            int intClassesCount = 0;
            intClassesCount = functionReturnValue.Length;

            Debug.Assert(!(intClassesCount < 1));

            m_dblClassesValues = functionReturnValue;
            m_colValueRanges.Clear();
            m_colValueRanges.Add("0" + " - " + Convert.ToString(m_dblClassesValues[0]));

            for (i = 0; i < intClassesCount - 1; i++)
            {
                m_colValueRanges.Add(Convert.ToString(m_dblClassesValues[i]) + " - " + Convert.ToString(m_dblClassesValues[i + 1]));
            }
            Debug.Assert(!(m_colValueRanges.Count < 1));
            return functionReturnValue;
        }

        //由分类方式下拉框得到选定的分类方式
        private IClassify GetSelectedClassMode(IBasicHistogram pHistogram, IStatisticsResults pStatisticsResults)
        {
            Debug.Assert((pHistogram != null));
            if (pHistogram == null)
            {
                return null;
            }

            if (ComboBoxCls.Items[ComboBoxCls.SelectedIndex].ToString() == "等间距")
            {
                EqualInterval pClassify = null;
                return (IClassify)pClassify;
            }
            else if (ComboBoxCls.Items[ComboBoxCls.SelectedIndex].ToString() == "分位")
            {
                Quantile pClassify = null;
                return (IClassify)pClassify;
            }
            else if (ComboBoxCls.Items[ComboBoxCls.SelectedIndex].ToString() == "柔和分割")
            {
                NaturalBreaks pClassify = null;
                return (IClassify)pClassify;
            }
            else if (ComboBoxCls.Items[ComboBoxCls.SelectedIndex].ToString() == "标准方差")
            {
                StandardDeviation pClassify = new StandardDeviation();

                IStatisticsResults pStatRes = default(IStatisticsResults);
                pStatRes = (IStatisticsResults)pHistogram;
                IDeviationInterval pStdDev = default(IDeviationInterval);
                pStdDev = (IDeviationInterval)pClassify;

                pStdDev.Mean = pStatRes.Mean;
                pStdDev.StandardDev = pStatRes.StandardDeviation;
                pStdDev.DeviationInterval = 1;

                return (IClassify)pClassify;
            }
            else if (ComboBoxCls.Items[ComboBoxCls.SelectedIndex].ToString() == "自定义等间距")
            {
                DefinedInterval pClassify = new DefinedInterval();

                IIntervalRange pIntervalRange = default(IIntervalRange);
                pIntervalRange = (IIntervalRange)pClassify;

                Debug.Assert((pStatisticsResults != null));
                if (pStatisticsResults == null)
                {
                    return null;
                }
                //当前图层没有进行自定义等间距的分类渲染
                if (m_dblCurrentIntervalRange == -1)
                {
                    //计算默认等间距
                    if (pStatisticsResults.Minimum > 0 & (pStatisticsResults.Minimum * 2 < pStatisticsResults.Maximum))
                    {
                        if (pStatisticsResults.Minimum < 100 & pStatisticsResults.Maximum > 1000000)
                        {
                            pIntervalRange.IntervalRange = 100000;
                        }
                        else
                        {
                            pIntervalRange.IntervalRange = pStatisticsResults.Minimum;
                        }
                    }
                    if (pStatisticsResults.Minimum > 0 & !(pStatisticsResults.Minimum * 2 < pStatisticsResults.Maximum))
                    {
                        pIntervalRange.IntervalRange = 0.1;
                    }
                }
                else
                {
                    pIntervalRange.IntervalRange = m_dblCurrentIntervalRange;
                }
                //初始化统计变量
                m_pStatisticsResults = pStatisticsResults;

                return (IClassify)pClassify;
            }
            else
            {
                return null;
            }
        }

        private void RadioButtonColor_Click(object sender, EventArgs e)
        {
            if (m_bIsInitializing == false)
            {
                if (!(m_enumClassRenderMode == tagClassRenderMode.enumClassRenderWithColor))
                {
                    CreateGraduatedSymbolsWithColor();
                    //根据图层类型、字段、分级数目、当前色带创建符号
                    if (m_colValueRanges.Count < 1)
                    {
                        CalculateClassedValue();
                    }
                }
                else
                {
                    Init();
                }

                //显示符号（含分类值）
                DisplayClassesValue();

                DisplayClassesValue();

                //更新界面
                if (ComboBoxCls.SelectedIndex == 3)
                {
                    ComboBoxCls.Width = 1500;
                }

                picSample.ImageLocation = Application.StartupPath + "\\Bitmaps\\GraduatedColor.bmp";
                picSample.Load();
                fraSymbol.Visible = false;
                fraBackground.Visible = false;
                fraColorRamp.Visible = true;
                btnApply.Enabled = true;
            }
        }

        private void RadioButtonSize_Click(object sender, EventArgs e)
        {
            if (m_bIsInitializing == false)
            {
                if (!(m_enumClassRenderMode == tagClassRenderMode.enumClassRenderWithSize))
                {
                    CreateGraduatedSymbolsWithSize();
                    LoadDefaultSymbols();
                }
                else
                {
                    Init();
                }

                DisplayClassesValue();

                picSample.ImageLocation = Application.StartupPath + "\\Bitmaps\\GraduatedSymbol.bmp";
                picSample.Load();
                fraSymbol.Visible = true;
                fraBackground.Visible = true;
                fraColorRamp.Visible = false;
                btnApply.Enabled = true;
            }
        }

        private void ListBoxLayers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DisplayClassesValue();
        }

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

        public string GetLayerShapeType(IGeoFeatureLayer pGeoFeatureLayer)
        {
            IFeatureClass pFeatureClass = default(IFeatureClass);
            esriGeometryType intShapeType = default(esriGeometryType);
            string LayerShapeType = "";
            if (pGeoFeatureLayer == null)
            {
                return null;
            }

            pFeatureClass = pGeoFeatureLayer.FeatureClass;

            if (pFeatureClass == null)
            {
                return null;
            }

            intShapeType = pFeatureClass.ShapeType;

            //面符号
            if (intShapeType == esriGeometryType.esriGeometryPolygon | intShapeType == esriGeometryType.esriGeometryEnvelope)
            {
                LayerShapeType = "Fill Symbols";
            }

            return LayerShapeType;
        }

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
                        break; ;
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

            //获取字段
            IArray pLayerFieldsArray = default(IArray);

            if (string.IsNullOrEmpty(ListBoxLayers.SelectedItem.ToString()))
            {
                return;
            }

            pLayerFieldsArray = GetLayerFields(m_pMap, ListBoxLayers.SelectedItem.ToString(), null);

            if (pLayerFieldsArray == null)
            {
                return;
            }

            cmbFields.Items.Clear();
            int i = 0;
            IField pField = default(IField);

            //过滤字段
            for (i = 0; i <= pLayerFieldsArray.Count - 1; i++)
            {
                pField = (IField)pLayerFieldsArray.get_Element(i);

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
                    System.Array ddd;
                    ddd = (System.Array)vntDataValues;
                    m_intDataValuesNum = ddd.Length;
                    if (m_intDataValuesNum <= 0)
                        return;

                    m_colValidFeildsArray.Add(pField.Name);
                    pTableHistogram = null;
                }
            }

            //根据图层情况初始化分类渲染窗体
            Init();

            if (m_bHasBeenRendered == false)
            {
                CalculateClassedValue();
                //计算分类值
                if (RadioButtonColor.Checked == true)
                    CreateGraduatedSymbolsWithColor();
                //创建颜色渐变符号
                if (RadioButtonSize.Checked == true)
                    CreateGraduatedSymbolsWithSize();
                //创建尺寸渐变符号
            }

            //显示符号
            DisplayClassesValue();

            //初始化界窗体完毕
            m_bIsInitializing = false;

            btnApply.Enabled = true;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (RadioButtonColor.Checked)
            {
                CreateGraduatedSymbolsWithColor();

                //只有一个分段值或没有，取消操作
                if (m_colValueRanges.Count < 2)
                    return;

                if (string.IsNullOrEmpty(txtClassCount.Text))
                {
                    btnApply.Enabled = false;
                    return;
                }

                //创建分类渲染对象
                IClassBreaksRenderer pClassBreaksRenderer = default(IClassBreaksRenderer);
                pClassBreaksRenderer = new ClassBreaksRenderer();
                Debug.Assert((pClassBreaksRenderer != null));
                if (pClassBreaksRenderer == null)
                    return;

                pClassBreaksRenderer.Field = cmbFields.Text;
                pClassBreaksRenderer.BreakCount = Convert.ToInt32(txtClassCount.Text);
                pClassBreaksRenderer.SortClassesAscending = true;

                //设置符号
                try
                {
                    int i = 0;
                    for (i = 0; i <= Convert.ToInt32(txtClassCount.Text) - 1; i++)
                    {
                        pClassBreaksRenderer.set_Symbol(i, (ISymbol)m_pSymbolsArray.get_Element(i));
                        pClassBreaksRenderer.set_Break(i, m_dblClassesValues[i + 1]);
                    }
                }
                catch
                {
                    ;
                }

                if (RadioButtonColor.Checked == true)
                {
                    if (m_strShapeType == "Fill Symbols")
                    {
                        if ((m_pNewBackgroundSymbol != null))
                        {
                            if (m_pNewBackgroundSymbol is IFillSymbol)
                            {
                                pClassBreaksRenderer.BackgroundSymbol = (IFillSymbol)m_pNewBackgroundSymbol;
                            }
                        }

                        if (m_pNewBackgroundSymbol == null)
                        {
                            pClassBreaksRenderer.BackgroundSymbol = m_pDefaultFillSymbol;
                        }
                    }
                }
                m_pGeoFeatureLayer.Renderer = (IFeatureRenderer)pClassBreaksRenderer;
            }
            if (RadioButtonSize.Checked)
            {
                CreateGraduatedSymbolsWithSize();

                //只有一个分段值或没有，取消操作
                if (m_colValueRanges.Count < 2)
                    return;

                if (string.IsNullOrEmpty(txtClassCount.Text))
                {
                    btnApply.Enabled = false;
                    return;
                }

                //创建分类渲染对象
                IClassBreaksRenderer pClassBreaksRenderer = default(IClassBreaksRenderer);
                pClassBreaksRenderer = new ClassBreaksRenderer();
                Debug.Assert((pClassBreaksRenderer != null));
                if (pClassBreaksRenderer == null)
                    return;

                pClassBreaksRenderer.Field = cmbFields.Text;
                pClassBreaksRenderer.BreakCount = Convert.ToInt32(txtClassCount.Text);
                pClassBreaksRenderer.SortClassesAscending = true;

                //设置符号
                int i = 0;
                for (i = 0; i <= Convert.ToInt32(txtClassCount.Text) - 1; i++)
                {
                    pClassBreaksRenderer.set_Symbol(i, (ISymbol)m_pSymbolsArray.get_Element(i));
                    pClassBreaksRenderer.set_Break(i, m_dblClassesValues[i + 1]);
                }

                if (RadioButtonSize.Checked == true)
                {
                    if (m_strShapeType == "Fill Symbols")
                    {
                        if ((m_pNewBackgroundSymbol != null))
                        {
                            if (m_pNewBackgroundSymbol is IFillSymbol)
                            {
                                pClassBreaksRenderer.BackgroundSymbol = (IFillSymbol)m_pNewBackgroundSymbol;
                            }
                        }
                        if (pbBackground)
                        {
                            m_pDefaultFillSymbol.Color = ConvertColorToIColor(picBackground.BackColor);
                            pbBackground = false;
                        }
                        if (m_pNewBackgroundSymbol == null)
                        {
                            pClassBreaksRenderer.BackgroundSymbol = m_pDefaultFillSymbol;
                        }
                    }
                }
                m_pGeoFeatureLayer.Renderer = (IFeatureRenderer)pClassBreaksRenderer;
            }

            _frmMain.pCurrentMap.Refresh();
            _frmMain.pCurrentTOC.Update();
            btnApply.Enabled = true;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (btnApply.Enabled == true)
            {
                btnApply_Click(null, null);
            }
            this.Close();
        }

        private void cmbFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_bIsInitializing == false)
            {
                //图层已经分类渲染过，得到这些符号
                if (m_bHasBeenRendered == true)
                {
                    Debug.Assert((m_pGeoFeatureLayer != null));
                    m_pSymbolsArray = GetLayerSymbols(m_pGeoFeatureLayer);
                    m_colValueRanges = GetCurrentValueRanges(m_pGeoFeatureLayer);
                    //图层未进行过分类渲染，生成新符号
                }
                else
                {
                    //创建颜色渐变符号
                    if (RadioButtonColor.Checked == true)
                    {
                        CreateGraduatedSymbolsWithColor();
                    }

                    //创建尺寸渐变符号
                    if (RadioButtonSize.Checked == true)
                    {
                        CreateGraduatedSymbolsWithSize();

                        //更新界面
                        LoadDefaultSymbols();
                    }
                    //计算分类值
                    CalculateClassedValue();
                }
                //显示符号（含分类值）
                DisplayClassesValue();
                btnApply.Enabled = true;
            }
        }

        public ArrayList GetCurrentValueRanges(ILayer pLayer)
        {
            if (pLayer == null)
            {
                return null;
            }

            IGeoFeatureLayer pGeoFeatureLayer = default(IGeoFeatureLayer);
            pGeoFeatureLayer = (IGeoFeatureLayer)pLayer;
            if (pGeoFeatureLayer == null)
            {
                return null;
            }

            ArrayList colValueRanges = default(ArrayList);
            colValueRanges = new ArrayList();
            Debug.Assert((colValueRanges != null));
            if (colValueRanges == null)
            {
                return null;
            }
            if (pGeoFeatureLayer.Renderer is IClassBreaksRenderer)
            {
                IClassBreaksRenderer pClassRenderer = default(IClassBreaksRenderer);
                pClassRenderer = (IClassBreaksRenderer)pGeoFeatureLayer.Renderer;

                colValueRanges.Add("0" + "-" + pClassRenderer.get_Break(0));

                int i = 0;
                for (i = 0; i <= pClassRenderer.BreakCount - 2; i++)
                {
                    colValueRanges.Add(pClassRenderer.get_Break(i) + "-" + pClassRenderer.get_Break(i + 1));
                }
            }
            return colValueRanges;
        }

        private void txtClassCount_KeyUp(object sender, KeyEventArgs e)
        {
            txtClassCount.Value = txtClassCount.Value;
        }

        private void txtClassCount_MouseUp(object sender, MouseEventArgs e)
        {
            txtClassCount.Value = txtClassCount.Value;
        }

        private void txtClassCount_ValueChanged(object sender, EventArgs e)
        {
            if (m_bIsInitializing == false)
            {
                if (string.IsNullOrEmpty(txtClassCount.Text) | txtClassCount.Text == "0" | txtClassCount.Text == "1")
                    return;

                txtClassCount.Text = txtClassCount.Value.ToString();
                if (RadioButtonColor.Checked == true)
                    CreateGraduatedSymbolsWithColor();
                if (RadioButtonSize.Checked == true)
                    CreateGraduatedSymbolsWithSize();
                CalculateClassedValue();
                DisplayClassesValue();
                btnApply.Enabled = true;
            }
        }

        private void ComboBoxCls_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_bIsInitializing == false)
            {
                if (RadioButtonColor.Checked == true)
                    CreateGraduatedSymbolsWithColor();
                if (RadioButtonSize.Checked == true)
                    CreateGraduatedSymbolsWithSize();
                CalculateClassedValue();
                DisplayClassesValue();
                btnApply.Enabled = true;
            }
        }

        private void txtMinSize_TextChanged(object sender, EventArgs e)
        {
            if (m_bIsInitializing == false)
            {
                if (string.IsNullOrEmpty(txtMinSize.Text) | txtMinSize.Text == "0")
                    return;
                if (RadioButtonSize.Checked == true)
                {
                    CreateGraduatedSymbolsWithSize();
                }
                else
                {
                    return;
                }
                CalculateClassedValue();
                DisplayClassesValue();
                btnApply.Enabled = true;
            }
        }

        private void txtMaxSize_TextChanged(object sender, EventArgs e)
        {
            if (m_bIsInitializing == false)
            {
                if (string.IsNullOrEmpty(txtMaxSize.Text) | txtMaxSize.Text == "0")
                    return;
                if (RadioButtonSize.Checked == true)
                {
                    CreateGraduatedSymbolsWithSize();
                }
                else
                {
                    return;
                }
                CalculateClassedValue();
                DisplayClassesValue();
                btnApply.Enabled = true;
            }
        }

        private void imgcboColorRamp_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_intColorRampIndex = this.imgcboColorRamp.SelectedIndex;

            pColorRamp = new RandomColorRamp();
            Debug.Assert((pColorRamp != null));
            if (pColorRamp == null)
                return;
            int i = 0;
            for (i = 0; i <= 3; i++)
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
            bool ddd = true;
            pColorRamp.CreateRamp(out ddd);
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            ColorDialog1.AllowFullOpen = true;
            ColorDialog1.FullOpen = true;
            ColorDialog1.ShowHelp = true;
            ColorDialog1.Color = Color.Black;
            ColorDialog1.ShowDialog();
            PictureBox1.BackColor = ColorDialog1.Color;
        }

        private void picBackground_Click(object sender, EventArgs e)
        {
            ColorDialog1.AllowFullOpen = true;
            ColorDialog1.FullOpen = true;
            ColorDialog1.ShowHelp = true;
            ColorDialog1.Color = Color.Black;
            ColorDialog1.ShowDialog();
            picBackground.BackColor = ColorDialog1.Color;
        }

        private void PictureBox1_BackColorChanged(object sender, EventArgs e)
        {
            pbMark1 = true;
        }

        private void picBackground_BackColorChanged(object sender, EventArgs e)
        {
            pbBackground = true;
        }
    }
}