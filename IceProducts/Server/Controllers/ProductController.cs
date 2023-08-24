using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IceProducts.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public async Task Get()
        {

        }
    }
}
