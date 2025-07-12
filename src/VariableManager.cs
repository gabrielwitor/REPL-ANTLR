using System.Collections.Generic;

/// <summary>
/// Classe responsável por gerenciar variáveis na calculadora
/// </summary>
public class VariableManager
{
    private readonly Dictionary<string, double> _variables = new Dictionary<string, double>();

    /// <summary>
    /// Define o valor de uma variável
    /// </summary>
    /// <param name="name">Nome da variável</param>
    /// <param name="value">Valor da variável</param>
    public void SetVariable(string name, double value)
    {
        _variables[name] = value;
    }

    /// <summary>
    /// Obtém o valor de uma variável
    /// </summary>
    /// <param name="name">Nome da variável</param>
    /// <returns>Valor da variável</returns>
    /// <exception cref="KeyNotFoundException">Quando a variável não existe</exception>
    public double GetVariable(string name)
    {
        if (_variables.TryGetValue(name, out double value))
        {
            return value;
        }
        
        throw new KeyNotFoundException($"Variável '{name}' não foi definida.");
    }

    /// <summary>
    /// Verifica se uma variável existe
    /// </summary>
    /// <param name="name">Nome da variável</param>
    /// <returns>True se a variável existe, false caso contrário</returns>
    public bool HasVariable(string name)
    {
        return _variables.ContainsKey(name);
    }

    /// <summary>
    /// Remove uma variável
    /// </summary>
    /// <param name="name">Nome da variável</param>
    /// <returns>True se a variável foi removida, false se não existia</returns>
    public bool RemoveVariable(string name)
    {
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
} 