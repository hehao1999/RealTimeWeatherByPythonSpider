# -*- coding:gbk -*-
# Author: Hao He

import requests
from pyquery import PyQuery as pq
from configs import *


def AQF():
    """ͨ����ҳ��ȡ��������Ԥ����Ϣ������txt"""
    response = requests.get(code_url, headers=headers, proxies=proxies)
    doc = pq(response.text)
    forecast = '     ' + pq(doc).find('#ctl00_ContentPlaceHolder1_labInfoPre').text().replace('\n', '')
    with open('AQF.txt', 'w', encoding='utf-8') as f:
        f.write(forecast)


# AQF()
