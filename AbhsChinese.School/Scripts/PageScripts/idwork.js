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

/*
*基于  Twitter snowflake算法 实现全局唯一ID
*       1bit( 0 无意义，表示正数)|41bit(当前时间戳，到毫秒)|5bit(当前机房Id)|5bit(当前机器Id)|12bit(当前机房当前机器当前毫秒内生成的Id)
*       总共64bit
*唯一id生成工具 当前时间戳(到毫秒)+4位自增数 共17位
*由于js位运算支持32位，所以简化
*
*
*/
_idWork = function (sequence) {
    this.Sequence = sequence || 0;
    this.SequenceBit = 12;
    this.CurrentStamp = this.genTimeStamp();
    this.SequenceMask = -1 ^ (-1 << this.SequenceBit);
    this.CurrentStampLeftShift = 4;
    //SequenceBit 与 CurrentStampLeftShift 有对应关系  1<<SequenceBit位是CurrentStampLeftShift长度数字
}

_idWork.prototype.getNext = function () {

    var result = 0;
    var timeStamp = this.genTimeStamp();
    if (timeStamp == this.CurrentStamp) {
        //# 这个意思是说一个毫秒内最多只能有4096个数字，无论你传递多少进来，
        //# 这个位运算保证始终就是在4096这个范围内，避免你自己传递个sequence超过了4096这个范围
        //# self.sequence_mask = 4095  4095&4096 = 0
        this.Sequence = (this.Sequence + 1) & this.SequenceMask;
        if (this.Sequence == 0) {
            this.CurrentStamp = this.getNextMill();
        }
    }
    else {
        this.Sequence = 0;
        this.CurrentStamp = this.getNextMill();
    }
    //js 大数相加会丢失精度,改用string拼接实现
    result = this.CurrentStamp.toString() + this.Sequence.toString().padStart(4, '0');
    return result;


}
_idWork.prototype.genTimeStamp = function () {
    return new Date().getTime();
}
_idWork.prototype.getNextMill = function () {
    var tmp = this.genTimeStamp();
    while (tmp <= this.CurrentStamp) {
        tmp = this.genTimeStamp();
    }
    this.CurrentStamp = tmp;
    return tmp;
}




function _idWorkFunc() {
    let seq = 0;
    let lastStamp = genTimeStamp();
    let seqBit = 12;
    let stampLeftShift = 4;
    let seqMask = -1 ^ (-1 << seqBit);
    function genTimeStamp() {
        return new Date().getTime();
    }
    function getNextMill() {
        var tmp = genTimeStamp();
        while (tmp <= lastStamp) {
            tmp = genTimeStamp();
        }
        lastStamp = tmp;
        return tmp;
    }
    return function () {
        let currentStamp = genTimeStamp();
        if (currentStamp == lastStamp) {
            seq = (seq + 1) & seqMask;
            if (seq == 0) {
                currentStamp = getNextMill();
            }
        } else {
            seq = 0;
            currentStamp = getNextMill();
        }
        //result = _BigNumSum(currentStamp * Math.pow(10, stampLeftShift), seq);
        result = currentStamp.toString() + seq.padStart(4, '0');
        return result;
    }
}

//对象实现 调用 idWork.getNext();
window.idWork = new _idWork();
//闭包实现  调用 idWork2();
window.idWork2 = _idWorkFunc();

window.__testIdWork = function () {
    var arr = [];
    var arr2 = [];
    for (var i = 0; i < 100; i++) {
        arr.push(idWork.getNext());
        arr2.push(idWork2());
        if (i % 7 == 0) {
            idWork.getNextMill();
        }
    }
    console.log(arr);
    console.log(arr2);
}


//大数相加
function _BigNumSum(a, b) {
    a = a.toString();
    b = b.toString();
    var res = '', c = 0;
    a = a.split('');
    b = b.split('');
    while (a.length || b.length || c) {
        //~按位取反 ~~转化位数字 非数字会转为0
        c += ~~a.pop() + ~~b.pop();
        res = c % 10 + res;
        c = c > 9;
    }
    return res.replace(/^0+/, '');

}
