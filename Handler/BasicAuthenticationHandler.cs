using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Net.Http.Headers;
using System.Text;
using ProductApiVSC.Models;
using System.Security.Claims;

namespace ProductApiVSC.Handler;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>{

  private readonly LearnDbContext _DBContext;

  public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, 
    ILoggerFactory logger, 
    UrlEncoder encoder, 
    ISystemClock clock,
    LearnDbContext dBContext) : base(options, logger, encoder, clock)
    {
      _DBContext = dBContext;
    }

  protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
  {
    if(!Request.Headers.ContainsKey("Authorization")){
      return AuthenticateResult.Fail("Missing Authorization Header");
    } else {
      Console.WriteLine("entro");
      var _headerValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
      var bytes = Convert.FromBase64String(_headerValue.Parameter!=null?_headerValue.Parameter:string.Empty);
      string credentials = Encoding.UTF8.GetString(bytes);

      if(!string.IsNullOrEmpty(credentials)){
        string[] array = credentials.Split(":");
        string username = array[0];
        string password = array[1];
        var user = await this._DBContext.TblUsers.FirstOrDefaultAsync(u => u.Name == username && u.Password == password);
        // var user = new[] { new { Username = "SA", Password = "Tandil2019" } };

        if(user == null)
          return AuthenticateResult.Fail("Unauthorized");

        // GENERATE TICKET
        var claims = new[] { new Claim(ClaimTypes.Name, username) };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
      }else {
        return AuthenticateResult.Fail("Unauthorized");
      }
    }
  }

}