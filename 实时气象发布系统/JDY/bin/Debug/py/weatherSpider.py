# -*- coding:gbk -*-
# Author: Hao He
import requests
import pymongo
from bs4 import BeautifulSoup
from configs import *


class WeatherSpider:
    """爬取天气预报"""
    def __init__(self, city_name):
        self.city_name = city_name
        self.str_xml = ''
        self.data = dict()

    def weather_spider(self):
        """获取接口返回的XML数据"""
        response = requests.get(weather_url.format(city_name=self.city_name), headers=headers, proxies=proxies, timeout=15)
        self.str_xml = response.text

    def parser_xml(self):
        """解析XML文件"""
        # 今日基础
        soup = BeautifulSoup(self.str_xml, 'lxml')
        wendu = soup.find('wendu').text
        shidu = soup.find('shidu').text
        fengxiang = soup.find('fengxiang').text
        self.data['温度'] = wendu
        self.data['湿度'] = shidu
        self.data['风向'] = fengxiang

        # 预报数据
        weathers = soup.find('forecast').find_all('weather')
        for i, weather in enumerate(weathers, start=1):
            self.data[str(i)] = weather.text.replace('\n', ' ')

        # 指数信息
        zhishus = soup.find_all('zhishu')
        for zhishu in zhishus:
            zhishu_name = zhishu.find('name').text
            zhishu_info = zhishu.find('value').text + ' ' + zhishu.find('detail').text
            self.data[zhishu_name] = zhishu_info

        # 预警信息、风力
        str_xml = self.str_xml.replace('<![CDATA[', '').replace(']]>', '').replace(' ', '')
        soup = BeautifulSoup(str_xml , 'lxml')
        fengli = soup.find('fengli').text
        self.data['风力'] = fengli
        try:
            info = ''
            tips = soup.find('alarm').text.replace('\n', ' ').split(' ')[12:-12]
            for tip in tips:
                info += tip
            self.data['预警信息'] = info
        except Exception:
            self.data['预警信息'] = '0'

    def save_to_mongo(self, data):
        """天气数据入库"""
        client = pymongo.MongoClient(host=host, port=port)
        db = client[CLIENT]
        collection = db[COLLECTION3]
        condition = {'city_name': self.city_name}
        collection.update_one(condition, {'$set': data}, True)


# weather_spider = WeatherSpider(city_name='汉中')
# weather_spider.weather_spider()
# weather_spider.parser_xml()
# weather_spider.save_to_mongo(weather_spider.data)
