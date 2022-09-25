using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab1.Models
{
    internal class Variable
    {
        public string dataType { get; private set; }
        public string name { get; private set; }
        public int id { get; private set; }
        public Variable(int id, string dataType, string name)
        {
            this.dataType = dataType;
            this.name = name;
            this.id = id;
        }

        public override string ToString()
        {
            return $"<{id}> <{dataType}> <{name}>";
        }
    }
}
