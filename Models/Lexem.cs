using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Models
{
    internal class Lexem
    {
        public enum Types : int
        {
            ParsingError = -1,
            DataType = 0,
            Variable = 1,
            Delimeter = 2,
            Identifier = 3,
            Constant = 4,
            Operation = 5,
            Keyword = 6,
            Literal = 7,
        }

        public Types type { get; private set; }
        public int id { get; private set; }
        public string desc { get; private set; }

        public Lexem(Types type, int id, string desc)
        {
            this.type = type;
            this.id = id;
            this.desc = desc;
        }

        public override string ToString()
        {
            return $"lexem type: {type};\t lexem id: {id};\t value: {desc}";
        }
    }
}
