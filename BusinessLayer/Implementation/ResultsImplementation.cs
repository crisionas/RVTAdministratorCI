using RVT_DataLayer.Entities;
using RVTLibrary.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementation
{
    public class ResultsImplementation
    {
        internal async Task<ResultsResponse> ResultsAction(string id)
        {

            List<VoteStatistics> parties = new List<VoteStatistics>();
            int votants = 0;
            int population = 0;
            int pending = 0;
            string name;
            var gender = new GenderStatistic();
            using (var context = new SFBDContext())
            {
                if (id != "0")
                {

                    votants = (from st in context.Blocks
                               where st.RegionChoosed == Int32.Parse(id)
                               select st.BlockId).Count();
                    name = (from st in context.Regions
                            where st.Idreg == Int32.Parse(id)
                            select st.Region1).Single().ToString();
                    //-----------------Number of parties to count------------------
                    for (int i = 1; i <= context.Parties.Count(); i++)
                    {
                        var party = new VoteStatistics();
                        party.IDParty = i;
                        party.Votes = (from st in context.Blocks
                                       where st.PartyChoosed == party.IDParty &&
                                       st.RegionChoosed == Int32.Parse(id)
                                       select st).Count();
                        parties.Add(party);
                    }
                    //-----------------Population------------------
                    population = (from st in context.FiscData
                                  where st.Region == name
                                  select st.Idnp).Count();

                    //-----------------Number of male gender voters------------------
                    gender.Male = (from st in context.Blocks
                                   where st.Gender == "Male" &&
                                   st.RegionChoosed == Int32.Parse(id)
                                   select st).Count();
                    //-----------------Number of female gender voters------------------
                    gender.Female = (from st in context.Blocks
                                     where st.Gender == "Female" &&
                                     st.RegionChoosed == Int32.Parse(id)
                                     select st).Count();
                }
                else
                {
                    name = "All";
                    votants = (from st in context.Blocks
                               select st.BlockId).Count();
                    //-----------------Number of parties to count------------------
                    for (int i = 1; i <= 5; i++)
                    {
                        var party = new VoteStatistics();
                        party.IDParty = i;
                        party.Votes = (from st in context.Blocks
                                       where st.PartyChoosed == party.IDParty
                                       select st).Count();
                        parties.Add(party);
                    }

                    population = (from st in context.FiscData
                                  select st.Idnp).Count();
                    pending = (from st in context.IdvnAccounts
                               select st.Idvn).Count();
                    gender.Male = (from st in context.Blocks
                                   where st.Gender == "Male"
                                   select st).Count();
                    gender.Female = (from st in context.Blocks
                                     where st.Gender == "Female"
                                     select st).Count();
                }


            }

            return new ResultsResponse
            {
                Name = name,
                Time = DateTime.Now,
                TotalVotes = parties,
                Votants = votants,
                Population = population,
                Pending = pending,
                GenderStatistics = gender
            };
        }
    }
}
