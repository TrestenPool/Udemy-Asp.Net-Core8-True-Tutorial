using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample1.Models
{
    public class Person{
        // properties
        public Guid Id { get; set; }  
        public string? Firstname { get; set; } 
        public string? Lastname { get; set; } 
        public int Age { get; set; }
    }
}