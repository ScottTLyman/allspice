using System;
using System.Collections.Generic;
using allspice.Models;
using allspice.Repositories;

namespace allspice.Services
{
  public class IngredientsService
  {
    private readonly IngredientsRepository _ingRepo;
    private readonly RecipesService _recipesService;
    public IngredientsService(IngredientsRepository ingRepo, RecipesService recipesService)
    {
      _ingRepo = ingRepo;
      _recipesService = recipesService;
    }

    internal Ingredient Create(Account userInfo, Ingredient ingredientData)
    {
      Recipe recipe = _recipesService.GetById(ingredientData.RecipeId);
      if (recipe.CreatorId != userInfo.Id)
      {
        throw new Exception("You cannot add ingredients to this recipe");
      }
      return _ingRepo.Create(ingredientData);
    }

    internal Ingredient GetById(int id)
    {
      Ingredient found = _ingRepo.GetById(id);
      if (found == null)
      {
        throw new Exception("Ingredient Id Invalid");
      }
      return found;
    }
    internal List<Ingredient> GetRecipeIngredients(int recipeId)
    {
      return _ingRepo.GetRecipeIngredients(recipeId);

    }


    internal Ingredient Update(Account userInfo, Ingredient updateData)
    {
      Ingredient original = GetById(updateData.Id);
      Recipe recipe = _recipesService.GetById(original.RecipeId);
      if (recipe.CreatorId != userInfo.Id)
      {
        throw new Exception("You cannot edit ingredients");
      }
      original.Name = updateData.Name ?? original.Name;
      original.Quantity = updateData.Quantity ?? original.Quantity;
      _ingRepo.Update(original);
      return original;
    }
    internal void Remove(int id, Account userInfo)
    {
      Ingredient toRemove = GetById(id);
      Recipe recipe = _recipesService.GetById(toRemove.RecipeId);
      if (recipe.CreatorId != userInfo.Id)
      {
        throw new Exception("You cannot delete this");
      }
      _ingRepo.Remove(id);
    }
  }
}