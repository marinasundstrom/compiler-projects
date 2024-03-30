using System;
using Raven.CodeAnalysis.Parser.Internal;

namespace Raven.CodeAnalysis.Syntax.InternalSyntax
{
    public class IfStatementSyntax : StatementSyntax
    {
        public IfStatementSyntax(SyntaxToken? ifToken, SyntaxToken? openParenToken, ExpressionSyntax? condition, SyntaxToken? closeParenToken, StatementSyntax? statement, ElseClauseSyntax? @else)
        {
            this.IfToken = ifToken;
            OpenParenToken = openParenToken;
            Condition = condition;
            CloseParenToken = closeParenToken;
            Statement = statement;
            Else = @else;
        }

        public override SyntaxKind Kind => SyntaxKind.IfStatementSyntax;

        public override int Width => IfToken.Width + OpenParenToken.TrailingTrivia.Width + CloseParenToken.Width + CloseParenToken.LeadingTrivia.Width;

        public override int FullWidth => OpenParenToken.FullWidth + CloseParenToken.FullWidth;

        public SyntaxToken? IfToken { get; }

        public SyntaxToken? OpenParenToken { get; }

        public ExpressionSyntax? Condition { get; }

        public SyntaxToken? CloseParenToken { get; }

        public StatementSyntax? Statement { get; }

        public ElseClauseSyntax? Else { get; }

        public override string ToFullString()
        {
            return IfToken.ToFullString() + OpenParenToken.ToFullString() + Condition?.ToFullString() + CloseParenToken.ToFullString() + Statement.ToFullString();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public static partial class SyntaxFactory
    {
        public static IfStatementSyntax IfStatementSyntax(ExpressionSyntax condition, StatementSyntax statement, ElseClauseSyntax elseClause)
        {
            return new IfStatementSyntax(SyntaxFactory.IfToken, SyntaxFactory.OpenParenToken, condition, SyntaxFactory.CloseParenToken, statement, elseClause);
        }
    }
}
