using Microsoft.VisualStudio.TestTools.UnitTesting;
using RVT_AdministratorAPI.Controllers.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace RVT_AdministratorAPI.Controllers.Users.Tests
{
    [TestClass()]
    public class ResultsControllerTests
    {
        [TestMethod()]
        public void StatisticsAllTest()
        {
            var controller = new ResultsController();
            var response=controller.StatisticsAll("1");
            Assert.IsNotNull(response);
        }
    }
}