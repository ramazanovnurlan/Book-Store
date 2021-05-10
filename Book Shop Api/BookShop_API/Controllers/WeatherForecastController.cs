using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
         
        [HttpGet]
        [Route("getStr")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IEnumerable<string> Get()
        {
          return  new List<string>() { "Birincisi Salam,", "ikincisi suali tekrar eliyin,", "carona virus" };
        }
    }
}
