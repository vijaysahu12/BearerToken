﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BearerToken.Controllers
{
    [Produces("application/json")]
    [Route("api/Values")]
    [Authorize]
    public class ValuesController : Controller
    {

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "values", "value2" };
        }
    }
}