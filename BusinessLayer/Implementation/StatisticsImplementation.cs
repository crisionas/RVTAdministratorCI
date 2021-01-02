using RVT_DataLayer.Entities;
using RVTLibrary.Models.Results;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace BusinessLayer.Implementation
{
    public class StatisticsImplementation
    {
        private static Logger _logger = LogManager.GetLogger("UserLog");

        internal async Task<ResultsResponse> ResultsAction(string id)
        {

            List<VoteStatistics> parties = new List<VoteStatistics>();
            int votants = 0;
            int population = 0;
            int pending = 0;
            string name=null;
            var gender = new GenderStatistic();
            try
            {
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
                            party.Color = context.Parties.Where(x => x.Idpart == party.IDParty).SingleOrDefault()
                                ?.Color;
                            party.Name = context.Parties.Where(x => x.Idpart == party.IDParty).SingleOrDefault()
                                ?.Party1;
                            parties.Add(party);
                        }

                        //-----------------Population------------------
                        population = (from st in context.FiscData
                            where st.Region == name
                            select st.Idnp).Count();

                        //-----------------Number of male gender voters------------------
                        gender.Male = (from st in context.Blocks
                            where st.Gender == "Masculin" &&
                                  st.RegionChoosed == Int32.Parse(id)
                            select st).Count();
                        //-----------------Number of female gender voters------------------
                        gender.Female = (from st in context.Blocks
                            where st.Gender == "Feminin" &&
                                  st.RegionChoosed == Int32.Parse(id)
                            select st).Count();
                    }
                    else
                    {
                        name = "Republica Moldova";
                        votants = (from st in context.Blocks
                            select st.BlockId).Count();
                        //-----------------Number of parties to count------------------
                        for (int i = 1; i <= 5; i++)
                        {
                            var party = new VoteStatistics();
                            party.IDParty = i;
                            var Name = context.Parties.Where(x => x.Idpart == party.IDParty).SingleOrDefault()?.Party1;

                            party.Votes = (from st in context.Blocks
                                where st.PartyChoosed == party.IDParty
                                select st).Count();
                            party.Color = context.Parties.Where(x => x.Idpart == party.IDParty).SingleOrDefault()
                                ?.Color;
                            party.Name = context.Parties.Where(x => x.Idpart == party.IDParty).SingleOrDefault()
                                ?.Party1;

                            parties.Add(party);
                        }

                        population = (from st in context.FiscData
                            select st.Idnp).Count();
                        pending = (from st in context.IdvnAccounts
                            select st.Idvn).Count();
                        gender.Male = (from st in context.Blocks
                            where st.Gender == "Masculin"
                            select st).Count();
                        gender.Female = (from st in context.Blocks
                            where st.Gender == "Feminin"
                            select st).Count();
                    }


                }
            }

            catch (Exception e)
            {
                _logger.Error("Results | " + e.Message);
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




        internal async Task<StatisticsResponse> StatisticsAction(string id)
        {
            List<AgeStatistics> agesList = new List<AgeStatistics>();
            int votants = 0;
            int population = 0;
            int pending = 0;
            string name=null;

            var gender = new GenderStatistic();
            try
            {
                using (var context = new SFBDContext())
                {
                    if (id != "0")
                    {
                        //-----------------Region name------------------
                        name = (from st in context.Regions
                            where st.Idreg == Int32.Parse(id)
                            select st.Region1).Single().ToString();

                        //-----------------Population------------------
                        population = (from st in context.FiscData
                            where st.Region == name
                            select st.Idnp).Count();

                        //-----------------Count voters by ages------------------
                        //18-25
                        var age18 = new AgeStatistics();
                        age18.Ages = "18-25";

                        age18.Voters = context.IdvnAccounts.Where(e => DateTime.Now.Year - e.BirthDate.Value.Year >= 18
                                                                       && DateTime.Now.Year - e.BirthDate.Value.Year <=
                                                                       25
                                                                       && e.Region.ToString() == id).Count().ToString();
                        agesList.Add(age18);

                        //26-40
                        var age26 = new AgeStatistics();
                        age26.Ages = "26-40";

                        age26.Voters = context.IdvnAccounts.Where(e => DateTime.Now.Year - e.BirthDate.Value.Year >= 26
                                                                       && DateTime.Now.Year - e.BirthDate.Value.Year <=
                                                                       40
                                                                       && e.Region.ToString() == id).Count().ToString();
                        agesList.Add(age26);

                        //41-50
                        var age41 = new AgeStatistics();
                        age41.Ages = "41-50";

                        age41.Voters = context.IdvnAccounts.Where(e => DateTime.Now.Year - e.BirthDate.Value.Year >= 41
                                                                       && DateTime.Now.Year - e.BirthDate.Value.Year <=
                                                                       50
                                                                       && e.Region.ToString() == id).Count().ToString();
                        agesList.Add(age41);

                        //51-64
                        var age51 = new AgeStatistics();
                        age51.Ages = "51-64";

                        age51.Voters = context.IdvnAccounts.Where(e => DateTime.Now.Year - e.BirthDate.Value.Year >= 51
                                                                       && DateTime.Now.Year - e.BirthDate.Value.Year <=
                                                                       65
                                                                       && e.Region.ToString() == id).Count().ToString();
                        agesList.Add(age51);

                        //65+
                        var age65 = new AgeStatistics();
                        age65.Ages = "65";

                        age65.Voters = context.IdvnAccounts.Where(e => DateTime.Now.Year - e.BirthDate.Value.Year >= 65
                                                                       && e.Region.ToString() == id).Count().ToString();
                        agesList.Add(age65);

                        //-----------------Population------------------
                        votants = context.IdvnAccounts.Where(e => e.Region.ToString() == id).Count();

                        //-----------------Number of male gender voters------------------
                        gender.Male = (from st in context.IdvnAccounts
                            where st.Gender == "Masculin" &&
                                  st.Region == Int32.Parse(id)
                            select st).Count();
                        //-----------------Number of female gender voters------------------
                        gender.Female = (from st in context.IdvnAccounts
                            where st.Gender == "Feminin" &&
                                  st.Region == Int32.Parse(id)
                            select st).Count();
                    }
                    else
                    {
                        name = "Republica Moldova";

                        //-----------------Population------------------
                        population = (from st in context.FiscData
                            select st.Idnp).Count();

                        //-----------------Count voters by ages------------------
                        //18-25
                        var age18 = new AgeStatistics();
                        age18.Ages = "18-25";

                        age18.Voters = context.IdvnAccounts.Where(e => DateTime.Now.Year - e.BirthDate.Value.Year >= 18
                                                                       && DateTime.Now.Year - e.BirthDate.Value.Year <=
                                                                       25).Count().ToString();
                        agesList.Add(age18);

                        //26-40
                        var age26 = new AgeStatistics();
                        age26.Ages = "26-40";

                        age26.Voters = context.IdvnAccounts.Where(e => DateTime.Now.Year - e.BirthDate.Value.Year >= 26
                                                                       && DateTime.Now.Year - e.BirthDate.Value.Year <=
                                                                       40).Count().ToString();
                        agesList.Add(age26);

                        //41-50
                        var age41 = new AgeStatistics();
                        age41.Ages = "41-50";

                        age41.Voters = context.IdvnAccounts.Where(e => DateTime.Now.Year - e.BirthDate.Value.Year >= 41
                                                                       && DateTime.Now.Year - e.BirthDate.Value.Year <=
                                                                       50).Count().ToString();
                        agesList.Add(age41);

                        //51-64
                        var age51 = new AgeStatistics();
                        age51.Ages = "51-64";

                        age51.Voters = context.IdvnAccounts.Where(e => DateTime.Now.Year - e.BirthDate.Value.Year >= 51
                                                                       && DateTime.Now.Year - e.BirthDate.Value.Year <=
                                                                       65).Count().ToString();
                        agesList.Add(age51);

                        //65+
                        var age65 = new AgeStatistics();
                        age65.Ages = "65+";

                        age65.Voters = context.IdvnAccounts.Where(e => DateTime.Now.Year - e.BirthDate.Value.Year >= 65)
                            .Count().ToString();
                        agesList.Add(age65);

                        //-----------------Population------------------
                        votants = context.IdvnAccounts.Select(e => e.Idvn).Count();

                        //-----------------Number of male gender voters------------------
                        gender.Male = (from st in context.IdvnAccounts
                            where st.Gender == "Masculin"
                            select st).Count();
                        //-----------------Number of female gender voters------------------
                        gender.Female = (from st in context.IdvnAccounts
                            where st.Gender == "Feminin"
                            select st).Count();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error("Statistics | " + e.Message);
            }

            return new StatisticsResponse
            {
                Name = name,
                AgeVoters = agesList,
                GenderStatistics = gender,
                Time = DateTime.Now,
                Voters = votants,
                Population = population
            };
        }
    }
}