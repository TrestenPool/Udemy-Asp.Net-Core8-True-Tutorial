using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample1.Models;
public class Book{
    public int? Bookid {get; set;}
    public string? Author {get; set;}

    public override string ToString() {
      return $"Book id: {Bookid}, Author = {Author}";
    }
}