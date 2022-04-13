using System;
using System.Collections.Generic;
using allspice.Models;
using allspice.Repositories;

namespace allspice.Services
{
  public class RecipesService
  {
    private readonly RecipesRepository _recRepo;

    public RecipesService(RecipesRepository recRepo)
    {
      _recRepo = recRepo;
    }
    // Get all Recipes
    internal List<Recipe> GetAll()
    {
      return _recRepo.GetAll();
    }
    // Get Recipe by Id
    internal Recipe GetById(int id)
    {
      Recipe found = _recRepo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }
    //Create New Recipe
    internal Recipe Create(Recipe recipeData)
    {
      return _recRepo.Create(recipeData);
    }
    // Delete Recipe
    internal string Remove(int id, Account user)
    {
      Recipe recipe = _recRepo.GetById(id);
      if (recipe.CreatorId != user.Id)
      {
        throw new Exception("You are not allowed to do this");
      }
      return _recRepo.Remove(id);
    }


  }
}