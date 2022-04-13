using System;
using System.Collections.Generic;
using allspice.Models;
using allspice.Repositories;

namespace allspice.Services
{
  public class StepsService
  {
    private readonly StepsRepository _stepsRepo;
    private readonly RecipesService _recipesService;


    public StepsService(StepsRepository stepsRepo, RecipesService recipesService)
    {
      _stepsRepo = stepsRepo;
      _recipesService = recipesService;
    }
    internal Step Create(Account userInfo, Step stepData)
    {
      Recipe recipe = _recipesService.GetById(stepData.RecipeId);
      if (recipe.CreatorId != userInfo.Id)
      {
        throw new Exception("You cannot add steps to this recipe");
      }
      return _stepsRepo.Create(stepData);
    }

    internal Step GetById(int id)
    {
      Step found = _stepsRepo.GetById(id);
      if (found == null)
      {
        throw new Exception("Step Id Invalid");
      }
      return found;
    }
    internal List<Step> GetRecipeSteps(int recipeId)
    {
      return _stepsRepo.GetRecipeSteps(recipeId);

    }


    internal Step Update(Account userInfo, Step updateData)
    {
      Step original = GetById(updateData.Id);
      Recipe recipe = _recipesService.GetById(original.RecipeId);
      if (recipe.CreatorId != userInfo.Id)
      {
        throw new Exception("You cannot edit steps");
      }
      original.Place = updateData.Place ?? original.Place;
      original.Body = updateData.Body ?? original.Body;
      _stepsRepo.Update(original);
      return original;
    }
    internal void Remove(int id, Account userInfo)
    {
      Step toRemove = GetById(id);
      Recipe recipe = _recipesService.GetById(toRemove.RecipeId);
      if (recipe.CreatorId != userInfo.Id)
      {
        throw new Exception("You cannot delete this");
      }
      _stepsRepo.Remove(id);
    }
  }
}