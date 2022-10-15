using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly PretparkContext _pretparkContext;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, PretparkContext pretparkContext)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _pretparkContext = pretparkContext;
    }

    // POST: api/Account/registreer?Role=
    [HttpPost]
    [Route("registreer")]
    public async Task<ActionResult<IEnumerable<Attractie>>> Registreer([FromBody] GebruikerMetWachwoord gebruikerMetWachwoord, [FromQuery] string Role)
    {
        if(Role == "Admin")
        {
            return BadRequest("You are not allowed to create new Admin users");
        }
        if(Role != "Medewerker" && Role != "Gast")
        {
            return BadRequest("Role type does not exist, you can only create users of role 'Medewerker' or 'Gast'");
        }
        var resultaat = await _userManager.CreateAsync(gebruikerMetWachwoord, gebruikerMetWachwoord.Password);
        if(resultaat.Succeeded)
        {
            var _user = await _userManager.FindByNameAsync(gebruikerMetWachwoord.UserName);
            await _userManager.AddToRoleAsync(_user, Role);
        }
        return !resultaat.Succeeded ? new BadRequestObjectResult(resultaat) : StatusCode(201);
    }

    /*
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] GebruikerLogin gebruikerLogin)
    {
    var _user = await _userManager.FindByNameAsync(gebruikerLogin.UserName);

    if (_user != null)
    {
        await _signInManager.SignInAsync(_user, true);
        return Ok();
    }

    return Unauthorized();
    }
    */

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] GebruikerLogin gebruikerLogin)
    {
        var _user = await _userManager.FindByNameAsync(gebruikerLogin.UserName);
        if (_user != null)
            if (await _userManager.CheckPasswordAsync(_user, gebruikerLogin.Password))
            {
                var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("awef98awef978haweof8g7aw789efhh789awef8h9awh89efh89awe98f89uawef9j8aw89hefawef"));

                var signingCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, _user.UserName) };
                var roles = await _userManager.GetRolesAsync(_user);
                foreach (var role in roles)
                    claims.Add(new Claim(ClaimTypes.Role, role));
                var tokenOptions = new JwtSecurityToken
                (
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: signingCredentials
                );
                return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions) });
            }

        return Unauthorized();
    }
    
    // GET: api/Account/UserWithRoles
    [HttpGet]
    [Route("UsersWithRoles")]
    public async Task<ActionResult<IEnumerable<GebruikerMetRoles>>> GetGebruikerMetRoles()
    {
        List<GebruikerMetRoles> results = new List<GebruikerMetRoles>();
        var users = await Task.Run(() => {
            return _pretparkContext.Users;
        });
        foreach(var User in users)
        {
            var Roles = await _userManager.GetRolesAsync(User);
            GebruikerMetRoles gebruikerMetRoles = new GebruikerMetRoles(User.UserName);
            foreach(var Role in Roles)
            {
                gebruikerMetRoles.AddRole(Role);
            }
            results.Add(gebruikerMetRoles);
        }
        return results;
    }

    // GET: api/Account/Bob/Roles
    [HttpGet]
    [Route("{UserName}/Roles")]
    public async Task<ActionResult<GebruikerMetRoles>> GetGebruikerMetRoles(string UserName)
    {
        var _user = await _userManager.FindByNameAsync(UserName);
        var Roles = await _userManager.GetRolesAsync(_user);
        GebruikerMetRoles gebruikerMetRoles = new GebruikerMetRoles(UserName);
        foreach(var Role in Roles) gebruikerMetRoles.AddRole(Role);
        return gebruikerMetRoles;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<ActionResult<IEnumerable<IdentityUser>>> GetAllUsers()
    {
        var users = _pretparkContext.Users.ToList();
        return users;
    }
}