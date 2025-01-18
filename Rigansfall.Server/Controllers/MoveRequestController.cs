using Microsoft.AspNetCore.Mvc;

namespace Rigansfall.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoveRequestController : Controller
    {
        [HttpPost]
        public IActionResult MovePlayer([FromBody] MoveRequest moveRequest)
        {
            if (moveRequest.newX < 0 || moveRequest.newY < 0 ||
                moveRequest.newX > 5 || moveRequest.newY > 5)
            {
                return Ok(new {canMove = false, reason = "Out of bounds" });
            }

            return Ok(new {canMove = true });
        }
    }
}
