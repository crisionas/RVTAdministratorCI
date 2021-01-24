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
        public async Task<RegistrationResponse> Registration(RegistrationMessage registration)
        {
            return await RegistrationAction(registration);
        }
        public async Task<AuthResponse> Auth(AuthMessage auth)
        {
            return await AuthAction(auth);
        }

        public async Task<VoteCoreResponse> Vote(VoteMessage vote)
        {
            return await VoteAction(vote);
        }
    }
}
