using BusinessLayer;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RVTLibrary.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RVT_AdministratorAPI.Controllers.Users
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        IStatistics results;
        public ResultsController()
        {
            var bl = new BusinessManager();
            results = bl.GetResults();
        }

        [HttpPost]
        [ActionName("Results")]
        public async Task<ActionResult<ResultsResponse>> ResultsAll([FromBody] string id)
        {
            return await results.Results(id);
        }

        [HttpPost]
        [ActionName("Statistics")]
        public async Task<ActionResult<StatisticsResponse>> StatisticsAll([FromBody] string id)
        {
            return await results.Statistics(id);
        }

    }
}
