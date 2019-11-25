; (function extend() {
    //扩展string中trim去除空格方法
    if (!String.prototype.trim) {
        Object.defineProperty(String.prototype, "trim", {
            value: function trim(char) {
                char = char || " ";
                var rl = new RegExp("^" + char + "+");
                var rr = new RegExp(char + "+$");
                return this.replace(rl, "").replace(rr, "");
            }
        });
    }
    //扩展string中trimStart去除右侧
    if (!String.prototype.trimLeft) {
        Object.defineProperty(String.prototype, "trimLeft", {
            value: function trimLeft(char) {
                char = char || " ";
                var rl = new RegExp("^" + char + "+");
                return this.replace(rl, "");
            }
        });
        Object.defineProperty(String.prototype, "trimStart", {
            value: this.trimLeft
        });
        
    }
    //扩展string中trimEnd去除右侧空格
    if (!String.prototype.trimRight) {
        Object.defineProperty(String.prototype, "trimRight", {
            value: function trimRight(char) {
                char = char || " ";
                var rr = new RegExp(char + "+$");
                return this.replace(rr, "");
            }
        });
        Object.defineProperty(String.prototype, "trimEnd", {
            value: this.trimRight
        });
    }
    //左侧补充字符
    if (!String.prototype.padStart) {
        Object.defineProperty(String.prototype, "padStart", {
            value: function padStart(targetLength, padString) {
                targetLength = targetLength >> 0; //floor if number or convert non-number to 0;
                padString = String((typeof padString !== 'undefined' ? padString : ' '));
                if (this.length > targetLength) {
                    return String(this);
                }
                else {
                    targetLength = targetLength - this.length;
                    if (targetLength > padString.length) {
                        padString += padString.repeat(targetLength / padString.length); //append to original to ensure we are longer than needed
                    }
                    return padString.slice(0, targetLength) + String(this);
                }
            }
        });
    }

    //右侧补充字符
    if (!String.prototype.padEnd) {
        Object.defineProperty(String.prototype, "padEnd", {
            value: function padEnd(targetLength, padString) {
                targetLength = targetLength >> 0; //floor if number or convert non-number to 0;
                padString = String((typeof padString !== 'undefined' ? padString : ''));
                if (this.length > targetLength) {
                    return String(this);
                }
                else {
                    targetLength = targetLength - this.length;
                    if (targetLength > padString.length) {
                        padString += padString.repeat(targetLength / padString.length); //append to original to ensure we are longer than needed
                    }
                    return String(this) + padString.slice(0, targetLength);
                }
            }
        });
    }
    //字符串缩略函数
    if (!String.prototype.ellipsis) {
        Object.defineProperty(String.prototype, "ellipsis", {
            value: function ellipsis(targetLength, str) {
                var _this = String(this);
                if (this.length > targetLength)//如果字符串长度超过了设定长度
                {
                    str = str.substr(0, targetLength);
                    _this = _this.substr(0, targetLength - str.length);
                    _this += str;
                }
                return String(_this);
            }
        });
    }
    //判断数组中是否包含
    if (!Array.prototype.contains) {
        Object.defineProperty(Array.prototype, "contains", {
            value: function contains(obj) {
                for (var i in this) {
                    if (this[i] == obj) {
                        return true;
                    }
                }
                return false;
            },
           
        });
    }
    //数组去重
    if (!Array.prototype.unique) {
        Array.prototype.unique = function () {
            var $this = this;
            if (!Array.isArray($this)) {
                console.log('type error!')
                return [];
            }
            var array = [];
            for (var i = 0; i < $this.length; i++) {
                if (array.indexOf($this[i]) === -1) {
                    array.push($this[i])
                }
            }
            return array;
        }
    }
    
    //判断属性名称是否包含在对象中的可枚举属性中
    if (!Object.prototype.contains) {
        Object.defineProperty(Object.prototype, "contains", {
            value: function contains(obj) {
                for (var i in this) {
                    if (i == obj) {
                        return true;
                    }
                }
                return false;
            },
            writable: true,
            configurable: true
        })
    }


    //var descriptor = {
    //    configurable: false,//修饰属性(就是下面的属性)是否可以修改
    //    enumerable: false,//该属性是否可以被枚举出来
    //    value: null,//内容值
    //    writable: false,//值是否可以被赋值运算符改变
    //    get: null,//get函数
    //    set: null//set函数
    //};
    ///扩展设置属性方法
    if (!Object.prototype.setAttribute) {
        Object.defineProperty(Object.prototype, "setAttribute", {
            value: function setAttribute(prop, descriptor) {
                Object.defineProperty(this, prop, descriptor);
            }
        })
    }
    //var props ={
    //  "propName":descriptor,
    //  ...
    //};
    //扩展设置多属性方法
    if (!Object.prototype.setAttributes) {
        Object.defineProperty(Object.prototype, "setAttributes", {
            value: function setAttributes(props) {
                Object.defineProperties(this, props);
            }
        })
    }

    if (!Date.prototype.formatter) {
        Object.defineProperty(Date.prototype, "formatter", {
            value: function formatter(str) {
                var o = {
                    "M+": this.getMonth() + 1,
                    "d+": this.getDate(),
                    "H+": this.getHours(),
                    "m+": this.getMinutes(),
                    "s+": this.getSeconds(),
                    "S+": this.getMilliseconds()
                };
                //因为date.getFullYear()出来的结果是number类型的,所以为了让结果变成字符串型，下面有两种方法：
                if (/(y+)/.test(str)) {
                    //第一种：利用字符串连接符“+”给date.getFullYear()+""，加一个空字符串便可以将number类型转换成字符串。
                    str = str.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
                }
                for (var k in o) {
                    if (new RegExp("(" + k + ")").test(str)) {
                        //第二种：使用String()类型进行强制数据类型转换String(date.getFullYear())，这种更容易理解。
                        str = str.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(String(o[k]).length)));
                    }
                }
                return str;
            }
        });
    }
})();