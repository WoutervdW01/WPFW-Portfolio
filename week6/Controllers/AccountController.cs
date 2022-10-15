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
}