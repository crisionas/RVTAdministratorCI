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
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        IResults results;
        public ResultsController()
        {
            var bl = new BusinessManager();
            results = bl.GetResults();
        }

        [HttpPost]
        public async Task<ActionResult<ResultsResponse>> ResultsAll([FromBody]string id)
        {
            return await results.Results(id);
        }
    }
}
