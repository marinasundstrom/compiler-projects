using Raven.CodeAnalysis.Parser.Internal;

namespace Raven.CodeAnalysis.Syntax.InternalSyntax
{
    public class ElseClauseSyntax : SyntaxNode
    {
        public ElseClauseSyntax(SyntaxToken elseToken, StatementSyntax? parseStatement)
        {
            this.ElseToken = elseToken;
            this.ParseStatement = parseStatement;
        }

        public SyntaxToken ElseToken { get; }

        public StatementSyntax? ParseStatement { get; }

        public override string ToFullString()
        {
            throw new System.NotImplementedException();
        }
    }
}
