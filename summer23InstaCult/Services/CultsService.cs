using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace summer23InstaCult.Services;

public class CultsService
{
  private readonly CultsRepository _cultsRepository;

  public CultsService(CultsRepository cultsRepository)
  {
    _cultsRepository = cultsRepository;
  }

  internal Cult CreateCult(Cult cultData)
  {
    int cultId = _cultsRepository.CreateCult(cultData);
    Cult cult = GetCultById(cultId);
    return cult;
  }

  internal List<Cult> GetCults()
  {
    List<Cult> cults = _cultsRepository.GetCults();
    return cults;
  }

  internal Cult GetCultById(int cultId)
  {
    Cult cult = _cultsRepository.GetCultById(cultId);
    if (cult == null)
    {
      throw new Exception("This cult does not exist.... yet....");
    }
    return cult;
  }
}
