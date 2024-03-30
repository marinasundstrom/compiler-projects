using Raven.CodeAnalysis.Parser.Internal;

namespace Raven.CodeAnalysis.Syntax.InternalSyntax
{
    public class BlockSyntax : StatementSyntax
    {
        public BlockSyntax(SyntaxToken openBraceToken, SyntaxToken closeBraceToken)
        {
            OpenBraceToken = openBraceToken;
            CloseBraceToken = closeBraceToken;
        }

        public override SyntaxKind Kind => SyntaxKind.BlockStatementSyntax;

        public override int Width => OpenBraceToken.Width + OpenBraceToken.TrailingTrivia.Width + CloseBraceToken.Width + CloseBraceToken.LeadingTrivia.Width;

        public override int FullWidth => OpenBraceToken.FullWidth + CloseBraceToken.FullWidth;

        public SyntaxToken OpenBraceToken { get; }

        public SyntaxToken CloseBraceToken { get; }

        public override string ToFullString()
        {
            return OpenBraceToken.ToFullString() + CloseBraceToken.ToFullString();
        }
    }
}
