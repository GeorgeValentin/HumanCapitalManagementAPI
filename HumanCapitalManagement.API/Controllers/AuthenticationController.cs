using HumanCapitalManagement.Service.Services;
using HumanCapitalManagement.Utilities.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanCapitalManagement.API.Controllers;

public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
	{
        _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
    }

    [HttpPost("authentication/token")]
    [AllowAnonymous]
    public IActionResult Token([FromBody] TokenRequest request)
    {
        string? tokenResult = _authenticationService.CreateTokenLocally(request)!;

        if(tokenResult == null)
            return Unauthorized(new { Message = "Invalid credentials!" });

        return Ok(new TokenResponse
        {
            AccessToken = tokenResult,
            ExpiresIn = "24h",
            TokenType = "Bearer"
        });
    }
}
