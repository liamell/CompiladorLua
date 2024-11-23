using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladorLua
{
    public class Interpreter
    {
        public double Evaluate(AstNode node)
        {
            if (node is NumberLiteral number)
            {
                return double.Parse(number.Value);
            }
            else if (node is BinaryExpression binary)
            {
                double left = Evaluate(binary.Left);
                double right = Evaluate(binary.Right);

                return binary.Operator switch
                {
                    "+" => left + right,
                    "-" => left - right,
                    "*" => left * right,
                    "/" => left / right,
                    _ => throw new Exception("Unknown operator")
                };
            }

            throw new Exception("Unknown AST node");
        }
    }

}
