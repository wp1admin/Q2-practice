using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using producer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace producer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        [HttpPost]
        public IActionResult PostTask([FromBody] Userinfo user)
        {
            APIresult apiResult = new APIresult();
            apiResult = Utils.Validat.CheckUserInfo(user);

            ObjectResult result = new ObjectResult(apiResult);
            if(apiResult.hasError == false)
            {
                result.ContentTypes.Add("application/json");

                Utils.RabbitMQ.publish(apiResult);
                result.StatusCode = StatusCodes.Status200OK;
            }
            else
            {
                result.ContentTypes.Add("application/json");


                result.StatusCode = StatusCodes.Status401Unauthorized;
            }

            return result;
        }
    }
}
