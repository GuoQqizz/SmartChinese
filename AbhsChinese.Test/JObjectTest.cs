using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Test
{
    [TestClass]
    public class JObjectTest
    {
        [TestMethod]
        public void TestMethod()
        {
            UnitStep s1 = new UnitStep();
            s1.Index = 1;
            s1.Actions.Add(new UnitStepTitleAction() { ActionType = "Title", PosX = 100, PosY = 200, Title = "测试" });
            s1.Actions.Add(new UnitStepBackgroundAction() { ActionType = "BackGround",BackgroundUrl="http://www.baidu.com" });
            UnitStep s2 = new UnitStep();
            s2.Index = 2;
            s2.Actions.Add(new UnitStepTitleAction() { ActionType = "Title", PosX = 100, PosY = 200, Title = "测试" });
            s2.Actions.Add(new UnitStepBackgroundAction() { ActionType = "BackGround", BackgroundUrl = "http://www.baidu.com" });

            UnitStep[] s3 = new UnitStep[2] { s1, s2 };

            string json = JsonConvert.SerializeObject(s3);
            JArray items = JsonConvert.DeserializeObject(json) as JArray;

            List<UnitStep> result = new List<UnitStep>();

            foreach (JObject o in items)
            {
                UnitStep us = new UnitStep();
                us.Index = Convert.ToInt32(o["Index"]);

                string innerJson = o["Actions"].ToString();
                JArray innerItems = JsonConvert.DeserializeObject(innerJson) as JArray;

                foreach (JObject jo in innerItems)
                {
                    if (jo["ActionType"].ToString() == "Title")
                    {
                        UnitStepTitleAction action = JsonConvert.DeserializeObject<UnitStepTitleAction>(jo.ToString());
                        us.Actions.Add(action);
                    }
                    else if (jo["ActionType"].ToString() == "BackGround")
                    {
                        UnitStepBackgroundAction action = JsonConvert.DeserializeObject<UnitStepBackgroundAction>(jo.ToString());
                        us.Actions.Add(action);
                    }
                }
                result.Add(us);
            }
        }
    }

    public class UnitStep
    {
        public UnitStep()
        {
            Actions = new List<UnitStepAction>();
        }

        public int Index
        {
            set;get;
        }

        public List<UnitStepAction> Actions
        {
            set;get;
        }
    }

    public abstract class UnitStepAction
    {
        public string ActionType
        {
            set;get;
        }
    }

    public class UnitStepTitleAction : UnitStepAction
    {
        public UnitStepTitleAction()
        {
            this.ActionType = "Title";
        }
        public string Title
        {
            set;get;
        }

        public int PosX
        {
            set;get;
        }

        public int PosY
        {
            set;get;
        }
    }

    public class UnitStepBackgroundAction : UnitStepAction
    {
        public UnitStepBackgroundAction()
        {
            this.ActionType = "BackGround";
        }
        public string BackgroundUrl
        {
            set;get;
        }
    }
}
