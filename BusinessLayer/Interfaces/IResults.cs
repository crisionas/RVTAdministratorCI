using RVTLibrary.Models.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IResults
    {
        public Task<ResultsResponse> Results(string id);
    }
}
