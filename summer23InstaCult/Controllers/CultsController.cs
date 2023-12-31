using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace summer23InstaCult.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CultsController : ControllerBase
{
  private readonly CultsService _cultsService;
  private readonly Auth0Provider _auth;
  private readonly CultMembersService _cultMembersService;

  public CultsController(CultsService cultsService, Auth0Provider auth, CultMembersService cultMembersService)
  {
    _cultsService = cultsService;
    _auth = auth;
    _cultMembersService = cultMembersService;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Cult>> CreateCult([FromBody] Cult cultData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      cultData.LeaderId = userInfo.Id;
      Cult cult = _cultsService.CreateCult(cultData);
      return Ok(cult);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet]
  public ActionResult<List<Cult>> GetCults()
  {
    try
    {
      List<Cult> cults = _cultsService.GetCults();
      return Ok(cults);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{cultId}")]

  public ActionResult<Cult> GetCultById(int cultId)
  {
    try
    {
      Cult cult = _cultsService.GetCultById(cultId);
      return Ok(cult);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{cultId}/cultMembers")]
  public ActionResult<List<Cultist>> GetCultistsByCultId(int cultId)
  {
    try
    {
      List<Cultist> cultists = _cultMembersService.GetCultistsByCultId(cultId);
      return Ok(cultists);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


}
