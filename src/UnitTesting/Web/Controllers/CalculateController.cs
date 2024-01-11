using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculateController : ControllerBase
    {

        [HttpGet(Name = "Add/{left}/{right}")]
        public int Get(int left, int right)
        {
            return new Calculator().Sum(left, right);
        }
        
    }
}
