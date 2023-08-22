using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace summer23InstaCult.Services;

public class CultMembersService
{
  private readonly CultMembersRepository _cultMembersRepository;
  private readonly CultsService _cultsService;

  public CultMembersService(CultMembersRepository cultMembersRepository, CultsService cultsService)
  {
    _cultMembersRepository = cultMembersRepository;
    _cultsService = cultsService;
  }

  internal CultMember CreateCultMember(CultMember cultMemberData)
  {
    CultMember cultMember = _cultMembersRepository.CreateCultMember(cultMemberData);
    return cultMember;
  }

  // UTILITY METHOD: GET CULT MEMBER BY ID
  internal CultMember GetCultMemberById(int cultMemberId)
  {
    CultMember cultMember = _cultMembersRepository.GetCultMemberById(cultMemberId);
    if (cultMember == null)
    {
      throw new Exception("This cultMember does not exist.");
    }
    return cultMember;
  }

  internal string RemoveCultMember(int cultMemberId, string userId)
  {
    CultMember cultMember = GetCultMemberById(cultMemberId);
    Cult cult = _cultsService.GetCultById(cultMember.CultId);
    if (cult.LeaderId != userId)
    {
      throw new Exception("Alerting cult leader of ..... you cant do that.");
    }
    _cultMembersRepository.RemoveCultMember(cultMemberId);
    return "Member removed.";
  }

  internal List<Cultist> GetCultistsByCultId(int cultId)
  {
    List<Cultist> cultists = _cultMembersRepository.GetCultistsByCultId(cultId);
    return cultists;
  }
}
