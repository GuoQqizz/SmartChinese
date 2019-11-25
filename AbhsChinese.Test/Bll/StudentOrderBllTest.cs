using AbhsChinese.Bll;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Test.Bll
{
    [TestClass]
    public class StudentOrderBllTest
    {
        [TestMethod]
        public void PayOrder()
        {
            decimal amount = 0;
            string orderNo = "";
            StudentOrderBll bll = new StudentOrderBll();
            List<Task> ts = new List<Task>();
            bll.OrderPay(10002, 10002, new int[2] { 10000, 10002 }, CashPayTypeEnum.微信, out amount, out orderNo);
            for (int index = 10101; index <= 10150; index++)
            {
                //if (index % 2 == 0)
                //{
                //    ts.Add(Task.Factory.StartNew<int>(() =>
                //    {
                //        return bll.OrderPay(10000, 10000, new int[3] { 10000, 10001, 10002 }, CashPayTypeEnum.微信);
                //    }));
                //}
                //else
                //{
                var x = index;
                ts.Add(Task.Factory.StartNew(() =>
                {
                    bll.OrderPay(x, x, new int[3] { 10000, 10001, 10002 }, CashPayTypeEnum.微信, out amount, out orderNo);
                }));
                //}
            }
            Task.WaitAll(ts.ToArray());
            //bll.OrderPay(10000, 10000, new int[3] { 10000, 10001, 10002 }, CashPayTypeEnum.微信);
        }

        [TestMethod]
        public void PatOrderCallBack()
        {
            decimal amount = 0;
            string orderNo = "";
            StudentOrderBll bll = new StudentOrderBll();
            //bll.OrderPayCallback("1190917134811234", CashPayTypeEnum.微信,2800);
            List<Task> ts = new List<Task>();
           // var x=bll.OrderPay(10101, 10101, new int[3] { 10000, 10001, 10002 }, CashPayTypeEnum.微信,out amount,out orderNo);
            for (int index = 10101; index <= 10150; index++)
            {
                //if (index % 2 == 0)
                //{
                //    ts.Add(Task.Factory.StartNew<int>(() =>
                //    {
                //        return bll.OrderPay(10000, 10000, new int[3] { 10000, 10001, 10002 }, CashPayTypeEnum.微信);
                //    }));
                //}
                //else
                //{
                var x = index;
                ts.Add(Task.Factory.StartNew<decimal>(() =>
                {
                    return 0;
                    //return bll.OrderPayCallback(x, CashPayTypeEnum.微信,2700);
                }));
                //}
            }
            Task.WaitAll(ts.ToArray());

            //StudentOrderBll bll = new StudentOrderBll();
            //bll.OrderPayCallback(10003, CashPayTypeEnum.微信);
        }

        [TestMethod]
        public void MakeOrderTest()
        {
            StudentOrderBll bll = new StudentOrderBll();
            bll.MakeOrder(10013, 10018);
        }

        [TestMethod]
        public void OrderPayTest()
        {
            StudentOrderBll bll = new StudentOrderBll();
            decimal amount = 0;
            string orderNo = "";
            bll.OrderPay(10225, 10013, new int[1] { 10020 }, CashPayTypeEnum.微信, out amount, out orderNo);
        }

        [TestMethod]
        public void OrderPayCallbackTest()
        {
            StudentOrderBll bll = new StudentOrderBll();
            bll.OrderPayCallback("611003114584610026211", "1004400740201409030005092168", CashPayTypeEnum.微信, 500);
        }

        [TestMethod]
        public void CancelOrderTest()
        {
            StudentOrderBll bll = new StudentOrderBll();
            bll.CancelOrder(10208);
            //string no=bll.GenerateOrderNo(10000, 10001);
            //Yw_StudentOrder order = bll.GetStudentOrder("1190917134810000");

        }

        [TestMethod]
        public void CancelOrderAutoTest()
        {
            StudentOrderBll bll = new StudentOrderBll();
            bll.CancelOrderAuto();
        }

        [TestMethod]
        public void OrderQueryTest()
        {
            StudentOrderBll bll = new StudentOrderBll();
            int orderId = 10227;
            int studentId = 10001;
            bll.OrderQuery(orderId, CashPayTypeEnum.微信, studentId);
        }
    }
}
