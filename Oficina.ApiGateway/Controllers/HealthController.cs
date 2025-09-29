using Microsoft.AspNetCore.Mvc;

namespace Oficina.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new { ok = true, utc = DateTime.UtcNow });
}
