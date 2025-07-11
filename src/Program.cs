using Antlr4.Runtime;
using System.Globalization;

public class Program
{
    public static void Main(string[] args)
    {
        // Configurar cultura para usar ponto como separador decimal
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        
        Console.WriteLine("=== REPL Calculadora com ANTLR4 ===");
        Console.WriteLine("Digite uma expressão matemática ou 'sair' para terminar.");
        Console.WriteLine();
        
        while (true)
        {
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input) || input.ToLower() == "sair")
            {
                Console.WriteLine("Encerrando.");
                break;
            }

            try
            {
                // Criar o stream de entrada
                var inputStream = new AntlrInputStream(input);
                
                // Criar o lexer
                var lexer = new GramaticaLexer(inputStream);
                
                // Criar o stream de tokens
                var tokenStream = new CommonTokenStream(lexer);
                
                // Criar o parser
                var parser = new GramaticaParser(tokenStream);
                
                // Fazer o parsing da expressão
                var tree = parser.start();
                
                // Verificar se houve erros de parsing
                if (parser.NumberOfSyntaxErrors > 0)
                {
                    Console.WriteLine("Erro de sintaxe na expressão!");
                    continue;
                }
                
                // Criar o visitor e calcular o resultado
                var visitor = new GramaticaCalculatorVisitor();
                var result = visitor.Visit(tree.expr());

                Console.WriteLine($"= {result}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
            }
        }
    }
}
