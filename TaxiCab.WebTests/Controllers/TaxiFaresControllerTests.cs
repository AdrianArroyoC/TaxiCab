using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxiCab.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TaxiCab.Web.Models;

namespace TaxiCab.Web.Controllers.Tests
{
    [TestClass()]
    public class TaxiFaresControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            var controller = new TaxiFaresController();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod()]
        public void DetailsTest()
        {
            var controller = new TaxiFaresController();
            var result = controller.Details(2) as ViewResult;
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod()]
        public void CreateTest()
        {
            var controller = new TaxiFaresController();
            var result = controller.Create() as ViewResult;
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod()]
        public void EditTest()
        {
            var controller = new TaxiFaresController();
            var result = controller.Edit(2) as ViewResult;
            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var controller = new TaxiFaresController();
            var result = controller.Delete(2) as ViewResult;
            Assert.AreEqual("Delete", result.ViewName);
        }
    }
}