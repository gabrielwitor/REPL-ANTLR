using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// Resultado de uma operação de atribuição de variável
/// </summary>
public class VariableResult
{
    public bool Success { get; }
    public string ErrorMessage { get; }
    public double Value { get; }

    public VariableResult(bool success, double value = 0, string errorMessage = "")
    {
        Success = success;
        Value = value;
        ErrorMessage = errorMessage;
    }

    public static VariableResult CreateSuccess(double value)
    {
        return new VariableResult(true, value);
    }

    public static VariableResult CreateError(string errorMessage)
    {
        return new VariableResult(false, 0, errorMessage);
    }
}

/// <summary>
/// Classe responsável por gerenciar variáveis na calculadora
/// </summary>
public class VariableManager
{
    private readonly Dictionary<string, double> _variables = new Dictionary<string, double>();
    
    // Palavras reservadas que não podem ser usadas como nomes de variáveis
    private static readonly HashSet<string> _reservedWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "quit", "vars", "clear", "help", "exit", "list", "show", "remove", "delete",
        "pi", "e", "inf", "nan", "infinity", "true", "false", "null"
    };

    // Regex para validar nomes de variáveis (deve começar com letra ou _, seguido de letras, números ou _)
    private static readonly Regex _validNameRegex = new Regex(@"^[a-zA-Z_][a-zA-Z0-9_]*$", RegexOptions.Compiled);

    /// <summary>
    /// Define o valor de uma variável com validação
    /// </summary>
    /// <param name="name">Nome da variável</param>
    /// <param name="value">Valor da variável</param>
    /// <returns>Resultado da operação</returns>
    public VariableResult SetVariable(string name, double value)
    {
        // Validar nome da variável
        var nameValidation = ValidateVariableName(name);
        if (!nameValidation.Success)
        {
            return nameValidation;
        }

        // Validar valor
        var valueValidation = ValidateVariableValue(value);
        if (!valueValidation.Success)
        {
            return valueValidation;
        }

        _variables[name] = value;
        return VariableResult.CreateSuccess(value);
    }

    /// <summary>
    /// Método legacy para compatibilidade (lança exceções)
    /// </summary>
    /// <param name="name">Nome da variável</param>
    /// <param name="value">Valor da variável</param>
    /// <exception cref="ArgumentException">Quando o nome ou valor são inválidos</exception>
    public void SetVariableLegacy(string name, double value)
    {
        var result = SetVariable(name, value);
        if (!result.Success)
        {
            throw new ArgumentException(result.ErrorMessage);
        }
    }

    /// <summary>
    /// Obtém o valor de uma variável
    /// </summary>
    /// <param name="name">Nome da variável</param>
    /// <returns>Valor da variável</returns>
    /// <exception cref="KeyNotFoundException">Quando a variável não existe</exception>
    public double GetVariable(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(ErrorMessages.InvalidVariableName(name ?? ""));
        }

        if (_variables.TryGetValue(name, out double value))
        {
            return value;
        }
        
        throw new KeyNotFoundException(ErrorMessages.UndefinedVariable(name));
    }

    /// <summary>
    /// Tenta obter o valor de uma variável
    /// </summary>
    /// <param name="name">Nome da variável</param>
    /// <param name="value">Valor da variável se encontrada</param>
    /// <returns>True se a variável foi encontrada</returns>
    public bool TryGetVariable(string name, out double value)
    {
        value = 0;
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }
        
        return _variables.TryGetValue(name, out value);
    }

    /// <summary>
    /// Verifica se uma variável existe
    /// </summary>
    /// <param name="name">Nome da variável</param>
    /// <returns>True se a variável existe, false caso contrário</returns>
    public bool HasVariable(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }
        
        return _variables.ContainsKey(name);
    }

    /// <summary>
    /// Remove uma variável
    /// </summary>
    /// <param name="name">Nome da variável</param>
    /// <returns>True se a variável foi removida, false se não existia</returns>
    public bool RemoveVariable(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }
        
        return _variables.Remove(name);
    }

    /// <summary>
    /// Lista todas as variáveis definidas
    /// </summary>
    /// <returns>Dicionário com todas as variáveis</returns>
    public Dictionary<string, double> GetAllVariables()
    {
        return new Dictionary<string, double>(_variables);
    }

    /// <summary>
    /// Limpa todas as variáveis
    /// </summary>
    public void Clear()
    {
        _variables.Clear();
    }

    /// <summary>
    /// Obtém o número de variáveis definidas
    /// </summary>
    public int Count => _variables.Count;

    /// <summary>
    /// Valida o nome de uma variável
    /// </summary>
    /// <param name="name">Nome da variável</param>
    /// <returns>Resultado da validação</returns>
    private VariableResult ValidateVariableName(string name)
    {
        // Verificar se é nulo ou vazio
        if (string.IsNullOrWhiteSpace(name))
        {
            return VariableResult.CreateError(ErrorMessages.InvalidVariableName("Nome vazio"));
        }

        // Verificar se é muito longo
        if (name.Length > 50)
        {
            return VariableResult.CreateError(ErrorMessages.InvalidVariableName($"Nome muito longo (máximo 50 caracteres): {name}"));
        }

        // Verificar se segue o padrão válido
        if (!_validNameRegex.IsMatch(name))
        {
            return VariableResult.CreateError(ErrorMessages.InvalidVariableName($"Nome inválido. Use apenas letras, números e _ (deve começar com letra ou _): {name}"));
        }

        // Verificar se é palavra reservada
        if (_reservedWords.Contains(name))
        {
            return VariableResult.CreateError(ErrorMessages.InvalidVariableName($"Nome reservado não pode ser usado: {name}"));
        }

        return VariableResult.CreateSuccess(0);
    }

    /// <summary>
    /// Valida o valor de uma variável
    /// </summary>
    /// <param name="value">Valor da variável</param>
    /// <returns>Resultado da validação</returns>
    private VariableResult ValidateVariableValue(double value)
    {
        // Verificar se é NaN
        if (double.IsNaN(value))
        {
            return VariableResult.CreateError("Valor inválido: NaN (Not a Number)");
        }

        // Verificar se é infinito
        if (double.IsInfinity(value))
        {
            return VariableResult.CreateError($"Valor inválido: {(double.IsPositiveInfinity(value) ? "Infinito positivo" : "Infinito negativo")}");
        }

        return VariableResult.CreateSuccess(value);
    }

    /// <summary>
    /// Verifica se um nome é uma palavra reservada
    /// </summary>
    /// <param name="name">Nome a verificar</param>
    /// <returns>True se é palavra reservada</returns>
    public static bool IsReservedWord(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && _reservedWords.Contains(name);
    }

    /// <summary>
    /// Obtém lista de palavras reservadas
    /// </summary>
    /// <returns>Lista de palavras reservadas</returns>
    public static IReadOnlyCollection<string> GetReservedWords()
    {
        return _reservedWords;
    }
} 