using Antlr4.Runtime;
using System.Globalization;

public class Program
{
    private static readonly VariableManager _variableManager = new VariableManager();

    public static void Main(string[] args)
    {
        // Configurar cultura para usar ponto como separador decimal
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        
        // Mostrar mensagem de boas-vindas
        Console.Write(ErrorMessages.ShowWelcome());
        
        while (true)
        {
            Console.Write("calc> ");
            var input = Console.ReadLine();

            // Verificar comandos especiais
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(ErrorMessages.EmptyExpression());
                continue;
            }

            var inputLower = input.ToLower().Trim();
            
            if (inputLower == "quit")
            {
                break;
            }


            if (inputLower == "vars")
            {
                Console.WriteLine(ErrorMessages.ShowVariables(_variableManager.GetAllVariables()));
                continue;
            }

            if (inputLower == "clear")
            {
                _variableManager.Clear();
                Console.WriteLine(ErrorMessages.VariablesCleared());
                continue;
            }

            try
            {
                var result = EvaluateExpression(input);
                Console.WriteLine($"= {result}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ErrorMessages.UnexpectedError(ex.Message));
            }
        }
    }

    private static double EvaluateExpression(string input)
    {
        // Criar o stream de entrada
        var inputStream = new AntlrInputStream(input);
        
        // Criar o lexer
        var lexer = new GramaticaLexer(inputStream);
        
        // Criar listener de erro personalizado
        var errorListener = new CustomErrorListener();
        lexer.RemoveErrorListeners();
        lexer.AddErrorListener(errorListener);
        
        // Criar o stream de tokens
        var tokenStream = new CommonTokenStream(lexer);
        
        // Criar o parser
        var parser = new GramaticaParser(tokenStream);
        
        // Adicionar listener de erro personalizado ao parser
        parser.RemoveErrorListeners();
        parser.AddErrorListener(errorListener);
        
        // Fazer o parsing da expressão
        var tree = parser.start();
        
        // Verificar se houve erros de parsing
        if (errorListener.HasErrors)
        {
            // Mostrar todos os erros encontrados
            foreach (var error in errorListener.Errors)
            {
                Console.WriteLine(error);
            }
            throw new InvalidOperationException("Erros de sintaxe encontrados.");
        }
        
        // Criar o visitor e calcular o resultado
        var visitor = new GramaticaCalculatorVisitor(_variableManager);
        return visitor.Visit(tree.statement());
    }
}
