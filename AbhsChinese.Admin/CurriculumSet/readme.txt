html2canvas alioss 跨域问题：
	方案1:cdn缓存导致配置的跨域header可能配置失效,在加载图片时候给url参数加上随机数(2535行)
	方案2:代理 配置proxy,地址为本地server 接受参数为url(图片地址),responseType(返回数据格式,blob等)
