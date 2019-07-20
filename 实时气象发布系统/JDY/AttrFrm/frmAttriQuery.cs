using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Windows.Forms;

namespace mainView.AttrFrm
{
    public partial class frmAttriQuery : Form
    {
        private AxMapControl axMapControl1;             //用来获取主界面中的那个主要的mapcontrol
        private esriSelectionResultEnum selectmethod = esriSelectionResultEnum.esriSelectionResultNew;//用来记录处理结果的方法，用在用whereclause查询的地方
        private IFeatureSelection pFeatureSelection;       //用来记录最终的结果，由于用户可能在不关闭此窗口的情况下进行

        public frmAttriQuery()
        {
            InitializeComponent();
        }

        private void frmAttriQuery_Load(object sender, EventArgs e)
        {
            AddAllLayerstoComboBox(comboBoxLayers);
            if (comboBoxLayers.Items.Count != 0)
            {
                comboBoxLayers.SelectedIndex = 0;
                //让combox的当前选中的项目变为第一项，即让其默认选中第一项。否则的话为空，这样在
                //变换checkboxShowVectorOnly后，如果listBox列表不清空（已经改进了这个问题），则会出现listBOXfields有内容
                //而combox为空的情况，进而点击任意列明的时候，需要列出属性值，这里需要根据combox的
                //当前值来获取图层，具体见listBoxFields_SelectedIndexChanged(object sender, EventArgs e)
                //，而其值为空，就会出错了
                comboBoxMethod.Enabled = true;
                comboBoxMethod.SelectedIndex = 0;

                buttonOk.Enabled = true;
                buttonClear.Enabled = true;
                buttonApply.Enabled = true;
            }
        }

        #region 以下是自己的函数

        private void AddAllLayerstoComboBox(ComboBox combox)
        {
            try
            {
                combox.Items.Clear();

                int pLayerCount = mainForm.m_mapControl.LayerCount;
                if (pLayerCount > 0)
                {
                    combox.Enabled = true;//下拉菜单可用
                    checkBoxShowVectorOnly.Enabled = true;//复选框可用

                    for (int i = 0; i <= pLayerCount - 1; i++)
                    {
                        if (checkBoxShowVectorOnly.Checked)
                        {
                            if (mainForm.m_mapControl.get_Layer(i) is IFeatureLayer)  //只添加矢量图层，栅格图层没有属性表
                                combox.Items.Add(mainForm.m_mapControl.get_Layer(i).Name);
                        }
                        else
                            combox.Items.Add(mainForm.m_mapControl.get_Layer(i).Name);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private ILayer GetLayerByName(string strLayerName)
        {
            ILayer pLayer = null;
            for (int i = 0; i <= mainForm.m_mapControl.LayerCount - 1; i++)
            {
                if (strLayerName == mainForm.m_mapControl.get_Layer(i).Name)
                { pLayer = mainForm.m_mapControl.get_Layer(i); break; }
            }
            return pLayer;
        }

        private void PerformAttributeSelect()
        {
            try
            {
                IQueryFilter pQueryFilter = new QueryFilter() as IQueryFilter;
                IFeatureLayer pFeatureLayer;

                pQueryFilter.WhereClause = textBoxWhereClause.Text;
                pFeatureLayer = GetLayerByName(comboBoxLayers.SelectedItem.ToString()) as IFeatureLayer;
                pFeatureSelection = pFeatureLayer as IFeatureSelection;

                int iSelectedFeaturesCount = pFeatureSelection.SelectionSet.Count;
                pFeatureSelection.SelectFeatures(pQueryFilter, selectmethod, false);//执行查询

                //如果本次查询后，查询的结果数目0，则认为本次查询到结果
                if (pFeatureSelection.SelectionSet.Count == 0)
                {
                    MessageBox.Show("没有符合本次查询条件的结果！");
                    return;
                }

                //如果复选框被选中，则定位到选择结果
                if (checkBoxZoomtoSelected.Checked == true)
                {
                    IEnumFeature pEnumFeature = mainForm.m_mapControl.Map.FeatureSelection as IEnumFeature;
                    IFeature pFeature = pEnumFeature.Next();
                    IEnvelope pEnvelope = new Envelope() as IEnvelope;
                    while (pFeature != null)
                    {
                        pEnvelope.Union(pFeature.Extent);
                        pFeature = pEnumFeature.Next();
                    }
                    mainForm.m_mapControl.ActiveView.Extent = pEnvelope;
                    mainForm.m_mapControl.ActiveView.Refresh();//如果不这样刷新，只要查询前地图已经被放大所效果的话，定位后
                    //底图没有刷新，选择集倒是定位和刷新了
                }
                else mainForm.m_mapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);

                double i = mainForm.m_mapControl.Map.SelectionCount;
                i = Math.Round(i, 0);//小数点后指定为０位数字
            }
            catch (Exception ex)
            {
                MessageBox.Show("您的查询语句可能有误,请检查 | " + ex.Message);
                return;
            }
        }

        #endregion 以下是自己的函数

        private void checkBoxShowVectorOnly_CheckedChanged(object sender, EventArgs e)
        {
            //当只显示矢量数据的复选框改变的时候,则要重新加载图层名到图层列表中
            AddAllLayerstoComboBox(comboBoxLayers);
            if (comboBoxLayers.Items.Count != 0)
                comboBoxLayers.SelectedIndex = 0;//让combox的当前选中的项目变为第一项，即让其默认选中第一项。否则的话为空，这样在
            //变换checkboxShowVectorOnly后，如果listBox列表不清空（已经改进了这个问题），则会出现listBOXfields有内容
            //而combox为空的情况，进而点击任意列明的时候，需要列出属性值，这里需要根据combox的
            //当前值来获取图层，具体见listBoxFields_SelectedIndexChanged(object sender, EventArgs e)
            //，而其值为空，就会出错了
            listBoxFields.Items.Clear();//为什么这么做，见AddAllLayersCombox的注视
        }

        private void comboBoxLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxFields.Items.Clear();
            listBoxValues.Items.Clear();
            string strSelectedLayerName = comboBoxLayers.Text;
            IFeatureLayer pFeatureLayer;
            IDisplayTable pDisPlayTable;

            try
            {
                for (int i = 0; i <= mainForm.m_mapControl.LayerCount - 1; i++)
                {
                    if (mainForm.m_mapControl.get_Layer(i).Name == strSelectedLayerName)
                    {
                        if (mainForm.m_mapControl.get_Layer(i) is IFeatureLayer)
                        {
                            pFeatureLayer = mainForm.m_mapControl.get_Layer(i) as IFeatureLayer;

                            pDisPlayTable = pFeatureLayer as IDisplayTable;

                            for (int j = 0; j <= pDisPlayTable.DisplayTable.Fields.FieldCount - 1; j++)
                            {
                                listBoxFields.Items.Add(pDisPlayTable.DisplayTable.Fields.get_Field(j).Name);
                            }

                            labelDescription2.Text = strSelectedLayerName;
                        }
                        else
                        { MessageBox.Show("您选择的图层不能够进行属性查询!请重新选择"); break; }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void comboBoxMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxMethod.SelectedIndex)
            {
                case 0: selectmethod = esriSelectionResultEnum.esriSelectionResultNew; break;
                case 1: selectmethod = esriSelectionResultEnum.esriSelectionResultAdd; break;
                case 2: selectmethod = esriSelectionResultEnum.esriSelectionResultSubtract; break;
                case 3: selectmethod = esriSelectionResultEnum.esriSelectionResultAnd; break;
            }
        }

        private void listBoxFields_DoubleClick(object sender, EventArgs e)
        {
            //此处用selectedtext可以直接获得想要的效果，而用textbox.text的话需要经过一些处理，比较麻烦
            //但根据selectedtext在帮助里的内容却看不出来是为什么……
            textBoxWhereClause.SelectedText = listBoxFields.SelectedItem.ToString() + " ";
        }

        private void listBoxValues_DoubleClick(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " " + listBoxValues.SelectedItem.ToString();
        }

        private void buttonEqual_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " = ";
        }

        private void buttonNotEqual_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " <> ";
        }

        private void buttonBig_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " > ";
        }

        private void buttonBigEqual_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " >= ";
        }

        private void buttonSmall_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " < ";
        }

        private void buttonSmallEqual_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " <= ";
        }

        private void buttonChars_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = "%";
        }

        private void buttonChar_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = "_";
        }

        private void buttonLike_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " Like ";
        }

        private void buttonAnd_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " And ";
        }

        private void buttonOr_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " Or ";
        }

        private void buttonNot_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " Not ";
        }

        private void buttonIs_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " Is ";
        }

        private void buttonBrace_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = "(  )";
            //让输入的位置恰好处在（）里面，就同arcmap的效果一样
            textBoxWhereClause.SelectionStart = textBoxWhereClause.Text.Length - 2;
        }

        private void buttonGetValue_Click(object sender, EventArgs e)
        {
            if (listBoxFields.Text == "")
                MessageBox.Show("请选择一个属性字段！");
            else
            {
                try
                {
                    frmPromptGetValues frm = new frmPromptGetValues();//当属性记录非常多的时候（比如说９００００）
                    //打开速度会很慢，在arcmap中也很慢。但它会出现一个提示窗口，这里借鉴了这种做法
                    frm.Show();
                    System.Windows.Forms.Application.DoEvents();
                    string strSelectedFieldName = listBoxFields.Text;//这个名字是选中的属性字段的名称
                    listBoxValues.Items.Clear();
                    label1.Text = "";

                    //IQueryFilter pQueryFilter = new QueryFilterClass();
                    IFeatureCursor pFeatureCursor;
                    IFeatureClass pFeatureClass;
                    IFeature pFeature;
                    double i = 0;//记录总数
                    if (strSelectedFieldName != null)
                    {
                        pFeatureClass = (GetLayerByName(comboBoxLayers.Text) as IFeatureLayer).FeatureClass;
                        pFeatureCursor = pFeatureClass.Search(null, true);
                        pFeature = pFeatureCursor.NextFeature();
                        int index = pFeatureClass.FindField(strSelectedFieldName);
                        while (pFeature != null)
                        {
                            i++;
                            string strValue = pFeature.get_Value(index).ToString();
                            if (checkBoxGetUniqueValue.Checked)   //如果去掉重复的值
                            {
                                if (pFeature.Fields.get_Field(index).Type == esriFieldType.esriFieldTypeString)
                                    //因为pFeature.get_Field().后面没有Type的属性，所以得用pFeature.fileds.XXXX
                                    strValue = "'" + strValue + "'";//如果属性值是字符，则添加' '，方便后面whereclause的格式
                                if (listBoxValues.FindStringExact(strValue) == ListBox.NoMatches)
                                {
                                    listBoxValues.Items.Add(strValue);
                                }
                            }
                            else                                  //否则添加所有的值，不管有没有重复
                            {
                                if (pFeature.Fields.get_Field(index).Type == esriFieldType.esriFieldTypeString)
                                    strValue = "'" + strValue + "'";//如果属性值是字符，则添加' '，方便后面whereclause的格式
                                listBoxValues.Items.Add(strValue);
                            }

                            if (i % 50 == 0)    //每五十个记录提示窗口更新一次
                            {
                                System.Windows.Forms.Application.DoEvents();//天哪，这方法都被我发现了，就是在这里发现的：
                                //这个doevents的功能是：When you run a Windows Form, it creates the new form,
                                //which then waits for events to handle. Each time the form handles an event,
                                //it processes all the code associated with that event. All other events wait in the queue.
                                //While your code handles the event, your application does not respond. For example,
                                //    the window does not repaint if another window is dragged on top.
                                //可以理解为，把当前的控制权交给这个函数下面的代码（事件）。所以这个提示框里的两个label才可能不断变化
                                //否则，就跟失去响应了一样，就是仅仅出现提示窗口，但是label是不会更新的

                                frm.labelname.Text = strValue;      //提示窗口提示当前正在添加的字段
                                frm.labelcount.Text = i.ToString(); //提示窗口提示当前正在添加的第几条记录
                            }

                            pFeature = pFeatureCursor.NextFeature();
                        }
                    }
                    frm.Dispose();
                    label1.Text = listBoxValues.Items.Count.ToString() + "条记录";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.Clear();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (textBoxWhereClause.Text == "")
            {
                MessageBox.Show("请生成查询语句！");
                return;
            }
            this.WindowState = FormWindowState.Minimized;//通过位置查询窗口最小化

            frmPromptQuerying frmPrompt = new frmPromptQuerying();
            frmPrompt.Show();
            System.Windows.Forms.Application.DoEvents();//'转让控制权，没有这一句的话提示窗口不能正常显示

            PerformAttributeSelect();
            frmPrompt.Dispose();
            this.WindowState = FormWindowState.Normal;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;//通过位置查询窗口最小化

            frmPromptQuerying frmPrompt = new frmPromptQuerying();
            frmPrompt.Show();
            System.Windows.Forms.Application.DoEvents();//'转让控制权，没有这一句的话提示窗口不能正常显示

            PerformAttributeSelect();
            frmPrompt.Dispose();
            this.Dispose();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void listBoxFields_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}