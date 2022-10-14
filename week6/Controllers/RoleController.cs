using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("new")]
    public async Task<ActionResult> NewRole([FromBody] RoleDTO roleDTO)
    {
        if(!(await _roleManager.RoleExistsAsync(roleDTO.Name)))
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = roleDTO.Name});
            return StatusCode(201);
        }
        return StatusCode(500);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<IdentityRole>>> GetRole()
    {
        if(_roleManager.Roles == null)
        {
            return NotFound();
        }
        return _roleManager.Roles.ToList();
    }
}

public class RoleDTO
{
    public string Name {get; set;}

    public RoleDTO(string name)
    {
        Name = name;
    }
}