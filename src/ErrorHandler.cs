using Antlr4.Runtime;
using System.Text;

/// <summary>
/// Classe responsável por capturar e formatar erros de parsing
/// </summary>
public class CustomErrorListener : BaseErrorListener, IAntlrErrorListener<int>
{
    public List<string> Errors { get; private set; } = new List<string>();
    public bool HasErrors => Errors.Count > 0;

    public override void SyntaxError(
        TextWriter output,
        IRecognizer recognizer,
        IToken offendingSymbol,
        int line,
        int charPositionInLine,
        string msg,
        RecognitionException e)
    {
        var errorMsg = FormatSyntaxError(offendingSymbol, charPositionInLine, msg);
        Errors.Add(errorMsg);
    }

    // Implementação específica para o lexer
    public void SyntaxError(
        TextWriter output,
        IRecognizer recognizer,
        int offendingSymbol,
        int line,
        int charPositionInLine,
        string msg,
        RecognitionException e)
    {
        var errorMsg = FormatLexerError(offendingSymbol, charPositionInLine, msg);
        Errors.Add(errorMsg);
    }

    private string FormatSyntaxError(IToken offendingSymbol, int position, string message)
    {
        var tokenText = offendingSymbol?.Text ?? "?";
        
        // Mensagens de erro específicas baseadas no contexto
        if (message.Contains("expecting"))
        {
            if (message.Contains("')'"))
            {
                return $"Parênteses não fechados.";
            }
            if (message.Contains("NUMBER"))
            {
                return $"Número esperado na posição {position + 1}. Encontrado: '{tokenText}'";
            }
            if (message.Contains("'('"))
            {
                return $"Expressão inválida. Esperado número ou '(' na posição {position + 1}.";
            }
            if (message.Contains("'='"))
            {
                return $"Esperado '=' para atribuição. Encontrado: '{tokenText}'";
            }
        }

        if (message.Contains("extraneous input"))
        {
            return $"Símbolo inesperado '{tokenText}' na posição {position + 1}.";
        }

        if (message.Contains("mismatched input"))
        {
            return $"Símbolo incorreto '{tokenText}' na posição {position + 1}.";
        }

        if (message.Contains("no viable alternative"))
        {
            return $"Expressão inválida próxima à posição {position + 1}.";
        }

        // Mensagem genérica se não conseguir identificar o erro específico
        return $"Erro de sintaxe na posição {position + 1}: {message}";
    }

    private string FormatLexerError(int offendingSymbol, int position, string message)
    {
        var charSymbol = offendingSymbol >= 0 ? ((char)offendingSymbol).ToString() : "?";
        
        if (message.Contains("token recognition error"))
        {
            return $"Caractere inválido '{charSymbol}' na posição {position + 1}.";
        }

        return $"Erro de análise léxica na posição {position + 1}: {message}";
    }

    public void Clear()
    {
        Errors.Clear();
    }
}

/// <summary>
/// Classe para gerenciar diferentes tipos de erros da calculadora
/// </summary>
public static class ErrorMessages
{
    public static string DivisionByZero()
    {
        return "Erro: Divisão por zero não é permitida!";
    }

    public static string InvalidNumber(string input)
    {
        return $"Erro: '{input}' não é um número válido.";
    }

    public static string EmptyExpression()
    {
        return "Expressão vazia. Digite uma operação matemática.";
    }

    public static string UnexpectedError(string details)
    {
        return $"Erro inesperado: {details}";
    }

    public static string InvalidOperation(string operation)
    {
        return $"Operação inválida: {operation}";
    }

    public static string UndefinedVariable(string variableName)
    {
        return $"Variável '{variableName}' não foi definida.";
    }

    public static string InvalidVariableName(string variableName)
    {
        return $"Nome de variável inválido: '{variableName}'.";
    }

    public static string ShowWelcome()
    {
        var welcome = new StringBuilder();
        welcome.AppendLine("=== REPL Calculadora com ANTLR4 ===");
        welcome.AppendLine();
        
        return welcome.ToString();
    }

    public static string ShowVariables(Dictionary<string, double> variables)
    {
        if (variables.Count == 0)
        {
            return "Nenhuma variável definida.";
        }

        var sb = new StringBuilder();
        sb.AppendLine($"Variáveis definidas ({variables.Count}):");
        
        foreach (var variable in variables.OrderBy(v => v.Key))
        {
            sb.AppendLine($"   • {variable.Key} = {variable.Value}");
        }
        
        return sb.ToString();
    }

    public static string VariablesCleared()
    {
        return "Todas as variáveis foram removidas.";
    }
} 