using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Models
{
    internal class Identifier
    {
        public int id { get; private set; }
        public string name { get; private set; }
        public Identifier(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
        public override string ToString()
        {
            return $"<{id}> <{name}>";
        }
    }
}
