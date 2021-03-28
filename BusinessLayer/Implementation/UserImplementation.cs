using AutoMapper;
using BusinessLayer.Helper;
using BusinessLayer.Services;
using Newtonsoft.Json;
using NLog;
using RVT_DataLayer.Entities;
using RVTLibrary.Algorithms;
using RVTLibrary.Models.AuthUser;
using RVTLibrary.Models.LoadBalancer;
using RVTLibrary.Models.UserIdentity;
using System;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using RVTLibrary.Models.Vote;
using System.Linq;
using BusinessLayer.DBContexts;

namespace BusinessLayer.Implementation
{

    public class UserImplementation
    {
        Logger logger = LogManager.GetLogger("UserLog");

        internal async Task<RegistrationResponse> RegistrationAction(RegistrationMessage registration)
        {
            return await Task.Run(() =>
            {
                var config = new MapperConfiguration(cfg =>
                  cfg.CreateMap<RegistrationMessage, FiscData>());
                var mapper = new Mapper(config);
                var fiscregistration = mapper.Map<FiscData>(registration);
                fiscregistration.BirthDate = registration.Birth_date;
                //Verify if registration registration are valid.
                using (var db = new SystemDBContext())
                {
                    try
                    {
                        var fisc = db.FiscData.FirstOrDefaultAsync(x =>
                  x.Idnp == fiscregistration.Idnp &&
                  x.Gender == fiscregistration.Gender &&
                  x.Region.Contains(fiscregistration.Region) &&
                  x.Surname == fiscregistration.Surname &&
                  x.Name == fiscregistration.Name &&
                  x.BirthDate == fiscregistration.BirthDate);
                        if (fisc.Result == null)
                        {
                            return new RegistrationResponse { Status = false, Message = "Datele introduse nu sunt correcte." };
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error("Registration | "+registration.IDNP+" "+e.Message);
                    }
                }

                //Send registration to LoadBalancer
                var content = new StringContent(JsonConvert.SerializeObject(registration), Encoding.UTF8, "application/json");
                //var clientCertificate = new X509Certificate2(Path.Combine(@"..\Certs", "administrator.pfx"), "ar4iar4i", X509KeyStorageFlags.Exportable);

                //var handler = new HttpClientHandler();
                //handler.ClientCertificates.Add(clientCertificate);
                // handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                var regLbResponse = new NodeRegResponse();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44322/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.PostAsync("api/Regist", content);

                    try
                    {
                        var registration_resp = response.Result.Content.ReadAsStringAsync().Result;
                        regLbResponse = JsonConvert.DeserializeObject<NodeRegResponse>(registration_resp);
                    }
                    catch (AggregateException e)
                    {
                        logger.Error("Registration |" +registration.IDNP+" "+e.Message);
                        return new RegistrationResponse { Status = false, Message = "Error! Sistemul nu funcționează la moment reveniți mai târziu"};
                    }
                }

                if (regLbResponse.Status == true)
                {
                    ///-------SEND EMAIL WITH PASSWORD------
                    EmailSender.Send(registration.Email, regLbResponse.VnPassword);

                    using (var db = new SystemDBContext())
                    {
                        var account = new IdvnAccount();
                        account.Idvn = regLbResponse.IDVN;
                        account.VnPassword = LoginHelper.HashGen(regLbResponse.VnPassword);
                        account.StatusNumber = "Non confirmed";
                        account.IpAddress = registration.Ip_address;
                        account.PhoneNumber = registration.Phone_Number;
                        account.Email = registration.Email;

                        db.Add(account);
                        db.SaveChanges();

                    }

                    var random = new Random();
                    return new RegistrationResponse { Status = true, ConfirmKey = random.Next(12452, 87620).ToString(), Message = "Înregistrare | IDNP: " + registration.IDNP + " a fost înregistrat.", IDVN = regLbResponse.IDVN, Email = registration.Email };
                }
                else
                {
                    return new RegistrationResponse { Status = false, Message = "Înregistrare | IDNP: " + registration.IDNP + " nu a fost posibil de înregistrat" };
                }
            });

        }
        internal async Task<AuthResponse> AuthAction(AuthMessage auth)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var pass = LoginHelper.HashGen(auth.VnPassword);
                    var idvn = IDVN_Gen.HashGen(auth.VnPassword + auth.IDNP);

                    using (var db = new SystemDBContext())
                    {
                        var verify = db.IdvnAccounts.FirstOrDefault(x =>
                            x.Idvn == idvn &&
                            x.VnPassword == pass);
                        var vote_state = db.VoteStatuses.FirstOrDefault(m => m.Idvn == idvn);
                        if (verify == null)
                        {
                            return new AuthResponse { Status = false, Message = "Error! IDNP-ul sau parola nu este corectă." };
                        }
                        if (vote_state != null)
                            return new AuthResponse
                            {
                                Status = false,
                                Message = "Vote | Ați votat deja, nu este posibil de votat de două ori."
                            };
                        return new AuthResponse { Status = true, IDVN = idvn, Message = "V-ați autentificat cu succes." };

                    }
                }
                catch (Exception e)
                {

                    logger.Error("Auth |"+auth.IDNP+" "+e.Message);
                    return new AuthResponse { Status = false, Message = "Auth | " + e.Message };
                }
            });
            
        }

        internal async Task<VoteCoreResponse> VoteAction(VoteMessage vote)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var bd = new SystemDBContext())
                    {
                        var account = bd.IdvnAccounts.FirstOrDefault(m => m.Idvn == vote.IDVN);
                        if (account == null)
                            return new VoteCoreResponse
                            {
                                VoteResponse = new VoteResponse
                                {
                                    VoteStatus = false,
                                    Message = "Vote | Utilizatorul nu există, este necesar să vă înregistrați.",
                                    ProcessedTime = DateTime.Now
                                },
                                LBMessage = null
                            };
                        else
                        {
                            var vote_state = bd.VoteStatuses.FirstOrDefault(m => m.Idvn == vote.IDVN);
                            if (vote_state != null)
                                return new VoteCoreResponse
                                {
                                    VoteResponse = new VoteResponse
                                    {
                                        VoteStatus = false,
                                        Message = "Vote | Ați votat deja, nu puteți vota de două ori.",
                                        ProcessedTime = DateTime.Now
                                    },
                                    LBMessage = null
                                };

                            var party = bd.Parties.FirstOrDefault(m => m.PartyId == vote.Party);
                            var user = bd.IdvnAccounts.FirstOrDefault(m => m.Idvn == vote.IDVN);
                            var chooser = new ChooserLBMessage
                            {
                                IDVN = user.Idvn,
                                Gender = user.Gender,
                                Birth_date = user.BirthDate,
                                PartyChoosed = vote.Party,
                                Region = user.RegionId,
                                Vote_date = DateTime.Now
                            };

                            return new VoteCoreResponse
                            {
                                LBMessage = chooser,
                                VoteResponse = new VoteResponse
                                {
                                    VoteStatus = true,
                                    Message = "Votul s-a transmis la validare.",
                                    ProcessedTime = DateTime.Now
                                }
                            };

                        }
                    }
                }
                catch (Exception e)
                {
                    logger.Error("Vote | "+vote.IDVN+" "+e.Message);
                    return new VoteCoreResponse
                    {
                        VoteResponse = new VoteResponse
                        {
                            VoteStatus = false,
                            Message = e.Message,
                            ProcessedTime = DateTime.Now
                        }
                    };
                }
            });
        }
    }
}
