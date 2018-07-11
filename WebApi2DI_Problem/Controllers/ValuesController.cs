using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi2DI_Problem.Models;

namespace WebApi2DI_Problem.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly ITest _test;

        public ValuesController(ITest test)
        {
            _test = test;
        }


        public IHttpActionResult Get()
        {
            var data = _test.GetData();
            return Ok(data);
        }
    }
}
