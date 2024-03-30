namespace Raven.CodeAnalysis.Syntax.InternalSyntax
{
    public abstract class SyntaxNode
    {
        public virtual SyntaxKind Kind { get; }

        public virtual int Width { get; }

        public virtual int FullWidth { get; }

        public bool ValidateNode()
        {
            return false;
        }

        public abstract string ToFullString();
    }
}
