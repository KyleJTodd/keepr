using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class VaultKeepRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepRepository(IDbConnection db)
    {
      _db = db;
    }

    // public IEnumerable<VaultKeep> GetAll()
    // {
    //   return _db.Query<VaultKeep>("SELECT * FROM vaultkeeps");
    // }

    public IEnumerable<Keep> GetKeepsByVaultId(int vaultId, string userId)
    {
      string query = @"
      SELECT * FROM vaultkeeps vk 
      INNER JOIN keeps k ON k.id = vk.keepId
      WHERE (vaultId = @vaultId AND vk.userId = @userId)";
      IEnumerable<Keep> data = _db.Query<Keep>(query, new { vaultId, userId });
      if (data == null) throw new Exception("Invalid ID");
      return data;
    }
    // public IEnumerable<VaultKeep> GetKeepsByVaultId(string id)
    // {
    //   string query = @"
    //             SELECT * FROM vaultkeeps
    //             WHERE vaultId = @id 
    //         ";
    //   return _db.Query<Vault>(query, new { id });
    // }

    public VaultKeep Create(VaultKeep value)
    {
      string query = @"
            INSERT INTO vaultkeeps (vaultId, keepId, userId)
            VALUES (@VaultId, @KeepId, @UserId);
            SELECT LAST_INSERT_ID();
            ";
      int id = _db.ExecuteScalar<int>(query, value);
      value.Id = id;
      return value;
    }

    // public Vault Update(Vault value)
    // {
    //   string query = @"
    //         UPDATE vaults 
    //         SET
    //         name = @Name,
    //         description = @Description
    //         WHERE id = @Id ;
    //         SELECT * FROM Vaults WHERE id = @Id;
    //        ";
    //   return _db.QueryFirstOrDefault<Vault>(query, value);
    // }

    public string Delete(VaultKeep value)
    {
      string query = @"DELETE FROM vaultkeeps 
      WHERE (vaultId = @VaultId AND keepId = @KeepId AND userId = @UserId);";
      int changedRows = _db.Execute(query, value);
      if (changedRows < 1) throw new Exception("Invalid Id");
      return "Successfully Deleted VaultKeep";
    }
  }
}