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
        public Task<ResultsResponse> Results(string id)
        {
            return ResultsAction(id);
        }

        public Task<StatisticsResponse> Statistics(string id)
        {
            return StatisticsAction(id);
        }
    }
}
