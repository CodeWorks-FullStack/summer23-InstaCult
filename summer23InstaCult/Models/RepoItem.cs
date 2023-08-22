using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace summer23InstaCult.Models;

// abstract cannot be instantiated 
public abstract class RepoItem<T>
{
  public T Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}
