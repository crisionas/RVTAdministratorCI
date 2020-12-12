using BusinessLayer.Implementation;
using BusinessLayer.Interfaces;
using RVTLibrary.Models.AuthUser;
using RVTLibrary.Models.UserIdentity;
using RVTLibrary.Models.Vote;
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
        public Task<AuthResponse> Auth(AuthMessage auth)
        {
            return AuthAction(auth);
        }

        public Task<VoteResponse> Vote(VoteMessage vote)
        {
            return VoteAction(vote);
        }
    }
}
