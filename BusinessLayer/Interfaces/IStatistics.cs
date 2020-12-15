using RVTLibrary.Models.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IStatistics
    {
        public Task<ResultsResponse> Results(string id);
        public Task<StatisticsResponse> Statistics(string id);
    }
}
