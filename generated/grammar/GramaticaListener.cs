//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.9.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from grammar/Gramatica.g4 by ANTLR 4.9.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="GramaticaParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.9.2")]
[System.CLSCompliant(false)]
public interface IGramaticaListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="GramaticaParser.start"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStart([NotNull] GramaticaParser.StartContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GramaticaParser.start"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStart([NotNull] GramaticaParser.StartContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Atribuicao</c>
	/// labeled alternative in <see cref="GramaticaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAtribuicao([NotNull] GramaticaParser.AtribuicaoContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Atribuicao</c>
	/// labeled alternative in <see cref="GramaticaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAtribuicao([NotNull] GramaticaParser.AtribuicaoContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Expressao</c>
	/// labeled alternative in <see cref="GramaticaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpressao([NotNull] GramaticaParser.ExpressaoContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Expressao</c>
	/// labeled alternative in <see cref="GramaticaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpressao([NotNull] GramaticaParser.ExpressaoContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Numero</c>
	/// labeled alternative in <see cref="GramaticaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumero([NotNull] GramaticaParser.NumeroContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Numero</c>
	/// labeled alternative in <see cref="GramaticaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumero([NotNull] GramaticaParser.NumeroContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Variavel</c>
	/// labeled alternative in <see cref="GramaticaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVariavel([NotNull] GramaticaParser.VariavelContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Variavel</c>
	/// labeled alternative in <see cref="GramaticaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVariavel([NotNull] GramaticaParser.VariavelContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Parenteses</c>
	/// labeled alternative in <see cref="GramaticaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParenteses([NotNull] GramaticaParser.ParentesesContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Parenteses</c>
	/// labeled alternative in <see cref="GramaticaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParenteses([NotNull] GramaticaParser.ParentesesContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Multiplicacao_Divisao</c>
	/// labeled alternative in <see cref="GramaticaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMultiplicacao_Divisao([NotNull] GramaticaParser.Multiplicacao_DivisaoContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Multiplicacao_Divisao</c>
	/// labeled alternative in <see cref="GramaticaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMultiplicacao_Divisao([NotNull] GramaticaParser.Multiplicacao_DivisaoContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Soma_Subtracao</c>
	/// labeled alternative in <see cref="GramaticaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSoma_Subtracao([NotNull] GramaticaParser.Soma_SubtracaoContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Soma_Subtracao</c>
	/// labeled alternative in <see cref="GramaticaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSoma_Subtracao([NotNull] GramaticaParser.Soma_SubtracaoContext context);
}
