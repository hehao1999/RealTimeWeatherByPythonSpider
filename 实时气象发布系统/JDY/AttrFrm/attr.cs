using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using MongoDB.Bson;
using System;

namespace mainView.AttrFrm
{
    internal class attr
    {
        /// <summary>
        /// 格式化当前时间为可用于查询的时间格式
        /// </summary>
        /// <returns date_format>可用于查询的时间格式</returns>
        public static string GetTime()
        {
            string date_format = DateTime.Now.ToShortDateString().ToString().Replace("/", "-") + " " + DateTime.Now.Hour.ToString() + ":00:00";
            return date_format;
        }

        /// <summary>
        /// 修改矢量数据对象的属性表
        /// </summary>
        /// <param name="pFeature">数据对象</param>
        public static void SetValue(IFeature pFeature)
        {
            if (pFeature != null)
            {
                string[] fileds = { "AQI", "PRIMARYPOLLUTANT", "QUALITY", "ISO2", "INO2", "ICO", "IO3", "IPM10", "IPM2_5", "SO2", "NO2", "CO", "O3", "PM10", "PM2_5" };
                string city_code = pFeature.get_Value(5).ToString();  //数据表中5为city_code
                string time = GetTime();
                BsonDocument query_result = DataBaseClass.DBFuncs.QueryMongo("hour24", city_code, time);
                foreach (var filed in fileds)
                {
                    if (query_result != null)
                    {
                        string s = pFeature.Fields.FindField(filed).ToString();
                        string ss = query_result.GetValue(filed).ToString();
                        pFeature.set_Value(pFeature.Fields.FindField(filed), query_result.GetValue(filed).ToString());
                    }
                    else
                    {
                        pFeature.set_Value(pFeature.Fields.FindField(filed), "-1");
                        goto loop;
                    }
                }
            }
            loop: pFeature.Store();
        }

        /// <summary>
        /// 通过图层名称获取图层
        /// </summary>
        /// <param name="strLayerName">图层名称</param>
        /// <returns pLayer>图层</returns>
        public static ILayer GetLayerByName(string strLayerName)
        {
            ILayer pLayer = null;
            for (int i = 0; i <= mainForm.m_mapControl.LayerCount - 1; i++)
            {
                if (strLayerName == mainForm.m_mapControl.get_Layer(i).Name)
                {
                    pLayer = mainForm.m_mapControl.get_Layer(i);
                    break;
                }
            }
            return pLayer;
        }

        /// <summary>
        /// 更新属性表
        /// </summary>
        public static void RefreshAttrTable()
        {
            Command.DataBaseRefresh();
            IFeatureCursor pFeatureCursor;
            IFeatureClass pFeatureClass;
            IFeature pFeature;
            pFeatureClass = (GetLayerByName("城市") as IFeatureLayer).FeatureClass;
            pFeatureCursor = pFeatureClass.Search(null, true);
            while (true)
            {
                pFeature = pFeatureCursor.NextFeature();
                if (pFeature == null)
                    break;
                SetValue(pFeature);
            }
        }
    }
}