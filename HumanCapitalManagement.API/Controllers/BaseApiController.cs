using HumanCapitalManagement.Service.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanCapitalManagement.API.Controllers;

[ApiController]
[CustomAuthorize]
public class BaseApiController : ControllerBase
{
}
