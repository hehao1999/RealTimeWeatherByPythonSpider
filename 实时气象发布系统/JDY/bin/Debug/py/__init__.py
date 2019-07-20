# -*- coding:gbk -*-
# Author: Hao He

""""
    ---�����������
    ---�������̷ֱ߳����������������
    ---����������������Ϊ���õĳ����Ϊ����Ӧ�ù�����չ��׼��
    ---�������õ��ĺ���/�๦�����£�
            mongoDB���ݿ⼯�ϵ���csv�෽��
            csv���봦��������ת���෽��
            ���س��д�����ȡ����url
            �������������ࡪ����̳�
"""
import threading
from AQForecastSoider import *
from weatherSpider import *
from airSpider import *


def air_run():
    code_spider = CodeSpider()
    code_spider.code_spider()
    for city_code in code_spider.city_code:
        city_code = str(city_code, encoding="utf-8")
        main_spide = Spiders()
        main_spide.hour24_spider(city_code)
        main_spide.day7_spider(city_code)


def weather_run():
    city_names = ['����', '�Ӱ�', '����', '����', '����', '����', '����', 'μ��', 'ͭ��', '����']
    for city in city_names:
        weather_spider = WeatherSpider(city_name=city)
        weather_spider.weather_spider()
        weather_spider.parser_xml()
        weather_spider.save_to_mongo(weather_spider.data)


def AQF_run():
    AQF()


if __name__ == '__main__':
    t1 = threading.Thread(target=air_run())
    t2 = threading.Thread(target=weather_run())
    t3 = threading.Thread(target=AQF_run())
    t1.start()
    t2.start()
    t3.start()
    t1.join()
    t2.join()
    t3.join()
