# -*- coding:gbk -*-
# Author: Hao He
import requests
import pymongo
from bs4 import BeautifulSoup
from configs import *


class WeatherSpider:
    """��ȡ����Ԥ��"""
    def __init__(self, city_name):
        self.city_name = city_name
        self.str_xml = ''
        self.data = dict()

    def weather_spider(self):
        """��ȡ�ӿڷ��ص�XML����"""
        response = requests.get(weather_url.format(city_name=self.city_name), headers=headers, proxies=proxies, timeout=15)
        self.str_xml = response.text

    def parser_xml(self):
        """����XML�ļ�"""
        # ���ջ���
        soup = BeautifulSoup(self.str_xml, 'lxml')
        wendu = soup.find('wendu').text
        shidu = soup.find('shidu').text
        fengxiang = soup.find('fengxiang').text
        self.data['�¶�'] = wendu
        self.data['ʪ��'] = shidu
        self.data['����'] = fengxiang

        # Ԥ������
        weathers = soup.find('forecast').find_all('weather')
        for i, weather in enumerate(weathers, start=1):
            self.data[str(i)] = weather.text.replace('\n', ' ')

        # ָ����Ϣ
        zhishus = soup.find_all('zhishu')
        for zhishu in zhishus:
            zhishu_name = zhishu.find('name').text
            zhishu_info = zhishu.find('value').text + ' ' + zhishu.find('detail').text
            self.data[zhishu_name] = zhishu_info

        # Ԥ����Ϣ������
        str_xml = self.str_xml.replace('<![CDATA[', '').replace(']]>', '').replace(' ', '')
        soup = BeautifulSoup(str_xml , 'lxml')
        fengli = soup.find('fengli').text
        self.data['����'] = fengli
        try:
            info = ''
            tips = soup.find('alarm').text.replace('\n', ' ').split(' ')[12:-12]
            for tip in tips:
                info += tip
            self.data['Ԥ����Ϣ'] = info
        except Exception:
            self.data['Ԥ����Ϣ'] = '0'

    def save_to_mongo(self, data):
        """�����������"""
        client = pymongo.MongoClient(host=host, port=port)
        db = client[CLIENT]
        collection = db[COLLECTION3]
        condition = {'city_name': self.city_name}
        collection.update_one(condition, {'$set': data}, True)


# weather_spider = WeatherSpider(city_name='����')
# weather_spider.weather_spider()
# weather_spider.parser_xml()
# weather_spider.save_to_mongo(weather_spider.data)
