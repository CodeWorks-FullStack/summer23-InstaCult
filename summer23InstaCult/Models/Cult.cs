using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace summer23InstaCult.Models;

public class Cult : RepoItem<int>
{
  // public int Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public int Fee { get; set; }
  public string CoverImg { get; set; }
  public string LeaderId { get; set; }
  public Profile Leader { get; set; }
  public int CultistCount { get; set; }
  // public DateTime CreatedAt { get; set; }
  // public DateTime UpdatedAt { get; set; }
}
