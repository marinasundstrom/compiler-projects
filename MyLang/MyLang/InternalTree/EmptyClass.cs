namespace MyLang.InternalTree;

public abstract class InternalSyntax
{
}

public abstract class InternalSyntaxNode : InternalSyntax
{
    public long Width { get; set; }
}

public abstract class InternalSyntaxToken : InternalSyntax
{

}

public abstract class InternalExpressionSyntax : InternalSyntaxNode
{

}

public sealed class InternalIdentifierExpressionSyntax : InternalExpressionSyntax
{
    public InternalIdentifierExpressionSyntax(InternalSyntaxToken token)
    {
        Token = token;
    }

    public InternalSyntaxToken Token { get; }
}

public sealed class InternalNumberExpressionSyntax : InternalExpressionSyntax
{
    public InternalNumberExpressionSyntax(InternalSyntaxToken token)
    {
        Token = token;
    }

    public InternalSyntaxToken Token { get; }
}

public sealed class InternalBinaryOperationExpressions : InternalExpressionSyntax
{
    public InternalBinaryOperationExpressions(
        InternalExpressionSyntax leftHandSide, InternalSyntaxToken operatorToken, InternalExpressionSyntax eightHandSide)
    {
        LeftHandSide = leftHandSide;
        OperatorToken = operatorToken;
        EightHandSide = eightHandSide;
    }

    public InternalExpressionSyntax LeftHandSide { get; }

    public InternalSyntaxToken OperatorToken { get; }

    public InternalExpressionSyntax EightHandSide { get; }
}

public sealed class InternalLetDeclarationExpressionSyntax : InternalExpressionSyntax
{
    public InternalLetDeclarationExpressionSyntax(
        InternalSyntaxToken letKeyword, InternalExpressionSyntax targetExpression, InternalSyntaxToken assignmentToken, InternalExpressionSyntax assignmentExpression)
    {
        LetKeyword = letKeyword;
        TargetExpression = targetExpression;
        AssignmentToken = assignmentToken;
        AssignmentExpression = assignmentExpression;

    }

    public InternalSyntaxToken LetKeyword { get; }

    public InternalExpressionSyntax TargetExpression { get; }

    public InternalSyntaxToken AssignmentToken { get; }

    public InternalExpressionSyntax AssignmentExpression { get; }
}

public sealed class InternalAssignmentExpressionSyntax : InternalExpressionSyntax
{
    public InternalAssignmentExpressionSyntax(
        InternalExpressionSyntax targetExpression, InternalSyntaxToken assignmentToken, InternalExpressionSyntax assignmentExpression)
    {
        TargetExpression = targetExpression;
        AssignmentToken = assignmentToken;
        AssignmentExpression = assignmentExpression;
    }

    public InternalExpressionSyntax TargetExpression { get; }

    public InternalSyntaxToken AssignmentToken { get; }

    public InternalExpressionSyntax AssignmentExpression { get; }
}

public sealed class InternalParenthesisExpressionSyntax : InternalExpressionSyntax
{
    public InternalParenthesisExpressionSyntax(
        InternalSyntaxToken openingParenToken, InternalExpressionSyntax expression, InternalSyntaxToken closingParenToken)
    {
        OpeningParenToken = openingParenToken;
        Expression = expression;
        ClosingParenToken = closingParenToken;
    }

    public InternalSyntaxToken OpeningParenToken { get; }

    public InternalExpressionSyntax Expression { get; }

    public InternalSyntaxToken ClosingParenToken { get; }
}

public sealed class InternalIfElseExpressionSyntax : InternalExpressionSyntax
{
    public InternalIfElseExpressionSyntax(
        InternalSyntaxToken ifKeyword, InternalExpressionSyntax conditionalExpression, InternalSyntaxToken
        thenKeyword, InternalExpressionSyntax thenExpression, InternalSyntaxToken? elseKeyword = null, InternalExpressionSyntax? elseExpression = null)
    {
        IfKeyword = ifKeyword;
        ConditionalExpression = conditionalExpression;
        ThenKeyword = thenKeyword;
        ThenExpression = thenExpression;
        ElseKeyword = elseKeyword;
        ElseExpression = elseExpression;
    }

    public InternalSyntaxToken IfKeyword { get; }

    public InternalExpressionSyntax ConditionalExpression { get; }

    public InternalSyntaxToken ThenKeyword { get; }

    public InternalExpressionSyntax ThenExpression { get; }

    public InternalSyntaxToken? ElseKeyword { get; }

    public InternalExpressionSyntax? ElseExpression { get; }
}

public abstract class InternalBooleanExpressionSyntax : InternalExpressionSyntax
{

}

public sealed class InternalBooleanTrueExpressionSyntax : InternalBooleanExpressionSyntax
{
    public InternalBooleanTrueExpressionSyntax(InternalSyntaxToken trueKeyword)
    {
        TrueKeyword = trueKeyword;
    }

    public InternalSyntaxToken TrueKeyword { get; }
}

public sealed class InternalBooleanFalseExpressionSyntax : InternalBooleanExpressionSyntax
{
    public InternalBooleanFalseExpressionSyntax(InternalSyntaxToken falseKeyword)
    {
        FalseKeyword = falseKeyword;
    }

    public InternalSyntaxToken FalseKeyword { get; }
}