using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
// [Route("api/gestion/apprenants")]
// [ApiExplorerSettings(GroupName = "Apprenants")] // Pour Swagger puis grouper les endpoints
//[Route("/zk")]
public class ApprenantsController : ControllerBase
{
    // 🔹 GET /
    [HttpGet]
    public IActionResult Hello()
    {
        var message = new { message = "Hello World apprenant" };
        return Ok(message);
    }

    // 🔹 GET /apprenants
    // [HttpGet("apprenants")]
    // public IActionResult GetAllApprenants()
    // {
    //     var liste = new[] {
    //         new { Id = 1, Nom = "Jonathan" },
    //         new { Id = 2, Nom = "Amina" }
    //     };
    //     return Ok(liste);
    // }

    // // 🔹 GET /apprenants/1
    // [HttpGet("apprenants/{id}")]
    // public IActionResult GetApprenantById(int id)
    // {
    //     var apprenant = new { Id = id, Nom = "Jonathan" };
    //     return Ok(apprenant);
    // }
}
