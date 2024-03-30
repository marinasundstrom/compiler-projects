using System;

namespace Raven.CodeAnalysis.Syntax
{
    public struct SyntaxToken : ISyntaxNode
    {
        private InternalSyntax.SyntaxToken syntaxToken;

        public SyntaxToken(InternalSyntax.SyntaxToken syntaxToken, SyntaxNode parentNode)
        {
            this.syntaxToken = syntaxToken;
            ParentNode = parentNode;
        }

        public SyntaxToken(InternalSyntax.SyntaxToken syntaxToken)
            : this(syntaxToken, null!)
        {
            
        }

        public SyntaxKind Kind => syntaxToken.Kind;

        public TextSpan Span => new TextSpan(0, syntaxToken.Width);

        public TextSpan FullSpan => new TextSpan(0, syntaxToken.Width);

        public SyntaxNode? ParentNode { get; internal set; }

        public SyntaxTriviaList LeadingTrivia => syntaxToken.LeadingTrivia;

        public SyntaxTriviaList TrailingTrivia => syntaxToken.TrailingTrivia;

        public string ToFullString()
        {
            return this.syntaxToken.ToFullString();
        }

        public override string ToString()
        {
            return SyntaxFacts.GetSyntaxTokenText(Kind);
        }
    }
}
