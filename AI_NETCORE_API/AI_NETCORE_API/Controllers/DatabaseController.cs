using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI_NETCORE_API.Models.Response.ExecutingTimes;
using Domain.Deleters.Thanos.Abstract;
using Domain.Infrastructure.Logging.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_NETCORE_API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        ILogger _logger;
        IDbDeleter _deleter;

        public DatabaseController(ILogger logger, IDbDeleter deleter)
        {
            _logger = logger;
            _deleter = deleter;
        }

        [HttpDelete("database/clear")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult ClearDatabase()
        {
            try
            {
                var tim = System.Diagnostics.Stopwatch.StartNew();
                var dbTime = _deleter.Clear();
                var time = tim.ElapsedMilliseconds;

                return Ok(new ExecutionDetails()
                {
                    DbTime = dbTime,
                    ExecTime = time,
                });
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        [HttpDelete("database/purge")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult PurgeDatabase()
        {
            try
            {
                var tim = System.Diagnostics.Stopwatch.StartNew();
                var dbTime = _deleter.Purge();
                var time = tim.ElapsedMilliseconds;

                return Ok(new ExecutionDetails()
                {
                    DbTime = dbTime,
                    ExecTime = time,
                });
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }
    }
}