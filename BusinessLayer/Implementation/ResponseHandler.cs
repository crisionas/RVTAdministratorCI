using BusinessLayer.DBContexts;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Newtonsoft.Json;
using NLog;
using RVT_DataLayer.Entities;
using RVTLibrary.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Implementation
{
    public class ResponseHandler : IResponseHandler
    {

        private static Logger _logger = LogManager.GetLogger("UserLog");

        public void PrepareVoteResponse(string content)
        {
            NodeVoteResponse response;
            try
            {
                response = JsonConvert.DeserializeObject<NodeVoteResponse>(content);
                SendMessage(response);
            }
            catch (JsonException e)
            {
                _logger.Error(e);
                throw e;
            }

            using (var db = new SystemDBContext())
            {
                VoteStatus voteStatus = new VoteStatus
                {
                    Idvn = response.IDVN,
                    VoteState = "Voted"
                };

                Block block = new Block
                {
                  //  BlockId = response.block.BlockId,
                    CreatedOn = (DateTime)response.block.CreatedOn,
                    PartyId = (int)response.block.PartyChoosed,
                    Gender = response.block.Gender,
                    Hash = response.block.Hash,
                    Idbd = response.block.Idbd,
                    PreviousHash = response.block.PreviousHash,
                    RegionId = (int)response.block.RegionChoosed,
                    YearBirth  = 1999// (int)response.block.YearBirth
                };
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        db.Add(voteStatus);
                        db.Add(block);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        _logger.Error(e);
                        Console.WriteLine(e);
                    }
                }
            }
        }

        public void SendMessage(NodeVoteResponse vote)
        {
            if (vote.Status)
            {
                using (var db = new SystemDBContext())
                {
                    try
                    {
                        var user = db.IdvnAccounts.FirstOrDefault(m => m.Idvn == vote.IDVN);
                        var votestatus = new VoteStatus { Idvn = vote.IDVN, VoteState = "Confirmed" };
                        db.Add(votestatus);
                        db.SaveChanges();
                        EmailSender.SendVoteResponse(user.Email, "Votul dumneavoastră a fost înregistrat cu succes!");
                    }
                    catch (Exception)
                    {}
                }
            }
            else
            {
                using (var db = new SystemDBContext())
                {
                    try
                    {
                        var user = db.IdvnAccounts.FirstOrDefault(m => m.Idvn == vote.IDVN);
                        db.Remove(user);
                        db.SaveChanges();
                        EmailSender.SendVoteResponse(user.Email, "Votul dumneavoastră nu a fost înregistrat cu succes. Vă rugăm să încercați din nou!");
                    }
                    catch (Exception)
                    {}
                }
            }
        }
    }
}
