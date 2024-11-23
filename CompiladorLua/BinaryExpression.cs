using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladorLua
{
    public class BinaryExpression : AstNode
    {
        public AstNode Left { get; set; }
        public string Operator { get; set; }
        public AstNode Right { get; set; }
    }

}
