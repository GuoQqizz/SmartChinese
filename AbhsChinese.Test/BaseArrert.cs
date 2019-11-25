using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Test
{
    public class BaseArrert
    {
        public void BaseAssertList<T>(IList<T> list)
        {
            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);
        }

        public void BaseAssertObj(object o)
        {
            Assert.IsNotNull(o);
        }
    }
}
