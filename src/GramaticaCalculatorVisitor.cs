using Antlr4.Runtime.Misc;

/// <summary>
/// Visitor personalizado para calcular expressões matemáticas e gerenciar variáveis
/// </summary>
public class GramaticaCalculatorVisitor : GramaticaBaseVisitor<double>
{
    private readonly VariableManager _variableManager;

    public GramaticaCalculatorVisitor(VariableManager variableManager)
    {
        _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
    }

    // Visita uma atribuição de variável
    public override double VisitAtribuicao([NotNull] GramaticaParser.AtribuicaoContext context)
    {
        var variableName = context.ID().GetText();
        var value = Visit(context.expr());
        
        var result = _variableManager.SetVariable(variableName, value);
        
        if (!result.Success)
        {
            throw new ArgumentException(result.ErrorMessage);
        }
        
        return result.Value; // Retorna o valor atribuído
    }

    // Visita uma expressão (sem atribuição)
    public override double VisitExpressao([NotNull] GramaticaParser.ExpressaoContext context)
    {
        return Visit(context.expr());
    }

    // Visita uma variável
    public override double VisitVariavel([NotNull] GramaticaParser.VariavelContext context)
    {
        var variableName = context.ID().GetText();
        
        // O método GetVariable já lança exceções adequadas com mensagens de erro
        return _variableManager.GetVariable(variableName);
    }

    public override double VisitMultiplicacao_Divisao([NotNull] GramaticaParser.Multiplicacao_DivisaoContext context)
    {
        var left = Visit(context.expr(0));
        var right = Visit(context.expr(1));

        if (context.MUL() != null)
        {
            return left * right;
        }
        else if (context.DIV() != null)
        {
            if (right == 0)
            {
                throw new DivideByZeroException(ErrorMessages.DivisionByZero());
            }
            return left / right;
        }
        
        throw new InvalidOperationException(ErrorMessages.InvalidOperation("multiplicação/divisão"));
    }

    public override double VisitSoma_Subtracao([NotNull] GramaticaParser.Soma_SubtracaoContext context)
    {
        var left = Visit(context.expr(0));
        var right = Visit(context.expr(1));

        if (context.ADD() != null)
        {
            return left + right;
        }
        else if (context.SUB() != null)
        {
            return left - right;
        }
        
        throw new InvalidOperationException(ErrorMessages.InvalidOperation("soma/subtração"));
    }

    public override double VisitNumero([NotNull] GramaticaParser.NumeroContext context)
    {
        var numberText = context.NUMBER().GetText();
        
        if (double.TryParse(numberText, out double result))
        {
            return result;
        }
        
        throw new FormatException(ErrorMessages.InvalidNumber(numberText));
    }

    public override double VisitParenteses([NotNull] GramaticaParser.ParentesesContext context)
    {
        return Visit(context.expr());
    }
}
