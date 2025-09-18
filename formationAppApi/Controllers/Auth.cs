using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    [HttpGet("login")]
    public IActionResult LoginAsync()
    {
        var message = new { message = "Your login ok" };
        return Ok(message);
    } 
}