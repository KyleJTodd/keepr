using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class VaultRepository
  {
    private readonly IDbConnection _db;

    public VaultRepository(IDbConnection db)
    {
      _db = db;
    }

    // public IEnumerable<Vault> GetAll()
    // {
    //   return _db.Query<Vault>("SELECT * FROM vaults");
    // }

    public Vault GetById(int id)
    {
      string query = "SELECT * FROM vaults WHERE id = @Id";
      Vault data = _db.QueryFirstOrDefault<Vault>(query, new { id });
      if (data == null) throw new Exception("Invalid ID");
      return data;
    }
    public IEnumerable<Vault> GetByUserId(string id)
    {
      string query = @"
                SELECT * FROM vaults
                WHERE userId = @id 
            ";
      return _db.Query<Vault>(query, new { id });
    }

    public Vault Create(Vault value)
    {
      string query = @"
            INSERT INTO vaults (name, description, userId)
            VALUES (@Name, @Description, @UserId);
            SELECT LAST_INSERT_ID();
            ";
      int id = _db.ExecuteScalar<int>(query, value);
      value.Id = id;
      return value;
    }

    public Vault Update(Vault value)
    {
      string query = @"
            UPDATE Vaults 
            SET
            name = @Name,
            description = @Description
            WHERE id = @Id ;
            SELECT * FROM Vaults WHERE id = @Id;
           ";
      return _db.QueryFirstOrDefault<Vault>(query, value);
    }

    internal object Delete(int id)
    {
      string query = "DELETE FROM vaults WHERE id = @Id;";
      int changedRows = _db.Execute(query, new { id });
      if (changedRows < 1) throw new Exception("Invalid Id");
      return "Successfully Deleted Vault";
    }
  }
}