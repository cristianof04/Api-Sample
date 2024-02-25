using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;

namespace ApiSample.Controllers
{
    public class ApiSampleController: ApiController
    {
        /// <summary>
        /// Overview
        /// </summary>
        /// <param name="sucess"></param>
        /// <returns>Return Sucess or Fail</returns>
        [HttpPost]
        [Route("api/ApiSample/Overview")]
        public HttpResponseMessage Overview(bool sucess)
        {

            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            if (sucess)
            {
                statusCode = HttpStatusCode.OK;
            }

            return new HttpResponseMessage()
            {
                Content = new StringContent(sucess == true ? "Sucess" : "Fail"),
                StatusCode = statusCode,
            };

        }
    }
}