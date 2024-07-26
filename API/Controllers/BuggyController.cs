using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;

public class BuggyController(DataContext context) : BaseApiController
{

[Authorize]
[HttpGet("auth")]
public ActionResult<string> GetAuth()
{
    return "secret text";
}

[HttpGet("not-found")]
public ActionResult<AppUser> GetNotFound()
{
    var thing = context.Users.Find(-1);

    if (thing == null) return NotFound();

    return thing;
}
[HttpGet("server-error")]
public ActionResult<string> GetServerError()
{
    var thing = context.Users.Find(-1);

    var thingToReturn = thing!.ToString();

            return thingToReturn;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was not a good request");
        }
}
