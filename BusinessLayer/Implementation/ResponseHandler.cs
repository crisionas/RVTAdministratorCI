using BusinessLayer.Interfaces;
using Newtonsoft.Json;
using RVT_DataLayer.Entities;
using RVTLibrary.Models.Vote;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Implementation
{
    public class ResponseHandler : IResponseHandler
    {
        public void PrepareVoteResponse(string content)
        {
            NodeVoteResponse response;
            try
            {
                response = JsonConvert.DeserializeObject<NodeVoteResponse>(content);
            }
            catch(JsonException e)
            {
                throw e;
            }

            using (var db = new SFBDContext())
            {
                VoteStatus voteStatus = new VoteStatus
                {
                    Idvn = response.IDVN,
                    VoteState = "Voted"
                };

                Block block = new Block
                {
                    BlockId = response.block.BlockId,
                    CreatedOn = (DateTime)response.block.CreatedOn,
                    PartyChoosed = (int)response.block.PartyChoosed,
                    Gender = response.block.Gender,
                    Hash = response.block.Hash,
                    Idbd = response.block.Idbd,
                    PreviousHash = response.block.PreviousHash,
                    RegionChoosed = (int)response.block.RegionChoosed,
                    YearBirth = (int)response.block.YearBirth
                };
                using(var transaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        db.Add(voteStatus);
                        db.Add(block);
                        db.SaveChanges();
                        transaction.Commit();
                    }catch(Exception e)
                    {
                        transaction.Rollback();
                        Console.WriteLine(e);
                    }
                }
            }

        }
    }
}
