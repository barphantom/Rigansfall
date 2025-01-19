using Microsoft.AspNetCore.Mvc;
using Rigansfall.Server.Models.DTOs;

namespace Rigansfall.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoveRequestController : Controller
    {
        [HttpPost]
        public IActionResult MovePlayer([FromBody] MoveRequest moveRequest)
        {
            if (Math.Abs(moveRequest.currentX - moveRequest.newX) <= 1 && Math.Abs(moveRequest.currentY - moveRequest.newY) <= 1)
            {
                return Ok(new {canMove = true, reason = "moved one tile" });
            }
            else
            {
                return Ok(new { canMove = false, reason = "too far" });
            }
            }
        }
    }
}
