using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class KeepsController : ControllerBase
  {
    private readonly KeepRepository _repo;
    public KeepsController(KeepRepository repo)
    {
      _repo = repo;
    }
    // GET api/teams
    [HttpGet]
    public ActionResult<IEnumerable<Keep>> Get()
    {
      try
      {
        return Ok(_repo.GetAll());
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }
    [Authorize]
    [HttpGet("user")]
    public ActionResult<IEnumerable<Keep>> GetByUser()
    {
      try
      {
        var userid = HttpContext.User.FindFirstValue("Id"); // THIS IS HOW YOU GET THE ID of the currently logged in user
        return Ok(_repo.GetByUserId(userid));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // GET api/teams/5
    [HttpGet("{id}")]
    public ActionResult<Keep> Get(int id)
    {
      try
      {
        return Ok(_repo.GetById(id));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // POST api/keeps
    [Authorize]
    [HttpPost]
    public ActionResult<Keep> Post([FromBody] Keep value)
    {
      try
      {
        var id = HttpContext.User.FindFirstValue("Id"); // THIS IS HOW YOU GET THE ID of the currently logged in user
        value.UserId = id;
        return Ok(_repo.Create(value));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // PUT api/keeps/5
    [Authorize]
    [HttpPut("{id}")]
    public ActionResult<Keep> Put(int id, [FromBody] Keep value)
    {
      try
      {
        var userid = HttpContext.User.FindFirstValue("Id"); // THIS IS HOW YOU GET THE ID of the currently logged in user
        value.UserId = userid;
        value.Id = id;
        return Ok(_repo.Update(value));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // DELETE api/teams/5
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(int id)
    {
      try
      {
        var userid = HttpContext.User.FindFirstValue("Id");

        return Ok(_repo.Delete(id, userid));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }
  }
}
