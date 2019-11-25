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

const ConvertConstToSelect2 = function (sourceArr) {
    let res = [];
    sourceArr.forEach(function (source) {
        res.push({
            id: source.key,
            text: source.value
        })
    })
}

//该变量表示:经过xx毫秒后页面会进行跳转
const timeToRedirect = 2000;

const SubjectTypeEnum = {
    '选择题': 1,
    '判断题': 2,
    '填空题': 3,
    '选择填空': 4,
    '连线题': 5,
    '主观题': 6,
    '圈点批注标色': 7,
    '圈点批注断句': 8
};


const SubjectStatusEnum = {
    '编辑中': 1, '待校对': 2, '合格': 3, '不合格': 4
};