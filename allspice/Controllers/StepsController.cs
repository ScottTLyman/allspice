using System;
using System.Threading.Tasks;
using allspice.Models;
using allspice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace allspice.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class StepsController : ControllerBase
  {
    private readonly StepsService _stepsService;
    public StepsController(StepsService stepsService)
    {
      _stepsService = stepsService;
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Step>> Create([FromBody] Step stepData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Step step = _stepsService.Create(userInfo, stepData);
        return Created($"api/steps/{step.Id}", step);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]
    public ActionResult<Step> GetById(int id)
    {
      try
      {
        Step step = _stepsService.GetById(id);
        return Ok(step);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Step>> Update([FromBody] Step updateData, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Step update = _stepsService.GetById(id);
        updateData.Id = id;
        updateData.RecipeId = update.RecipeId;
        Step step = _stepsService.Update(userInfo, updateData);
        return Ok(updateData);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Remove(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _stepsService.Remove(id, userInfo);
        return Ok("Step removed");
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);
      }
    }
  }


}