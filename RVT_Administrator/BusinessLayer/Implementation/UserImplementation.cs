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
using System.Data.Entity;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
            //Verify if registration registration are valid.
            using (var db = new SFBD_AccountsContext())
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
            var clientCertificate =new X509Certificate2(Path.Combine(@"..\Certs", "administrator.pfx"), "ar4iar4i"
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
                _logger.Error(e.Message);
                return new RegistrationResponse { Status = false, Message = "Error! LoadBalancer is not responding." + e.Message };
            }

            if (regLbResponse.Status == true)
            {
                ///-------SEND EMAIL WITH PASSWORD------
                EmailSender.Send(registration.Email, regLbResponse.VnPassword);

                using (var db = new SFBD_AccountsContext())
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
                return new RegistrationResponse { Status = true, ConfirmKey = random.Next(12452, 87620).ToString(), Message = "Registration | IP: " + registration.Ip_address + "IDNP: " + registration.IDNP + " was registered.", IDVN = regLbResponse.IDVN, Email = registration.Email };
            }
            else
            {
                _logger.Error("Registration | "+registration);
                return new RegistrationResponse { Status = false, Message = "Registration | Error! User IP: " + registration.Ip_address + "IDNP: " + registration.IDNP + " can't be registered." };
            }
        }
        internal async Task<AuthResponse> AuthAction(AuthMessage auth)
        {
            try
            {
                var pass = LoginHelper.HashGen(auth.VnPassword);
                var idvn = IDVN_Gen.HashGen(auth.VnPassword + auth.IDNP);

                using (var db = new SFBD_AccountsContext())
                {
                    var verify = db.IdvnAccounts.FirstOrDefaultAsync(x =>
                      x.Idvn == idvn &&
                      x.VnPassword == pass);

                    if (verify == null)
                    {
                        return new AuthResponse { Status = false, Message = "Auth | Error! IDNP or password are not correct." };
                    }
                    else
                        return new AuthResponse { Status = true, IDVN = idvn, Message = "Auth | Authentication Successfull!" };
                }
            }
            catch(Exception e)
            {
                _logger.Error("Auth | Error! " + e.Message);
                return new AuthResponse { Status = false, Message = "Auth | "+e.Message };
            }
        }


    }
}
