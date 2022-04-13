using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using allspice.Models;
using Dapper;

namespace allspice.Repositories
{
  public class IngredientsRepository
  {
    private readonly IDbConnection _db;
    public IngredientsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Ingredient Create(Ingredient ingredientData)
    {
      string sql = @"
      INSERT INTO ingredients
      (name, quantity, recipeId)
      VALUES
      (@Name, @Quantity, @RecipeId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, ingredientData);
      ingredientData.Id = id;
      return ingredientData;
    }

    internal Ingredient GetById(int id)
    {
      string sql = @"
      SELECT *
      FROM ingredients
      WHERE id = @Id;
      ";
      return _db.QueryFirstOrDefault<Ingredient>(sql, new { id });
    }
    internal List<Ingredient> GetRecipeIngredients(int recipeId)
    {
      string sql = @"
      SELECT *
      FROM ingredients
      WHERE recipeId = @RecipeId;
      ";
      return _db.Query<Ingredient>(sql, new { recipeId }).ToList();
    }

    internal void Update(Ingredient original)
    {
      string sql = @"
      UPDATE ingredients
      SET
        name = @Name,
        quantity = @Quantity
      WHERE id = @Id;
      ";
      _db.Execute(sql, original);
    }

    internal string Remove(int id)
    {
      string sql = @"
      DELETE FROM ingredients
      WHERE id = @Id
      LIMIT 1;
      ";
      int rowsAffected = _db.Execute(sql, new { id });
      if (rowsAffected > 0)
      {
        return "Ingredient removed";
      }
      throw new Exception("Could not delete");
    }
  }
}