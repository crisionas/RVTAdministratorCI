using BusinessLayer;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RVT_AdministratorAPI.AppServices;
using RVTLibrary.Models.AuthUser;
using RVTLibrary.Models.UserIdentity;
using RVTLibrary.Models.Vote;
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
        private readonly IQueueConnection _queueConnection;
        
        public IdentityController(IServiceProvider provider)
        {
            var bl = new BusinessManager();
            user = bl.GetUser();
            //_queueConnection = provider.GetRequiredService<RabbitMQQueueConnection>();
        }

        [HttpPost]
        [ActionName("Registration")]
        public async Task<ActionResult<RegistrationResponse>> RegistrationAct([FromBody] RegistrationMessage registration)
        { 
           return await user.Registration(registration);
        }

        [HttpPost]
        [ActionName("Auth")]
        public async Task<ActionResult<AuthResponse>> AuthAct([FromBody] AuthMessage auth)
        {
            return await user.Auth(auth);
        }

        [HttpPost]
        [ActionName("Vote")]
        public async Task<ActionResult<VoteResponse>> VoteAct([FromBody] VoteMessage vote)
        {
            var result = await user.Vote(vote);

            var body = JsonConvert.SerializeObject(result.LBMessage);
            _queueConnection.PublishData("voteDataMsg", body);
            return result.VoteResponse;
        }
    }
}
