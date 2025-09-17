using Microsoft.AspNetCore.Mvc;
using formationAppApi.Models;
using formationAppApi.Data;

[ApiController]
[Route("api/v1/[controller]")]
// [Route("api/gestion/apprenants")]
// [ApiExplorerSettings(GroupName = "Apprenants")] // Pour Swagger puis grouper les endpoints
//[Route("/zk")]
public class ApprenantsController : ControllerBase
{
    //readonly => On ne peut affecter une valeur à cette variable qu’une seule fois,
    private readonly MonDbContext _context;
    public ApprenantsController(MonDbContext context)
    {
        _context = context;
    }

    // 🔹 GET /
    [HttpGet]
    public IActionResult GetAllApprenantsAsync()
    {
        var message = new { message = "Hello World apprenant" };
        return Ok(message);
    }

    // GET: /api/apprenant/5
    //[HttpGet("{id}")]
    [HttpGet("{id}", Name = "GetApprenant")]
    public async Task<ActionResult<Apprenant>> GetApprenantAsync(int id)
    {
        var apprenant = await _context.Apprenants.FindAsync(id);

        if (apprenant == null)
        {
            return NotFound();
        }

        return apprenant;
        // Simuler une opération asynchrone
        
        // await Task.Delay(1); // ← Évite le warning CS1998    
        // var message = new { message = $"Hello World apprenant Id: {id}" };
        // return Ok(message);
    }

    // [HttpPost("createV01")]
    // public async Task<ActionResult<Apprenant>> PostApprenant(Apprenant requestData)
    // {
    //     _context.Apprenants.Add(requestData);
    //     await _context.SaveChangesAsync();

    //     //return CreatedAtAction(nameof(GetApprenantAsync), new { id = requestData.Id }, requestData);
    //     //return CreatedAtRoute("GetApprenant", new { id = requestData.Id }, requestData);
    //     //return CreatedAtAction( requestData);
    //     // return Ok(new { 
    //     //     message = "Enregistrement réussi",
    //     //     nom = model.Nom,
    //     //     postnom = model.Postnom 
    //     // });
    //     return Ok(new { datas = requestData });
    // }

    [HttpPost("create")]
    public async Task<ActionResult<Apprenant>> PostApprenant(Apprenant requestData)
    {
        // Création de l'objet Apprenant à partir des données reçues
        var apprenant = new Apprenant
        {
            Nom = requestData.Nom.ToUpper(),        // Utilisez requestData.Nom
            Postnom = requestData.Postnom, // Utilisez requestData.Postnom
            Prenom = "30" // Utilisez requestData.Prenom
        };
        _context.Apprenants.Add(apprenant);
        await _context.SaveChangesAsync();

        return Ok(new { 
            message = "Enregistrement réussi",
            requestData = apprenant  
        });
    }

    // [HttpPost("createM")]
    // public async Task<IActionResult> CreateApprenantAsync([FromBody] ApprenantDto apprenantDto)
    // {
    //     // if (!ModelState.IsValid)
    //     // {
    //     //     return BadRequest(ModelState);
    //     // }

    //     // Simuler une opération asynchrone
    //     //await Task.Delay(1000);

    //     //return CreatedAtAction(nameof(GetAllApprenantsAsync), new { id = apprenantDto.Id }, apprenantDto);
    //     // var liste = new[] {
    //     //     new { Id = 1, Nom = "Jonathan" },
    //     //     new { Id = 2, Nom = "Amina" }        
    //     // };
    //     // return Ok(liste);
    //     return Ok(new { message = "Hello World apprenant" });
    // }

    // [HttpPost("createV0")]
    // public IActionResult Create([FromBody] Apprenant requestData)
    // {
    //     return Ok(new { datass = requestData.Nom.ToUpper() });
    // }

    // [HttpPost("createMM")]
    // public IActionResult Enregistrer([FromBody] object requestData)
    // {
    //     return Ok(new { datass = requestData });
    // }

    // [HttpPost("createMMM")]
    // public IActionResult Enregistrer([FromBody] dynamic requestData)
    // {
    //     //string prenom = requestData.prenom; // ou requestData.Prenom
    //     //string prenom = requestData["prenom"]?.ToString();
    //     string prenom = requestData.prenom?.ToString();
    //     return Ok(new { prenom = prenom });
    //     //return Ok(new { data = requestData.prenom });
    // }

    // [HttpPost("createMMMM")]
    // public IActionResult Enregistrer([FromBody] Dictionary<string, object>  requestData)
    // {
    //     string prenom = requestData["prenom"]?.ToString();
    //     return Ok(new { prenom = prenom });

    //     if (requestData.TryGetValue("prenom", out var prenomValue))
    //     {
    //         string prenom = prenomValue?.ToString();
    //         return Ok(new { prenom = prenom });
    //     }
    //     else
    //     {
    //         return BadRequest(new { error = "Le champ 'prenom' est requis" });
    //     }
    // }

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

    // POST: /api/Utilisateurs
    // [HttpPost("apprenantsV0")]
    // public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
    // {
    //     _context.Utilisateurs.Add(utilisateur);
    //     await _context.SaveChangesAsync();

    //     return CreatedAtAction(nameof(GetUtilisateur), new { id = utilisateur.Id }, utilisateur);
    // }
}
