namespace Raven.CodeAnalysis.Syntax
{
    public interface ISyntaxNode
    {
        SyntaxKind Kind { get; }

        TextSpan Span { get; }

        TextSpan FullSpan { get; }

        SyntaxNode? ParentNode { get; }
    }

    public abstract class SyntaxNode : ISyntaxNode
    {
        public virtual SyntaxKind Kind { get; }

        public virtual TextSpan Span { get; } = null!;

        public virtual TextSpan FullSpan { get; } = null!;

        public virtual SyntaxNode? ParentNode { get; } = null!;
    }
}
