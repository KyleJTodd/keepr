using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Repositories;
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
    [HttpGet("{id}")]
    public ActionResult<VaultKeep> Get(int id)
    {
      try
      {
        return Ok(_repo.GetKeepsByVaultId(id));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // POST api/teams
    [HttpPost]
    public ActionResult<VaultKeep> Post([FromBody] VaultKeep value)
    {
      try
      {
        return Ok(_repo.Create(value));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // PUT api/teams/5
    [HttpPut("{id}")]
    public ActionResult<VaultKeep> Put(int id, [FromBody] VaultKeep value)
    {
      try
      {
        value.Id = id;
        //evaluate and determine winner

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
        return Ok(_repo.Delete(id));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }
  }
}
