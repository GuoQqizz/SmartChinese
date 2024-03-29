// <copyright file="FormTestTest.cs">Copyright ©  2019</copyright>
using System;
using AbhsChinese.Test;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbhsChinese.Test.Tests
{
    /// <summary>此类包含 FormTest 的参数化单元测试</summary>
    [PexClass(typeof(FormTest))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class FormTestTest
    {
        /// <summary>测试 AddFormShouldSuccess() 的存根</summary>
        [PexMethod]
        public void AddFormShouldSuccessTest([PexAssumeUnderTest]FormTest target)
        {
            target.AddFormShouldSuccess();
            // TODO: 将断言添加到 方法 FormTestTest.AddFormShouldSuccessTest(FormTest)
        }
    }
}
