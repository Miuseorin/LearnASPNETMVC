using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.Controllers;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Transactions;
namespace WebApplication1.Tests.Controllers
{
    [TestClass]
    public class VLTeaTest
    {
        [TestMethod]
        public void TestIndex()
        {
            var db = new CS4PEEntities();
            var controller = new VLTeaController();
            var result = controller.Index();

            var view = result as ViewResult;
            Assert.IsNotNull(view);
            var model = view.Model as List<BubleTea>;
            Assert.IsNotNull(model);
            Assert.AreEqual(db.BubleTeas.Count(), model.Count);
        }
        [TestMethod]
        public void TestCreateImg()
        {
            var controller = new VLTeaController();
            var result = controller.Create();

            var view = result as ViewResult;
            Assert.IsNotNull(view);
        }

        [TestMethod]
        public void TestCreateP()
        {
            var model = new BubleTea
            {
                Name="Hoa huong duong",
                Topping ="tran chau,banh flan",
                Price =0,
            };
            var db = new CS4PEEntities();
            var controller = new VLTeaController();
            using (var scope = new TransactionScope())
            {
                var result = controller.Create(model);
                var view = result as ViewResult;
                Assert.IsNotNull(view);
                Assert.IsInstanceOfType(view.Model, typeof(BubleTea));
                Assert.AreEqual(Resource1.PRICE_LESS_0,
                controller.ViewData.ModelState["Price"].Errors[0].ErrorMessage);

                model.Price = 26000;
                controller = new VLTeaController();
                result = controller.Create(model);
                var redirect = result as RedirectToRouteResult;
                Assert.IsNotNull(redirect);
                Assert.AreEqual("Index", redirect.RouteValues["action"]);
                var item = db.BubleTeas.Find(model.id);
                Assert.IsNotNull(item);
                Assert.AreEqual(model.Name, item.Name);
                Assert.AreEqual(model.Topping, item.Topping);
                Assert.AreEqual(model.Price, item.Price);
            }           
        }

        [TestMethod]
        public void TestDel()
        {
            var db = new CS4PEEntities();
            var controller = new VLTeaController();

            var result = controller.Delete(0);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));

            using (var scope = new TransactionScope())
            {
                var model = db.BubleTeas.AsNoTracking().First();
                result = controller.Delete(model.id);
                var redirect = result as RedirectToRouteResult;
                Assert.IsNotNull(redirect);
                Assert.AreEqual("Index", redirect.RouteValues["action"]);
                var item = db.BubleTeas.Find(model.id);
                Assert.IsNull(item);
            }
        }

        [TestMethod]
        public void TestEditG()
        {
            var db = new CS4PEEntities();
            var controller = new VLTeaController();


            var item = db.BubleTeas.First();
            var result = controller.Edit(item.id);
            var view = result as ViewResult;
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
            Assert.IsNotNull(view);
            var model = view.Model as BubleTea;
            Assert.IsNotNull(model);
            Assert.AreEqual(model.Name, item.id);
            Assert.AreEqual(model.Name, item.Name);
            Assert.AreEqual(model.Topping, item.Topping);
            Assert.AreEqual(model.Price, item.Price);
        }

        [TestMethod]
        public void TestEditP()
        {
            var db = new CS4PEEntities();
            var model = new BubleTea
            {
                id = db.BubleTeas.AsNoTracking().First().id,
                Name = "Hoa huong duong",
                Topping = "tran chau,banh flan",
                Price = 0,
            };
            var controller = new VLTeaController();
            using (var scope = new TransactionScope())
            {
                var result = controller.Edit(model);
                var view = result as ViewResult;
                Assert.IsNotNull(view);
                Assert.IsInstanceOfType(view.Model, typeof(BubleTea));
                Assert.AreEqual(Resource1.PRICE_LESS_0,
                controller.ViewData.ModelState["Price"].Errors[0].ErrorMessage);

                model.Price = 26000;
                controller = new VLTeaController();
                result = controller.Edit(model);
                var redirect = result as RedirectToRouteResult;
                Assert.IsNotNull(redirect);
                Assert.AreEqual("Index", redirect.RouteValues["action"]);
                var item = db.BubleTeas.Find(model.id);
                Assert.IsNotNull(item);
                Assert.AreEqual(model.Name, item.id);
                Assert.AreEqual(model.Name, item.Name);
                Assert.AreEqual(model.Topping, item.Topping);
                Assert.AreEqual(model.Price, item.Price);
            }
        }




    }
}
