using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RVT_AdministratorAPI.Controllers.Users;
using RVTLibrary.Models.AuthUser;
using RVTLibrary.Models.UserIdentity;
using RVTLibrary.Models.Vote;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RVT_AdministratorAPI.Controllers.Users.Tests
{
    [TestClass()]
    public class IdentityControllerTests
    {
        //    IdentityController controller;

        //    [TestMethod()]
        //    public async Task RegistrationActTestTrue()
        //    {
        //        controller = new IdentityController();
        //        RegistrationMessage registrationModel = new RegistrationMessage
        //        {
        //            IDNP = "1234567891111",
        //            Name = "Ionas",
        //            Surname = "Cristian",
        //            Gender = "Masculin",
        //            Birth_date = DateTime.Parse("1998-01-17"),
        //            Ip_address = Dns.GetHostName(),
        //            Phone_Number = "01234567899",
        //            Email = "cris.ionas@gmail.com",
        //            Region = "Ialoveni",
        //            RegisterDate = DateTime.Now
        //        };
        //        var response = await controller.RegistrationAct(registrationModel);

        //        Assert.IsTrue(response.Value.Status);
        //    }



        //    [TestMethod()]
        //    public async Task RegistrationActTestFalse()
        //    {
        //        controller = new IdentityController();
        //        RegistrationMessage registrationModel = new RegistrationMessage
        //        {
        //            IDNP = "1234567891111",
        //            Name = "Ionas",
        //            Surname = "Cristian",
        //            Gender = "Masculin",
        //            Birth_date = DateTime.Parse("2012-01-17"),
        //            Ip_address = Dns.GetHostName(),
        //            Phone_Number = "01234567899",
        //            Email = "cris.ionas@gmail.com",
        //            Region = "Chisinau",
        //            RegisterDate = DateTime.Now
        //        };
        //        var response = await controller.RegistrationAct(registrationModel);

        //        Assert.IsFalse(response.Value.Status);
        //    }

        //    [TestMethod()]
        //    public async Task AuthActTestTrue()
        //    {
        //        controller = new IdentityController();
        //        var authModel = new AuthMessage
        //        {
        //            IDNP = "1234567891111",
        //            Ip_address = Dns.GetHostName(),
        //            Time = DateTime.Now,
        //            VnPassword = "eerf8645bfe5663dsgfhdsd"
        //        };

        //        var response = await controller.AuthAct(authModel);

        //        Assert.IsTrue(response.Value.Status);
        //    }

        //    [TestMethod()]
        //    public async Task AuthActTestFalse()
        //    {
        //        controller = new IdentityController();
        //        var authModel = new AuthMessage
        //        {
        //            IDNP = "1234567891111",
        //            Ip_address = Dns.GetHostName(),
        //            Time = DateTime.Now,
        //            VnPassword = "TestPassword"
        //        };

        //        var response = await controller.AuthAct(authModel);
        //        Assert.IsFalse(response.Value.Status);
        //    }

        [TestMethod()]
        public async Task VoteActTestTrue()
        {


            var model = new VoteMessage()
            {
                IDVN = "7a51298ecac0dbe8d63935301327e53b",
                Party = 2

            };
            var jsonStr = JsonConvert.SerializeObject(model);

        }

        //    [TestMethod()]
        //    public async Task VoteActTestFalse()
        //    {
        //        controller = new IdentityController();

        //        var authModel = new AuthMessage
        //        {
        //            IDNP = "1234567891111",
        //            Ip_address = Dns.GetHostName(),
        //            Time = DateTime.Now,
        //            VnPassword = "eerf8645bfe5663dsgfhdsd"
        //        };

        //        var responseAuth = await controller.AuthAct(authModel);

        //        var voteModel = new VoteMessage
        //        {
        //            IDVN = responseAuth.Value.IDVN,
        //            Party = 10
        //        };

        //        var response = await controller.VoteAct(voteModel);

        //        Assert.IsFalse(response.Value.VoteStatus);
        //    }
    }
}