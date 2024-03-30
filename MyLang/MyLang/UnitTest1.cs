using System.Reflection;
using System.Text.Json;
using MyLang;
using MyLang.InternalTree;

namespace MyLang;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // let x = if y > 1337 then true

        var expr1 = SyntaxFactory.Let(
            SyntaxFactory.Identifier("x"),
            SyntaxFactory.IfElseExpression(
                    SyntaxFactory.BinaryOperation(
                         SyntaxFactory.Identifier("y"),
                         SyntaxFactory.GreaterThanToken,
                         SyntaxFactory.Number(1337)
                    ),
                    SyntaxFactory.True()
                ));

        var foo = expr1.Location;

        var str2 = JsonSerializer.Serialize(expr1, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        Console.WriteLine(str2);


        // x = if (y > 1337) then true else false

        var expr = SyntaxFactory.Assignment(
                SyntaxFactory.Identifier("x"),
                SyntaxFactory.IfElseExpression(
                    SyntaxFactory.Parenthesis(
                        SyntaxFactory.BinaryOperation(
                             SyntaxFactory.Identifier("y"),
                             SyntaxFactory.GreaterThanToken,
                             SyntaxFactory.Number(1337)
                        )
                    ),
                    SyntaxFactory.True(),
                    SyntaxFactory.False()
                )
            );

        var str = JsonSerializer.Serialize(expr, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        Console.WriteLine(str);
    }
}

public interface ISyntax
{
    SourceSpan Location { get; }

    SyntaxNode Parent { get; }
}

internal interface IHasParent
{
    SyntaxNode Parent { get; }

    void SetParent(SyntaxNode parent);
}

public record struct SyntaxToken(string Value) : ISyntax, IHasParent
{
    public SourceSpan Location { get; }

    public SyntaxNode Parent { get; private set; }

    public int Width => Value.Length;

    void IHasParent.SetParent(SyntaxNode parent) => Parent = parent;

    internal InternalSyntaxToken InternalSyntax { get; set; }
}

public record struct SourceLocation(long Line, long Column);

public record struct SourceSpan(SourceLocation Start, SourceLocation End);

public abstract class SyntaxNode : ISyntax, IHasParent
{
    List<ISyntax> _children = new List<ISyntax>();

    public SourceSpan Location { get; private set; }

    public SyntaxNode Parent { get; private set; } = null!;

    IReadOnlyList<SyntaxNode> ChildNodes => _children.OfType<SyntaxNode>().ToList();

    protected void AttachChild(ISyntax childSyntax)
    {
        ((IHasParent)childSyntax).SetParent(this);
        _children.Add(childSyntax);
    }

    void IHasParent.SetParent(SyntaxNode parent) => Parent = parent;

    public TSyntaxNode GetOrCreateNode<TSyntaxNode>(InternalTree.InternalSyntax internalSyntax, ref TSyntaxNode node)
        where TSyntaxNode : ISyntax
    {
        if(node is null)
        {
            node = (TSyntaxNode)Activator.CreateInstance(typeof(TSyntaxNode), new object [] {  internalSyntax})!;

            AttachChild(node);
        }

        return node!;
    }
}

public abstract class ExpressionSyntax : SyntaxNode
{
    public InternalExpressionSyntax InternalSyntax { get; private set; } = default;
}

public sealed class IdentifierExpressionSyntax : ExpressionSyntax
{
    public IdentifierExpressionSyntax(SyntaxToken token)
    {
        Token = token;

        AttachChild(token);
    }

    public SyntaxToken Token { get; }

    internal IdentifierExpressionSyntax(InternalIdentifierExpressionSyntax internalSyntax)
    {
        InternalSyntax = internalSyntax;
    }

    new internal InternalIdentifierExpressionSyntax InternalSyntax { get; set; }
}

public sealed class NumberExpressionSyntax : ExpressionSyntax
{
    public NumberExpressionSyntax(SyntaxToken token)
    {
        Token = token;

        AttachChild(token);
    }

    public SyntaxToken Token { get; }

    internal NumberExpressionSyntax(InternalNumberExpressionSyntax internalSyntax)
    {
        InternalSyntax = internalSyntax;
    }

    internal InternalNumberExpressionSyntax InternalSyntax { get; set; }
}

public sealed class BinaryOperationExpressions : ExpressionSyntax
{
    public BinaryOperationExpressions(
        ExpressionSyntax leftHandSide, SyntaxToken operatorToken, ExpressionSyntax eightHandSide)
    {
        LeftHandSide = leftHandSide;
        OperatorToken = operatorToken;
        EightHandSide = eightHandSide;

        AttachChild(leftHandSide);
        AttachChild(operatorToken);
        AttachChild(eightHandSide);
    }

    public ExpressionSyntax LeftHandSide { get; }

    public SyntaxToken OperatorToken { get; }

    public ExpressionSyntax EightHandSide { get; }

    internal BinaryOperationExpressions(InternalBinaryOperationExpressions internalSyntax)
    {
        InternalSyntax = internalSyntax;
    }

    internal InternalBinaryOperationExpressions InternalSyntax { get; set; }
}

public sealed class LetDeclarationExpressionSyntax : ExpressionSyntax
{
    private SyntaxToken _letKeyword;
    private ExpressionSyntax _targetExpression;
    private SyntaxToken _assignmentToken;
    private ExpressionSyntax _assignmentExpression;

    public LetDeclarationExpressionSyntax(
        SyntaxToken letKeyword, ExpressionSyntax targetExpression, SyntaxToken assignmentToken, ExpressionSyntax assignmentExpression)
    {
        InternalSyntax = new InternalLetDeclarationExpressionSyntax(
            _letKeyword.InternalSyntax, _targetExpression.InternalSyntax, _assignmentToken.InternalSyntax, _assignmentExpression.InternalSyntax);

        _letKeyword = letKeyword;
        _targetExpression = targetExpression;
        _assignmentToken = assignmentToken;
        _assignmentExpression = assignmentExpression;

        AttachChild(letKeyword);
        AttachChild(targetExpression);
        AttachChild(assignmentToken);
        AttachChild(assignmentExpression);
    }

    public SyntaxToken LetKeyword => GetOrCreateNode(InternalSyntax.LetKeyword, ref _letKeyword);

    public ExpressionSyntax TargetExpression => GetOrCreateNode(InternalSyntax.TargetExpression, ref _targetExpression);

    public SyntaxToken AssignmentToken => GetOrCreateNode(InternalSyntax.AssignmentToken, ref _assignmentToken);

    public ExpressionSyntax AssignmentExpression => GetOrCreateNode(InternalSyntax.AssignmentExpression, ref _assignmentExpression);

    internal LetDeclarationExpressionSyntax(InternalLetDeclarationExpressionSyntax internalSyntax)
    {
        InternalSyntax = internalSyntax;
    }

    internal InternalLetDeclarationExpressionSyntax InternalSyntax { get; set; }
}

public sealed class AssignmentExpressionSyntax : ExpressionSyntax
{
    public AssignmentExpressionSyntax(
        ExpressionSyntax targetExpression, SyntaxToken assignmentToken, ExpressionSyntax assignmentExpression)
    {
        TargetExpression = targetExpression;
        AssignmentToken = assignmentToken;
        AssignmentExpression = assignmentExpression;

        AttachChild(targetExpression);
        AttachChild(assignmentToken);
        AttachChild(assignmentExpression);
    }

    public ExpressionSyntax TargetExpression { get; }
    public SyntaxToken AssignmentToken { get; }
    public ExpressionSyntax AssignmentExpression { get; }

    internal AssignmentExpressionSyntax(InternalAssignmentExpressionSyntax internalSyntax)
    {
        InternalSyntax = internalSyntax;
    }

    internal InternalAssignmentExpressionSyntax InternalSyntax { get; set; }
}

public sealed class ParenthesisExpressionSyntax : ExpressionSyntax
{
    public ParenthesisExpressionSyntax(
        SyntaxToken openingParenToken, ExpressionSyntax expression, SyntaxToken closingParenToken)
    {
        OpeningParenToken = openingParenToken;
        Expression = expression;
        ClosingParenToken = closingParenToken;

        AttachChild(openingParenToken);
        AttachChild(expression);
        AttachChild(closingParenToken);
    }

    public SyntaxToken OpeningParenToken { get; }

    public ExpressionSyntax Expression { get; }

    public SyntaxToken ClosingParenToken { get; }

    internal ParenthesisExpressionSyntax(InternalParenthesisExpressionSyntax internalSyntax)
    {
        InternalSyntax = internalSyntax;
    }

    internal InternalParenthesisExpressionSyntax InternalSyntax { get; set; }
}

public sealed class IfElseExpressionSyntax : ExpressionSyntax
{
    public IfElseExpressionSyntax(
        SyntaxToken ifKeyword, ExpressionSyntax conditionalExpression, SyntaxToken thenKeyword,
        ExpressionSyntax thenExpression, SyntaxToken? elseKeyword = null, ExpressionSyntax? elseExpression = null)
    {
        IfKeyword = ifKeyword;
        ConditionalExpression = conditionalExpression;
        ThenKeyword = thenKeyword;
        ThenExpression = thenExpression;
        ElseKeyword = elseKeyword;
        ElseExpression = elseExpression;

        AttachChild(ifKeyword);
        AttachChild(conditionalExpression);
        AttachChild(thenKeyword);

        if (elseKeyword is not null)
        {
            AttachChild(elseKeyword);
        }

        if (elseExpression is not null)
        {
            AttachChild(elseExpression);
        }
    }

    public SyntaxToken IfKeyword { get; }

    public ExpressionSyntax ConditionalExpression { get; }

    public SyntaxToken ThenKeyword { get; }

    public ExpressionSyntax ThenExpression { get; }

    public SyntaxToken? ElseKeyword { get; }

    public ExpressionSyntax? ElseExpression { get; }

    internal IfElseExpressionSyntax(InternalIfElseExpressionSyntax internalSyntax)
    {
        InternalSyntax = internalSyntax;
    }

    internal InternalIfElseExpressionSyntax InternalSyntax { get; set; }
}

public abstract class BooleanExpressionSyntax : ExpressionSyntax
{

}

public sealed class BooleanTrueExpressionSyntax : BooleanExpressionSyntax
{
    public BooleanTrueExpressionSyntax(SyntaxToken trueKeyword)
    {
        TrueKeyword = trueKeyword;

        AttachChild(trueKeyword);
    }

    public SyntaxToken TrueKeyword { get; }

    internal BooleanTrueExpressionSyntax(InternalBooleanTrueExpressionSyntax internalSyntax)
    {
        InternalSyntax = internalSyntax;
    }

    internal InternalBooleanTrueExpressionSyntax InternalSyntax { get; set; }
}

public sealed class BooleanFalseExpressionSyntax : BooleanExpressionSyntax
{
    public BooleanFalseExpressionSyntax(SyntaxToken falseKeyword)
    {
        FalseKeyword = falseKeyword;

        AttachChild(falseKeyword);
    }

    public SyntaxToken FalseKeyword { get; }

    internal BooleanFalseExpressionSyntax(InternalBooleanFalseExpressionSyntax internalSyntax)
    {
        InternalSyntax = internalSyntax;
    }

    internal InternalBooleanFalseExpressionSyntax InternalSyntax { get; set; }
}

public static class SyntaxFactory
{
    public static IdentifierExpressionSyntax Identifier(SyntaxToken token) => new IdentifierExpressionSyntax(token);

    public static IdentifierExpressionSyntax Identifier(string identifier) => Identifier(new SyntaxToken(identifier));

    public static NumberExpressionSyntax Number(SyntaxToken token) => new NumberExpressionSyntax(token);

    public static NumberExpressionSyntax Number(int value) => new NumberExpressionSyntax(new SyntaxToken(value.ToString()));

    public static BinaryOperationExpressions BinaryOperation(ExpressionSyntax leftHandSide, SyntaxToken operatorToken, ExpressionSyntax rightHandSide)
        => new BinaryOperationExpressions(leftHandSide, operatorToken, rightHandSide);

    public static LetDeclarationExpressionSyntax Let(ExpressionSyntax targetExpression, ExpressionSyntax assignmentExpression)
        => new LetDeclarationExpressionSyntax(SyntaxFactory.LetKeyword, targetExpression, SyntaxFactory.AssignmentToken, assignmentExpression);

    public static AssignmentExpressionSyntax Assignment(ExpressionSyntax targetExpression, ExpressionSyntax assignmentExpression)
        => new AssignmentExpressionSyntax(targetExpression, SyntaxFactory.AssignmentToken, assignmentExpression);

    public static ParenthesisExpressionSyntax Parenthesis(ExpressionSyntax expression)
        => new ParenthesisExpressionSyntax(SyntaxFactory.OpeningParenToken, expression, SyntaxFactory.ClosingParenToken);

    public static IfElseExpressionSyntax IfElseExpression(ExpressionSyntax conditionalExpression, ExpressionSyntax thenExpression, ExpressionSyntax? elseExpression = null)
        => new IfElseExpressionSyntax(SyntaxFactory.IfKeyword, conditionalExpression, SyntaxFactory.ThenKeyword, thenExpression, SyntaxFactory.ElseKeyword, elseExpression);

    public static BooleanTrueExpressionSyntax True()
        => new BooleanTrueExpressionSyntax(SyntaxFactory.TrueKeyword);

    public static BooleanFalseExpressionSyntax False()
        => new BooleanFalseExpressionSyntax(SyntaxFactory.FalseKeyword);

    public static readonly SyntaxToken AssignmentToken = new SyntaxToken("=");

    public static readonly SyntaxToken IfKeyword = new SyntaxToken("if");

    public static readonly SyntaxToken LetKeyword = new SyntaxToken("let");

    public static readonly SyntaxToken ElseKeyword = new SyntaxToken("else");

    public static readonly SyntaxToken TrueKeyword = new SyntaxToken("true");

    public static readonly SyntaxToken FalseKeyword = new SyntaxToken("false");

    public static readonly SyntaxToken ThenKeyword = new SyntaxToken("then");

    public static readonly SyntaxToken GreaterThanToken = new SyntaxToken(">");

    public static readonly SyntaxToken OpeningParenToken = new SyntaxToken("(");

    public static readonly SyntaxToken ClosingParenToken = new SyntaxToken(")");
}