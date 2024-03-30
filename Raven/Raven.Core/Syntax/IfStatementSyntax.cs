using System;

namespace Raven.CodeAnalysis.Syntax
{
    public class IfStatementSyntax : StatementSyntax
    {
        private readonly InternalSyntax.IfStatementSyntax ifStatementSyntax;

        internal IfStatementSyntax(InternalSyntax.IfStatementSyntax ifStatementSyntax)
        {
            this.ifStatementSyntax = ifStatementSyntax;
        }
    }
}
