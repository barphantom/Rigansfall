using Microsoft.AspNetCore.Mvc;

namespace Rigansfall.Server.Controllers
{
    [ApiController]
    [Route("api/MovePlayer")]
    public class MoveRequestController : Controller
    {
        [HttpPost]
        public IActionResult MovePlayer([FromBody] MoveRequestController moveRequest)
        {
            return View();
        }
    }
}
