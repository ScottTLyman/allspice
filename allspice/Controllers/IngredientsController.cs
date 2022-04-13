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
  public class IngredientsController : ControllerBase
  {
    private readonly IngredientsService _ingredientsService;
    private readonly RecipesService _recipesService;
    public IngredientsController(IngredientsService ingredientsService, RecipesService recipesService)
    {
      _ingredientsService = ingredientsService;
      _recipesService = recipesService;
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Ingredient>> Create([FromBody] Ingredient ingredientData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Ingredient ingredient = _ingredientsService.Create(userInfo, ingredientData);
        return Created($"api/ingredients/{ingredient.Id}", ingredient);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]
    public ActionResult<Ingredient> GetById(int id)
    {
      try
      {
        Ingredient ingredient = _ingredientsService.GetById(id);
        return Ok(ingredient);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Ingredient>> Update([FromBody] Ingredient updateData, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Ingredient update = _ingredientsService.GetById(id);
        updateData.Id = id;
        updateData.RecipeId = update.RecipeId;
        Ingredient ingredient = _ingredientsService.Update(userInfo, updateData);
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
        _ingredientsService.Remove(id, userInfo);
        return Ok("Ingredient removed");
      }
      catch (Exception e)
      {

        return BadRequest(e.Message);
      }
    }
  }


}