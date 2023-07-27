using Microsoft.AspNetCore.Mvc;
using ProductApiVSC.Models;
using Microsoft.AspNetCore.Authorization;  
using Microsoft.EntityFrameworkCore;
using ProductApiVSC.Container;
using ProductApiVSC.Entity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace ProductApiVSC.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
  private readonly LearnDbContext _DBContext;
  private readonly JwtSettings _JwtSettings;

  public UserController(LearnDbContext dbContext, IOptions<JwtSettings> options)
  {
    this._DBContext = dbContext;
    this._JwtSettings = options.Value;
  }

  [HttpPost("Authentitcate")]
  public async Task<IActionResult> Authentitcate([FromBody] UserCred userCred)
  {
    var user = await this._DBContext.TblUsers.FirstOrDefaultAsync(u => u.Name == userCred.Username && u.Password == userCred.Password);

    if(user == null){
      return Unauthorized();
    }

    // GENERATE TOKEN 
    var tokenhandler = new JwtSecurityTokenHandler();
    var tokenkey = Encoding.UTF8.GetBytes(this._JwtSettings.securitykey);
    var tokendesc = new SecurityTokenDescriptor{
      Subject = new ClaimsIdentity(
        new Claim[] { new Claim(ClaimTypes.Name, user.Name), new Claim(ClaimTypes.Role, user.Role) }
      ),
      Expires = DateTime.Now.AddHours(20),
      // Expires = DateTime.Now.AddSeconds(20),
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
    };
    var token = tokenhandler.CreateToken(tokendesc);
    string finaltoken = tokenhandler.WriteToken(token);

    return Ok(finaltoken);
  }
}