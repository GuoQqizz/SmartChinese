
const gradeSourceArr = [
    { key: 1, value: "一年级" },
    { key: 2, value: "二年级" },
    { key: 4, value: "三年级" },
    { key: 8, value: "四年级" },
    { key: 16, value: "五年级" },
    { key: 32, value: "六年级" },
    { key: 64, value: "初一" },
    { key: 128, value: "初二" },
    { key: 256, value: "初三" },
    { key: 512, value: "高一" },
    { key: 1024, value: "高二" },
    { key: 2048, value: "高三" }
]
const statusSourceArr = [
    { key: 1, value: "有效" },
    { key: 2, value: "无效" },
    { key: 3, value: "删除" },

]
const sexSourceArr = [
    { key: 1, value: "男" },
    { key: 2, value: "女" },
]

const schoolStatusArr = [
    { key: 1, value: "正式运行" },
    { key: 2, value: "营建期" },
    { key: 3, value: "合同到期" },
]


const weekDayArr = [
    { key: 1, value: "周一" },
    { key: 2, value: "周二" },
    { key: 3, value: "周三" },
    { key: 4, value: "周四" },
    { key: 5, value: "周五" },
    { key: 6, value: "周六" },
    { key: 7, value: "周日" },
]

const courseTypeArr = [
    { key: 1, value: "同步课" },
    { key: 2, value: "专项课" },
    { key: 3, value: "复习课" },
    { key: 4, value: "整本书阅读课" },
    { key: 5, value: "文学史课" },

]

const classStatusArr = [
    { key: 1, value: "未开始" },
    { key: 2, value: "上课中" },
    { key: 3, value: "已结课" },
]

const studentStatusArr = [
    { key: 1, value: "已绑定校区" },
    { key: 2, value: "在读" },
    { key: 3, value: "不在读" },
]

const studentAccountTypeArr = [
    { key: 1, value: "客户" },
    { key: 2, value: "用户" },

]

const voucherStatusArr = [
    { key: 1, value: "未领完" },
    { key: 2, value: "已领完" },
]

const ConvertConstToSelect2 = function (sourceArr) {
    let res = [];
    sourceArr.forEach(function (source) {
        res.push({
            id: source.key,
            text: source.value
        })
    })
}