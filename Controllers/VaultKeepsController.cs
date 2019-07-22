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
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepRepository _repo;
    public VaultKeepsController(VaultKeepRepository repo)
    {
      _repo = repo;
    }
    // GET api/teams
    // [HttpGet]
    // public ActionResult<IEnumerable<VaultKeep>> Get()
    // {
    //   try
    //   {
    //     return Ok(_repo.GetAll());
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e);
    //   }
    // }

    // GET api/vaultkeeps/5
    [Authorize]
    [HttpGet("{vaultId}")]
    public ActionResult<IEnumerable<Keep>> Get(int vaultId)
    {
      try
      {
        var userId = HttpContext.User.FindFirstValue("Id"); // THIS IS HOW YOU GET THE ID of the currently logged in user

        return Ok(_repo.GetKeepsByVaultId(vaultId, userId));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // POST api/teams
    [Authorize]
    [HttpPost]
    public ActionResult<VaultKeep> Post([FromBody] VaultKeep value)
    {
      try
      {
        var userId = HttpContext.User.FindFirstValue("Id");
        value.UserId = userId;
        return Ok(_repo.Create(value));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // PUT api/teams/5
    // [HttpPut("{id}")]
    // public ActionResult<VaultKeep> Put(int id, [FromBody] VaultKeep value)
    // {
    //   try
    //   {
    //     value.Id = id;
    //     //evaluate and determine winner

    //     return Ok(_repo.Update(value));
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e);
    //   }
    // }

    // DELETE api/teams/5
    [Authorize]
    [HttpPut]
    public ActionResult<string> Delete(VaultKeep value)
    {
      try
      {
        var userId = HttpContext.User.FindFirstValue("Id");
        value.UserId = userId;
        return Ok(_repo.Delete(value));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }
  }
}
