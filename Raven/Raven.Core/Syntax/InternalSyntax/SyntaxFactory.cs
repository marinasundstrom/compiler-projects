using System;
namespace Raven.CodeAnalysis.Syntax.InternalSyntax
{
    public static partial class SyntaxFactory
    {
        //public static SyntaxTrivia ElasticToken { get; } = new SyntaxTrivia((SyntaxKind.ElasticToken);

        public static SyntaxToken ElasticToken { get; } = new SyntaxToken(SyntaxKind.ElasticToken);
        public static SyntaxToken IfToken { get; } = new SyntaxToken(SyntaxKind.IfToken);
        public static SyntaxToken OpenParenToken { get; } = new SyntaxToken(SyntaxKind.OpenParenToken);
        public static SyntaxToken CloseParenToken { get; } = new SyntaxToken(SyntaxKind.CloseParenToken);

        public static SyntaxToken Identifier(SyntaxTriviaList leadingTrivia, SyntaxKind contextualKind, string text, SyntaxTriviaList trailingTrivia)
        {
            return new SyntaxToken(leadingTrivia, contextualKind, text, trailingTrivia);
        }
    }
}
