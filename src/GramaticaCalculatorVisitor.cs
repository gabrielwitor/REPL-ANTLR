using Antlr4.Runtime.Misc;

public class GramaticaCalculatorVisitor : GramaticaBaseVisitor<double>
{
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
                throw new DivideByZeroException("Divisão por zero não é permitida!");
            }
            return left / right;
        }
        
        return 0; // Fallback (não deveria acontecer)
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
        
        return 0; // Fallback (não deveria acontecer)
    }

    public override double VisitNumero([NotNull] GramaticaParser.NumeroContext context)
    {
        return double.Parse(context.NUMBER().GetText());
    }

    public override double VisitParenteses([NotNull] GramaticaParser.ParentesesContext context)
    {
        return Visit(context.expr());
    }
}
