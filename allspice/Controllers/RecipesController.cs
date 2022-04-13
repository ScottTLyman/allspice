using System;
using System.Collections.Generic;
using allspice.Models;
using allspice.Services;
using Microsoft.AspNetCore.Mvc;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace allspice.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class RecipesController : ControllerBase
  {
    private readonly RecipesService _recipesService;
    private readonly IngredientsService _ingredientsService;
    private readonly StepsService _stepsService;

    public RecipesController(RecipesService recipesService, IngredientsService ingredientsService, StepsService stepsService)
    {
      _recipesService = recipesService;
      _ingredientsService = ingredientsService;
      _stepsService = stepsService;
    }
    // Get All Recipes
    [HttpGet]
    public ActionResult<List<Recipe>> GetAll()
    {
      try
      {
        List<Recipe> recipes = _recipesService.GetAll();
        return Ok(recipes);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // Get Recipe By Id
    [HttpGet("{id}")]
    public ActionResult<Recipe> GetById(int id)
    {
      try
      {
        Recipe recipe = _recipesService.GetById(id);
        return Ok(recipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // Get Ingredients by Recipe
    [HttpGet("{id}/ingredients")]
    public ActionResult<List<Recipe>> getRecipeIngredients(int id)
    {
      try
      {
        Recipe recipe = _recipesService.GetById(id);
        return Ok(_ingredientsService.GetRecipeIngredients(recipe.Id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // Get Steps by Recipe
    [HttpGet("{id}/steps")]
    public ActionResult<List<Recipe>> getRecipeSteps(int id)
    {
      try
      {
        Recipe recipe = _recipesService.GetById(id);
        return Ok(_stepsService.GetRecipeSteps(recipe.Id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // Create Recipe
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Recipe>> Create([FromBody] Recipe recipeData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        recipeData.CreatorId = userInfo.Id;
        Recipe recipe = _recipesService.Create(recipeData);
        recipe.Creator = userInfo;
        return Created($"api/recipes/{recipe.Id}", recipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // Delete Recipe
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Remove(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_recipesService.Remove(id, userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}