using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace summer23InstaCult.Repositories;

public class CultMembersRepository
{
  private readonly IDbConnection _db;

  public CultMembersRepository(IDbConnection db)
  {
    _db = db;
  }

  internal CultMember CreateCultMember(CultMember cultMemberData)
  {
    string sql = @"
    INSERT INTO cultMembers
    (accountId, cultId)
    VALUES
    (@AccountId, @CultId);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, cultMemberData);
    cultMemberData.Id = id;
    return cultMemberData;

  }

  internal CultMember GetCultMemberById(int cultMemberId)
  {
    string sql = @"
      SELECT 
      *
      FROM cultMembers
      WHERE id = @cultMemberId;
      ";
    CultMember cultMember = _db.QueryFirstOrDefault<CultMember>(sql, new { cultMemberId });
    return cultMember;
  }

  internal void RemoveCultMember(int cultMemberId)
  {
    string sql = "DELETE FROM cultMembers WHERE id = @cultMemberId LIMIT 1;";
    _db.Execute(sql, new { cultMemberId });
  }

  internal List<Cultist> GetCultistsByCultId(int cultId)
  {
    string sql = @"
    SELECT
    cm.*,
    acc.*
    FROM cultMembers cm
    JOIN accounts acc ON acc.id = cm.accountId
    WHERE cm.cultId = @cultId;
    ";
    List<Cultist> cultists = _db.Query<CultMember, Cultist, Cultist>(sql, (cultMember, cultist) =>
    {
      cultist.CultMemberId = cultMember.Id;
      return cultist;
    }, new { cultId }).ToList();
    return cultists;
  }
}
