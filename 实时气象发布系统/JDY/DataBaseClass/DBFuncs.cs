using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace mainView.DataBaseClass
{
    internal class DBFuncs
    {
        public static MongoClient client = new MongoClient("mongodb://127.0.0.1:27017");
        public static IMongoDatabase database = client.GetDatabase("RealTimeWeather");

        /// <summary>
        ///查询mongo数据库中符合条件的值
        /// </summary>
        /// <param name="col">数据库集合名称</param>
        /// <param name="city_code">城市编码</param>
        /// <param name="timepoint">时间</param>
        /// <returns result>查询结果</returns>
        public static BsonDocument QueryMongo(string col, string city_code, string timepoint)
        {
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(col);
            //创建约束生成器
            FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
            //约束条件
            FilterDefinition<BsonDocument> filter = builder.And(builder.Eq("city_code", city_code), builder.Eq("TIMEPOINT", timepoint));
            try
            {
                var result = collection.Find<BsonDocument>(filter).ToList()[0];
                return result;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取空气质量预报信息
        /// </summary>
        /// <param name="path">AQF.txt文件的路径</param>
        /// <returns forcast>预报内容</returns>
        public static string GetAQF(string path)
        {
            StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8);
            string forcast = sr.ReadToEnd();
            return forcast;
        }

        /// <summary>
        /// 获取天气预报数据
        /// </summary>
        /// <param name="col"></param>
        /// <param name="city_name"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetWF(string col, string city_name)
        {
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(col);
            FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = builder.Eq("city_name", city_name);

            var result = collection.Find<BsonDocument>(filter).ToList()[0];
            string city = result.GetValue("city_name").ToString();
            string high = result.GetValue("1").ToString().Split(' ')[1].Split('℃')[0];
            string low = result.GetValue("1").ToString().Split(' ')[2].Split('℃')[0];
            string tq = result.GetValue("1").ToString().Split(' ')[2].Split('℃')[1];

            Dictionary<string, string> myDictionary = new Dictionary<string, string>();
            myDictionary.Add("城市名称", result.GetValue("city_name").ToString());
            myDictionary.Add("天气概要", tq);
            myDictionary.Add("平均气温", result.GetValue("温度").ToString());
            myDictionary.Add("最高气温", high);
            myDictionary.Add("最低气温", low);
            myDictionary.Add("空气湿度", result.GetValue("湿度").ToString());
            myDictionary.Add("风力信息", result.GetValue("风力").ToString());
            myDictionary.Add("风向信息", result.GetValue("风向").ToString());
            myDictionary.Add("紫外线强度", result.GetValue("紫外线强度").ToString());
            myDictionary.Add("舒适度", result.GetValue("舒适度").ToString());
            myDictionary.Add("感冒指数", result.GetValue("感冒指数").ToString());
            myDictionary.Add("旅游指数", result.GetValue("旅游指数").ToString());
            myDictionary.Add("穿衣指数", result.GetValue("穿衣指数").ToString());
            myDictionary.Add("雨伞指数", result.GetValue("雨伞指数").ToString());
            myDictionary.Add("运动指数", result.GetValue("运动指数").ToString());
            myDictionary.Add("晨练指数", result.GetValue("晨练指数").ToString());
            myDictionary.Add("晾晒指数", result.GetValue("晾晒指数").ToString());
            myDictionary.Add("洗车指数", result.GetValue("洗车指数").ToString());
            myDictionary.Add("约会指数", result.GetValue("约会指数").ToString());
            myDictionary.Add("预警信息", result.GetValue("预警信息").ToString());
            myDictionary.Add("2", result.GetValue("2").ToString());
            myDictionary.Add("3", result.GetValue("3").ToString());
            myDictionary.Add("4", result.GetValue("4").ToString());
            myDictionary.Add("5", result.GetValue("5").ToString());

            return myDictionary;
        }
    }
}