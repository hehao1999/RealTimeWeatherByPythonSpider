# -*- coding:gbk -*-
# Author: Hao He

""""
    ---数据引擎入口
    ---采用三线程分别运行三个爬虫程序
    ---程序中仍留有少量为利用的程序可为将来应用功能扩展做准备
    ---可能能用到的函数/类功能如下：
            mongoDB数据库集合导出csv类方法
            csv乱码处理——编码转换类方法
            区县城市代码爬取——url
            空气质量数据类——多继承
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
    city_names = ['榆林', '延安', '宝鸡', '汉中', '安康', '商州', '西安', '渭南', '铜川', '咸阳']
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
