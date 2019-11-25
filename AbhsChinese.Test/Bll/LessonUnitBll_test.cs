using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbhsChinese.Bll;
using AbhsChinese.Domain.Dto.Response;
using System.Collections.Generic;
using AbhsChinese.Domain.JsonEntity.UnitStep;

namespace AbhsChinese.Test.Bll
{
    [TestClass]
    public class LessonUnitBll_test
    {
        [TestMethod]
        public void TestMethod_Insert()
        {
            LessonUnitBll bll = new LessonUnitBll();
            for (int i = 0; i < 10; i++)
            {
                DtoLessonUnit unit = new DtoLessonUnit();
                unit.CourseId = 9999;
                unit.LessonId = 9999;
                unit.Index = i;
                unit.Name = $"第{i + 1}页";
                unit.Screenshot = "";
                unit.Status = 0;
                unit.Steps = new List<Step>();
                unit.Steps.Add(new Step
                {
                    id = 0,
                    StepNum = 0,
                    Actions = new List<StepAction>()
                        {
                            new SetTitle() { Actionid="asdfgh", Text="test", Align="left",Color="#000000", ActionNum=0, InType="none", Size="big",X=102.3435,Y=123.2342 }
                        }
                });
                bll.Add(unit);
            }
        }

        [TestMethod]
        public void TestMethod_Update()
        {
            LessonUnitBll bll = new LessonUnitBll();

            DtoLessonUnit unit = new DtoLessonUnit();
            unit.Id = 10000;
            unit.CourseId = 9999;
            unit.LessonId = 9999;
            unit.Index = 100;
            unit.Name = $"测试内容了";
            unit.Screenshot = "testurl";
            unit.Status = 0;
            unit.Steps = new List<Step>();
            unit.Steps.Add(new Step
            {
                id = 0,
                StepNum = 0,
                Actions = new List<StepAction>()
                {
                    new SetTitle() { Actionid="asdfgh", Text="test", Align="left",Color="#ffffff", ActionNum=0, InType="none", Size="small",X=102.3435,Y=123.2342 }
                }
            });
            bll.Update(unit);
        }

        [TestMethod]
        public void TestMethod_SelectSteps()
        {

            LessonUnitBll bll = new LessonUnitBll();
            var s = bll.SelectUnit(10000,1);
        }
        [TestMethod]
        public void TestMethod_SelectUnits()
        {
            LessonUnitBll bll = new LessonUnitBll();
            var s = bll.SelectUnitsByLesson(9999,1);

        }

        [TestMethod]
        public void TestMethod_MoveUnit()
        {
            LessonUnitBll bll = new LessonUnitBll();
            var unit = bll.SelectUnit(10000,1);
            bll.MoveUnit(10000, 5);
        }

    }
}
