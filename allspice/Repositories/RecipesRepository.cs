using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using allspice.Models;
using System;

namespace allspice.Repositories
{

  public class RecipesRepository
  {
    private readonly IDbConnection _db;

    public RecipesRepository(IDbConnection db)
    {
      _db = db;
    }
    // Get all Recipes
    internal List<Recipe> GetAll()
    {
      string sql = @"
      SELECT r.*,
      a.*
      FROM recipes r
      JOIN accounts a WHERE a.id = r.creatorId
      ";
      return _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
      {
        //NOTE this populates the creator
        recipe.Creator = account;
        return recipe;
      }).ToList();
    }
    // Get Recipe by Id
    internal Recipe GetById(int id)
    {
      string sql = @"
      SELECT *
      FROM recipes
      WHERE id = @id;
      ";
      return _db.QueryFirstOrDefault<Recipe>(sql, new { id });
    }
    // Create new Recipe
    internal Recipe Create(Recipe recipeData)
    {
      string sql = @"
      INSERT INTO recipes
      (title, subtitle, category, creatorId)
      VALUES
      (@Title, @Subtitle, @Category, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, recipeData);
      recipeData.Id = id;
      return recipeData;
    }
    // Delete Recipe
    internal string Remove(int id)
    {
      string sql = @"
      DELETE FROM recipes
      WHERE id = @id
      LIMIT 1;
      ";
      int rowsAffected = _db.Execute(sql, new { id });
      if (rowsAffected > 0)
      {
        return "Recipe deleted";
      }
      throw new Exception("Could not delete");
    }
  }
}