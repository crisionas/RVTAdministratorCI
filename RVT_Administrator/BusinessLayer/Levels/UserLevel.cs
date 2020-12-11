using BusinessLayer.Implementation;
using BusinessLayer.Interfaces;
using RVTLibrary.Models.UserIdentity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Levels
{
    public class UserLevel : UserImplementation, IUser
    {
        public Task<RegistrationResponse> Registration(RegistrationMessage registration)
        {
            return RegistrationAction(registration);
        }
    }
}
