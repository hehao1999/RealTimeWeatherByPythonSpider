# -*- coding:utf-8 -*-
# Author: Hao He

# 数据库相关参数
CLIENT = 'RealTimeWeather'
COLLECTION1 = 'hour24'
COLLECTION2 = 'day7'
COLLECTION3 = 'weather'
host = 'localhost'
port = 27017

# 代理设置
proxies = None

# 城市编码
code_url = u'http://113.140.66.226:8024/sxAQIWeb/PageCity.aspx?cityCode=NjEwMTAw'
# 天气接口
weather_url = r'http://wthrcdn.etouch.cn/WeatherApi?city={city_name}'

# 浏览器标识
headers = {
    'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:65.0) Gecko/20100101 Firefox/65.0',
    'Accept': 'text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8',
    'Accept-Language': 'zh-CN,zh;q=0.8,zh-TW;q=0.7,zh-HK;q=0.5,en-US;q=0.3,en;q=0.2',
    'Accept-Encoding': 'gzip, deflate, br',
}
