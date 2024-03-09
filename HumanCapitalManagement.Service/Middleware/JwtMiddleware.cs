using HumanCapitalManagement.Service.Authorization.Jwt;
using Microsoft.AspNetCore.Http;

namespace HumanCapitalManagement.Service.Middleware;
public class JwtMiddleware
{
	private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext context, IJwtUtils jwtUtils)
	{
		var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

		var clientId = jwtUtils.ValidateToken(token!);

		if (clientId != null)
		{
			context.Items["clientId"] = clientId;
        }

		await _next(context);
	}
}
