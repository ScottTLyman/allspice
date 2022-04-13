using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using allspice.Models;
using Dapper;

namespace allspice.Repositories
{
  public class StepsRepository
  {
    private readonly IDbConnection _db;
    public StepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Step Create(Step stepData)
    {
      string sql = @"
      INSERT INTO steps
      (place, body, recipeId)
      VALUES
      (@Place, @Body, @RecipeId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, stepData);
      stepData.Id = id;
      return stepData;
    }

    internal Step GetById(int id)
    {
      string sql = @"
      SELECT *
      FROM steps
      WHERE id = @Id;
      ";
      return _db.QueryFirstOrDefault<Step>(sql, new { id });
    }
    internal List<Step> GetRecipeSteps(int recipeId)
    {
      string sql = @"
      SELECT *
      FROM steps
      WHERE recipeId = @RecipeId;
      ";
      return _db.Query<Step>(sql, new { recipeId }).ToList();
    }

    internal void Update(Step original)
    {
      string sql = @"
      UPDATE steps
      SET
        place = @Place,
        body = @Body
      WHERE id = @Id;
      ";
      _db.Execute(sql, original);
    }

    internal string Remove(int id)
    {
      string sql = @"
      DELETE FROM steps
      WHERE id = @Id
      LIMIT 1;
      ";
      int rowsAffected = _db.Execute(sql, new { id });
      if (rowsAffected > 0)
      {
        return "Step removed";
      }
      throw new Exception("Could not delete");
    }
  }
}