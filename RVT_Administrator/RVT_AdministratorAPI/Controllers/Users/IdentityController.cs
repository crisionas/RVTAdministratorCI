using BusinessLayer;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RVTLibrary.Models.AuthUser;
using RVTLibrary.Models.UserIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RVT_AdministratorAPI.Controllers.Users
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        IUser user;

        public IdentityController()
        {
            var bl = new BusinessManager();
            user = bl.GetUser();
        }

        [HttpPost]
        [ActionName("Registration")]
        public async Task<ActionResult<RegistrationResponse>> RegistrationAct([FromBody]RegistrationMessage registration)
        {
            if (ModelState.IsValid)
            {
                var result = await user.Registration(registration);
                return result;
            }
            else return BadRequest();
        }
        
        [HttpPost]
        [ActionName("Auth")]
        public async Task<ActionResult<AuthResponse>> AuthAct([FromBody]AuthMessage auth)
        {
            if (ModelState.IsValid)
            {
                var result = await user.Auth(auth);
                return result;
            }
            else return BadRequest();
        }
    }
}
