using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expenditure;
using Expenditure.Controllers;
using Expenditure.Models;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Expenditure.Tests.Controllers
{
    [TestClass]
    public class ExpenditureControllerTest
    {
        [TestMethod]
        public void TestIndex()
        {
            var db = new B();
            var controller = new ExpenditureController();

            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(List<B>));
            
        }
    }
}
