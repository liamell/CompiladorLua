using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladorLua
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Bienvenido al compilador de Lua. Escribe tu código Lua:");

            while (true)
            {
                Console.Write(">> ");
                string code = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(code))
                {
                    break; // Salir si el código está vacío
                }

                try
                {
                    // Paso 1: Análisis Léxico
                    var lexer = new Lexer(code);
                    var tokens = lexer.Tokenize();

                    Console.WriteLine("Tokens generados:");
                    foreach (var token in tokens)
                    {
                        Console.WriteLine($"Tipo: {token.Type}, Valor: {token.Value}");
                    }

                    // Paso 2: Análisis Sintáctico
                    var parser = new Parser(tokens);
                    var ast = parser.ParseExpression();

                    // Paso 3: Evaluación
                    var interpreter = new Interpreter();
                    double result = interpreter.Evaluate(ast);

                    Console.WriteLine($"Resultado: {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            Console.WriteLine("Fin del compilador.");
        }
    }

}
