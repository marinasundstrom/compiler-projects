using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Raven.CodeAnalysis.Syntax.InternalSyntax
{
    public class SyntaxTriviaList : List<SyntaxTrivia>
    {
        public int Width => this.Sum(x => x.Width);

        internal string ToFullString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var item in this)
            {
                sb.Append(item.Text);
            }
            return sb.ToString();
        }
    }
}
