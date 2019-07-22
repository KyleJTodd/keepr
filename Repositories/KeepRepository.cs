using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class KeepRepository
  {
    private readonly IDbConnection _db;

    public KeepRepository(IDbConnection db)
    {
      _db = db;
    }

    public IEnumerable<Keep> GetAll()
    {
      return _db.Query<Keep>("SELECT * FROM keeps");
    }

    public Keep GetById(int id)
    {
      string query = "SELECT * FROM keeps WHERE id = @Id";
      Keep data = _db.QueryFirstOrDefault<Keep>(query, new { id });
      if (data == null) throw new Exception("Invalid ID");
      return data;
    }
    public IEnumerable<Keep> GetByUserId(string id)
    {
      string query = @"
                SELECT * FROM keeps
                WHERE userId = @id 
            ";
      return _db.Query<Keep>(query, new { id });
    }

    public Keep Create(Keep value)
    {
      string query = @"
            INSERT INTO keeps (name, description, img, isPrivate, userId)
            VALUES (@Name, @Description, @Img, @IsPrivate, @UserId);
            SELECT LAST_INSERT_ID();
            ";
      int id = _db.ExecuteScalar<int>(query, value);
      value.Id = id;
      return value;
    }

    public Keep Update(Keep value)
    {
      string query = @"
            UPDATE keeps 
            SET
            name = @Name,
            description = @Description,
            img = @Img,
            isPrivate = @IsPrivate,
            views = @Views,
            shares = @Shares,
            keeps = @Keeps
            WHERE id = @Id ;
            SELECT * FROM keeps WHERE id = @Id;
           ";
      return _db.QueryFirstOrDefault<Keep>(query, value);
    }

    internal object Delete(int id)
    {
      string query = "DELETE FROM keeps WHERE id = @Id;";
      int changedRows = _db.Execute(query, new { id });
      if (changedRows < 1) throw new Exception("Invalid Id");
      return "Successfully Deleted Keep";
    }
  }
}