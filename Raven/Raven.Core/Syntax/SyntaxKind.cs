namespace Raven.CodeAnalysis.Syntax
{
    public enum SyntaxKind
    {
        EndOfFileTrivia,

        WhitespaceTrivia,
        NewlineTrivia,

        IdentifierToken,
        NumberToken,
 
        OpenParenToken,
        CloseParenToken,
        OpenBraceToken,
        CloseBraceToken,
        OpenSquareToken,
        CloseSquareToken,
        OpenAngleToken,
        CloseAngleToken,

        AssignToken,
        EqualsToken,
        NotEqualsToken,
        GreaterEqualsToken,
        LessEqualsToken,

        PlusToken,
        DashToken,
        SlashToken,
        BackslashToken,
        StarToken,
        PercentToken,
        ColonToken,
        SemicolonToken,
        DoublequoteToken,
        SinglequoteToken,
        BackquoteToken,

        VarToken,
        LetToken,
        IfToken,
        ElseToken,

        IfStatementSyntax,
        ElseClauseSyntax,
        BlockStatementSyntax,
        DotToken,
        InvalidSyntax,
        TabTrivia,
        ElasticToken
    }
}
