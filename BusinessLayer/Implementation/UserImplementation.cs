﻿using AutoMapper;
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

namespace BusinessLayer.Implementation
{

    public class UserImplementation
    {
        private static Logger _logger = LogManager.GetLogger("UserLog");

        internal async Task<RegistrationResponse> RegistrationAction(RegistrationMessage registration)
        {

            var config = new MapperConfiguration(cfg =>
                  cfg.CreateMap<RegistrationMessage, FiscDatum>());
            var mapper = new Mapper(config);
            var fiscregistration = mapper.Map<FiscDatum>(registration);
            fiscregistration.BirthDate = registration.Birth_date;
            //Verify if registration registration are valid.
            using (var db = new SFBDContext())
            {
                try
                {
                    var fisc = await db.FiscData.FirstOrDefaultAsync(x =>
               x.Idnp == fiscregistration.Idnp &&
               x.Gender == fiscregistration.Gender &&
               x.Region.Contains(fiscregistration.Region) &&
               x.Surname == fiscregistration.Surname &&
               x.Name == fiscregistration.Name &&
               x.BirthDate == fiscregistration.BirthDate);
                    if (fisc == null)
                    {
                        return new RegistrationResponse { Status = false, Message = "Datele introduse nu sunt correcte." };
                    }
                }
                catch (Exception e)
                {
                    _logger.Error("Registration | " + e.Message);
                }
            }

            //Send registration to LoadBalancer
            var content = new StringContent(JsonConvert.SerializeObject(registration), Encoding.UTF8, "application/json");
            var clientCertificate = new X509Certificate2(Path.Combine(@"..\Certs", "administrator.pfx"), "ar4iar4i"
                , X509KeyStorageFlags.Exportable);

            var handler = new HttpClientHandler();
            handler.ClientCertificates.Add(clientCertificate);
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:44322/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsync("api/Regist", content);

            var regLbResponse = new RegLBResponse();
            try
            {
                var registration_resp = await response.Content.ReadAsStringAsync();
                regLbResponse = JsonConvert.DeserializeObject<RegLBResponse>(registration_resp);
            }
            catch (AggregateException e)
            {
                _logger.Error("Registration | " + e.Message);
                return new RegistrationResponse { Status = false, Message = "Error! LoadBalancer nu răspunde." + e.Message };
            }

            if (regLbResponse.Status == true)
            {
                ///-------SEND EMAIL WITH PASSWORD------
                EmailSender.Send(registration.Email, regLbResponse.VnPassword);

                using (var db = new SFBDContext())
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
                return new RegistrationResponse { Status = true, ConfirmKey = random.Next(12452, 87620).ToString(), Message = "Registration | IDNP: " + registration.IDNP + " a fost înregistrat.", IDVN = regLbResponse.IDVN, Email = registration.Email };
            }
            else
            {
                return new RegistrationResponse { Status = false, Message = "Registration | IDNP: " + registration.IDNP + " nu a fost posibil de înregistrat" };
            }
        }
        internal async Task<AuthResponse> AuthAction(AuthMessage auth)
        {
            try
            {
                var pass = LoginHelper.HashGen(auth.VnPassword);
                var idvn = IDVN_Gen.HashGen(auth.VnPassword + auth.IDNP);

                using (var db = new SFBDContext())
                {
                    var verify = db.IdvnAccounts.FirstOrDefaultAsync(x =>
                      x.Idvn == idvn &&
                      x.VnPassword == pass);

                    if (verify.Result == null)
                    {
                        return new AuthResponse { Status = false, Message = "Error! IDNP-ul sau parola nu este corectă." };
                    }
                    else
                        return new AuthResponse { Status = true, IDVN = idvn, Message = "V-ați autentificat cu succes." };

                }
            }
            catch (Exception e)
            {
                _logger.Error("Auth | Error! " + e.Message);
                return new AuthResponse { Status = false, Message = "Auth | " + e.Message };
            }
        }
        internal async Task<VoteCoreResponse> VoteAction(VoteMessage vote)
        {
            using (var bd = new SFBDContext())
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

                    var party = bd.Parties.FirstOrDefault(m => m.Idpart == vote.Party);
                    var user = bd.IdvnAccounts.FirstOrDefault(m => m.Idvn == vote.IDVN);
                    var chooser = new ChooserLBMessage
                    {
                        IDVN = user.Idvn,
                        Gender = user.Gender,
                        Birth_date = user.BirthDate,
                        PartyChoosed = vote.Party,
                        Region = user.Region,
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
    }
}
