新增beforeSend 属性，用户ajax请求之前自定义判断 ，参数xhr 
示例：
	 beforeSend:function(e){
                    console.log(e);
                    e.abort(); //或者 return false;
                },