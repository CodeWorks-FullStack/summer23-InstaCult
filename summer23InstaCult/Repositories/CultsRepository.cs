using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace summer23InstaCult.Repositories;

public class CultsRepository
{
  private readonly IDbConnection _db;

  public CultsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal int CreateCult(Cult cultData)
  {
    string sql = @"
    INSERT INTO cults
    (name, description, fee, coverImg, leaderId)
    VALUES
    (@Name, @Description, @Fee, @CoverImg, @LeaderId);
    SELECT LAST_INSERT_ID();
    ";
    int cultId = _db.ExecuteScalar<int>(sql, cultData);
    // cultData.Id = cultId;
    return cultId;
  }


  internal List<Cult> GetCults()
  {
    string sql = @"
    SELECT
    c.*,
    COUNT(cm.id) AS cultistCount,
    acc.*
    FROM cults c
    LEFT JOIN cultMembers cm ON cm.cultId = c.id
    JOIN accounts acc ON acc.id = c.leaderId
    GROUP BY c.id;
    ";
    List<Cult> cults = _db.Query<Cult, Profile, Cult>(
      sql,
      (cult, profile) =>
    {
      cult.Leader = profile;
      return cult;
    }).ToList();
    return cults;
  }

  internal Cult GetCultById(int cultId)
  {
    string sql = @"
    SELECT 
    c.*,
    acc.*
    FROM cults c
    JOIN accounts acc ON acc.id = c.leaderId
    WHERE c.id = @cultId;
    ";
    Cult cult = _db.Query<Cult, Profile, Cult>(sql, (cult, profile) =>
    {
      cult.Leader = profile;
      return cult;
    }, new { cultId }).FirstOrDefault();
    return cult;
  }
}
