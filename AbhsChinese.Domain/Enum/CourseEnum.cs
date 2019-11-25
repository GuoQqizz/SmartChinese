namespace AbhsChinese.Domain.Enum
{
    public enum CourseCategoryEnum
    {
        同步课 = 1,
        专项课 = 2,
        复习课 = 3,
        整本书阅读课 = 4,
        文学史课 = 5
    }

    public enum CourseStatusEnum
    {
        未定价 = 1,
        待上架 = 2,
        已上架 = 3,
        已下架 = 4,
        已关闭 = 5,
        已删除 = 6
    }
    public enum CourseActionEnum
    {
        定价 = 1,
        上架 = 2,
        下架 = 3,
        关闭 = 4,
        删除 = 5,
        重新打开 = 6,
        添加课程 = 7
    }
}