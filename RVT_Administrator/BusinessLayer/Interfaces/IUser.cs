using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RVTLibrary.Models.UserIdentity;

namespace BusinessLayer.Interfaces
{
    public interface IUser
    {
        public Task<RegistrationResponse> Registration(RegistrationMessage registration);
    }
}
