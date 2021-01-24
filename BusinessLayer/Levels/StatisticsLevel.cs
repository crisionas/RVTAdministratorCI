using BusinessLayer.Implementation;
using BusinessLayer.Interfaces;
using RVTLibrary.Models.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Levels
{
    public class StatisticsLevel : StatisticsImplementation, IStatistics
    {
        public async Task<ResultsResponse> Results(string id)
        {
            return await ResultsAction(id);
        }

        public async Task<StatisticsResponse> Statistics(string id)
        {
            return await StatisticsAction(id);
        }
    }
}
