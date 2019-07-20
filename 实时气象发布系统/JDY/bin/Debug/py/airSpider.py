# -*- coding:gbk -*-
# Author: Hao He
import requests
import base64
import pymongo
import os
from pyquery import PyQuery as pq
from configs import *


#设计的类
"""
class AQI:
    # 城市综合AQI
    def __init__(self, PRIMARYPOLLUTANT, AQI, QUALITY, city_code=None, TIMEPOINT=None):
        self.city_code = city_code
        self.TIMEPOINT = TIMEPOINT
        self.PRIMARYPOLLUTANT = PRIMARYPOLLUTANT
        self.AQI = AQI
        self.QUALITY = QUALITY


class AirAQI:
    # 城市空气污染物AQI
    def __init__(self, ISO2=None, INO2=None, ICO=None, IO3=None, IPM10=None, IPM2_5=None,
                 city_code=None, TIMEPOINT=None, TIMEPOINT1=None):
        self.city_code = city_code
        self.TIMEPOINT = TIMEPOINT
        self.TIMEPOINT1 = TIMEPOINT1
        self.ISO2 =ISO2
        self.INO2 =INO2
        self.ICO =ICO
        self.IO3 =IO3
        self.IPM10 =IPM10
        self.IPM2_5 =IPM2_5


class AirCon:
    # 城市空气污染物浓度
    def __init__(self, SO2=None, NO2=None, CO=None, O3=None, PM10=None,
                 PM2_5=None, city_code=None, TIMEPOINT1=None):
        self.city_code = city_code
        self.TIMEPOINT1 = TIMEPOINT1
        self.SO2 = SO2
        self.NO2 = NO2
        self.CO = CO
        self.O3 = O3
        self.PM10 = PM10
        self.PM2_5 = PM2_5


class AirQuality(AirAQI, AirCon):
    # 城市空气质量综合评价及建议
    def __init__(self,ISO2=None,INO2=None,ICO=None,IO3=None,IPM10=None,IPM2_5=None,
                 SO2=None, NO2=None, CO=None, O3=None, PM10=None, PM2_5=None,
                 AQI=None, level=None, QUALITY=None,  rank=None, PRIMARYPOLLUTANT=None, health_effects=None,
                 advise=None, city_code=None, TIMEPOINT=None, TIMEPOINT1=None):
        AirAQI.__init__(self,ISO2,INO2,ICO,IO3,IPM10,IPM2_5)
        AirCon.__init__(self, SO2, NO2, CO, O3, PM10, PM2_5=PM2_5)
        self.city_code = city_code
        self.TIMEPOINT = TIMEPOINT
        self.TIMEPOINT1 = TIMEPOINT1
        self.AQI = AQI
        self.level = level
        self.QUALITY = QUALITY
        self.rank = rank
        self.PRIMARYPOLLUTANT = PRIMARYPOLLUTANT
        self.health_effects = health_effects
        self.advise = advise    
"""


class CodeSpider:
    """获取陕西省各城市编码"""
    def __init__(self):
        self.city_code = []

    def code_spider(self):
        response = requests.get(code_url, headers=headers, proxies=proxies, timeout=15)
        doc = pq(response.text)
        items = doc('#aspnetForm > div.box > div.top > div > ul > li:nth-child(3) > ul >li').items()
        for item in items:
            self.city_code.append(item.find('a').attr('onclick').split('\'')[3].encode())

    @staticmethod
    def city_encode(city_code):
        return base64.b64encode(city_code)


class Spiders:
    """数据爬虫"""
    def __init__(self):
        # 县区编码
        self.county_code = 'http://113.140.66.226:8024/sxAQIWeb/ashx/StatisticsData.ashx?cityCode={city_code}&type={type}'
        # 城市空气质量24小时AQI
        self.city_24AQI_base_url = 'http://113.140.66.226:8024/sxAQIWeb/ashx/getCity_24AQI.ashx?cityName={city_code}'
        # 城市空气质量24小时各类污染物AQI
        self.city_24AQI_pollutant_base_url = 'http://113.140.66.226:8024/sxAQIWeb/ashx/getDistrict_24IAQI.ashx?cityCode={city_code}'
        # 城市空气质量24小时各类污染物浓度
        self.city_24con_pollutant_base_url = 'http://113.140.66.226:8024/sxAQIWeb/ashx/getDistrict_24Nd.ashx?cityCode={city_code}'
        # 城市空气质量7日AQI
        self.city_7AQI_pollutant_base_url = 'http://113.140.66.226:8024/sxAQIWeb/ashx/getCity_7DayAQI.ashx?cityName={city_code}'
        self.day_num = 1

    def hour24_spider(self, city_code):
        """爬取24小时空气质量数据并存入MongoDB"""
        response = requests.get(self.city_24AQI_base_url.format(city_code=city_code), headers=headers, proxies=proxies, timeout=15)
        response_dicts = response.json()
        for data in response_dicts:
            data['TIMEPOINT'], data['TIMEPOINT1'] = data['TIMEPOINT1'], data['TIMEPOINT']
            # print(data)
            self.save_to_mongo(data, city_code)

        response2 = requests.get(self.city_24AQI_pollutant_base_url.format(city_code=city_code), headers=headers, proxies=proxies, timeout=15)
        response2_dicts = response2.json()
        for data in response2_dicts:
            # print(data)
            self.save_to_mongo(data, city_code)

        response3 = requests.get(self.city_24con_pollutant_base_url.format(city_code=city_code), headers=headers, proxies=proxies, timeout=15)
        response3_dicts = response3.json()
        for data in response3_dicts:
            # print(data)
            self.save_to_mongo(data, city_code)

    def day7_spider(self, city_code):
        """爬取7日空气质量数据并存入MongoDB"""
        response = requests.get(self.city_7AQI_pollutant_base_url.format(city_code=city_code), headers=headers, proxies=proxies, timeout=15)
        response_dicts = response.json()
        for data in response_dicts:
            data['city_code'] = city_code
            data['day_num'] = self.day_num
            self.save_to_clear_mongo(data, city_code)
            self.day_num += 1

    def save_to_mongo(self, data, city_code):
        """更新MongoDB数据，存数据"""
        client = pymongo.MongoClient(host=host, port=port)
        db = client[CLIENT]
        collection = db[COLLECTION1]
        condition = {'city_code': city_code, 'TIMEPOINT1': data['TIMEPOINT1']}
        collection.update_one(condition, {'$set': data}, True)

    def save_to_clear_mongo(self, data, city_code):
        """清理MongoDB，并存入数据"""
        client = pymongo.MongoClient(host=host, port=port)
        db = client[CLIENT]
        collection = db[COLLECTION2]
        collection.delete_many({'city_code': city_code, 'day_num': self.day_num})
        collection.insert_one(data)

    @staticmethod
    def export_all_mongo():
        """MongoDB数据集合导出为CSV文件"""
        os.system('mongoexport.exe --type=csv -d RealTimeWeather -c day7 -f city_code,day_num,TIMEPOINT,AQI,'
                  'PRIMARYPOLLUTANT,QUALITY -o./day7.csv')
        os.system('mongoexport.exe --type=csv -d RealTimeWeather -c hour24 -f city_code,TIMEPOINT,TIMEPOINT1,AQI,'
                  'PRIMARYPOLLUTANT,QUALITY,ISO2,INO2,ICO,IO3,IPM10,IPM2_5,SO2,NO2,CO,O3,PM10,PM2_5  -o./hour.csv')

    @staticmethod
    def change_encode():
        """爬取到的数据中文存在乱码，使用此静态方法将csv文件该文gbk编码"""
        fns = (fn for fn in os.listdir() if fn.endswith('.csv'))
        for fn in fns:
            try:
                with open(fn, encoding='gbk') as fp:
                    fp.read()
            except:
                with open(fn, encoding='utf8') as fp1:
                    with open('temp.csv', 'w', encoding='gbk') as fp2:
                        fp2.write(fp1.read())
                os.remove(fn)
                os.rename('temp.csv', fn)


# if __name__ == '__main__':
#     code_spider = CodeSpider()
#     code_spider.code_spider()
#     for city_code in code_spider.city_code:
#         city_code = str(city_code, encoding="utf-8")
#         main_spide = Spiders()
#         main_spide.hour24_spider(city_code)
#         main_spide.day7_spider(city_code)
    # os.system('mongoexport.exe --type=csv -d RealTimeWeather -c day7 -f city_code,day_num,TIMEPOINT,AQI,'
    #           'PRIMARYPOLLUTANT,QUALITY -o./day7.csv')
    # os.system('mongoexport.exe --type=csv -d RealTimeWeather -c hour24 -f city_code,TIMEPOINT,TIMEPOINT1,AQI,'
    #           'PRIMARYPOLLUTANT,QUALITY,ISO2,INO2,ICO,IO3,IPM10,IPM2_5,SO2,NO2,CO,O3,PM10,PM2_5  -o./hour.csv')
    # main_spide.change_encode()
