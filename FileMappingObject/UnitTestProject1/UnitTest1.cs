using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            FileMappingObject.Form1 form1 = new FileMappingObject.Form1();
            form1.Dispose();
        }
    }
}
