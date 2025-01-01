using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartialViewsExample.Models;

public class ListModel{
  public string ListTitle { get; set; } = "";
  public List<string> ListItems { get; set; } = new List<string>();
}