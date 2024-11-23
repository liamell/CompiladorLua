using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladorLua
{
    public class NumberLiteral : AstNode
    {
        public string Value { get; set; }
    }

}
