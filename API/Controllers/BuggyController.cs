using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _ctx;

        public BuggyController(DataContext ctx)
        {
            _ctx = ctx;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            string x = "secret text";
            return x;
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var obj = _ctx.Users.Find(-1);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var obj = _ctx.Users.Find(-1);//returns null ref so must convet  to string to display
            var serverException = obj.ToString();
            return serverException;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("this was not a valid request");
        }


    }
}