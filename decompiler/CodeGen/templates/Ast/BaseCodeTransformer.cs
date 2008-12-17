#region license
//
//	(C) 2005 - 2007 db4objects Inc. http://www.db4o.com
//	(C) 2007 - 2008 Novell, Inc. http://www.novell.com
//	(C) 2007 - 2008 Jb Evain http://evain.net
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
#endregion

// Warning: generated do not edit

using System;
using System.Collections;
using System.Collections.Generic;

namespace Cecil.Decompiler.Ast {

	public class BaseCodeTransformer : ICodeTransformer {

		public virtual ICodeNode Visit (ICodeNode node)
		{
			if (node == null)
				return null;

			switch (node.CodeNodeType) {
<%
	for node in model.GetVisitableNodes():
%>			case CodeNodeType.${node.Name}:
				return Visit${node.Name} ((${node.Name}) node);
<%
	end
%>			default:
				throw new ArgumentException ();
			}
		}

		public virtual IList<TElement> Visit<TElement> (IList<TElement> list)
			where TElement : ICodeNode
		{
			for (int i = 0; i < list.Count; i++) {
				list [i] = (TElement) Visit (list [i]);
			}
			return list;
		}
<%
	for node in model.GetVisitableNodes():
%>
		public virtual ICodeNode Visit${node.Name} (${node.Name} node)
		{
<%
		for field in model.GetVisitableFields(node):
%>			node.${field.Name} = (${field.Type.ToString ()}) Visit (node.${field.Name});
<%
		end
%>			return node;
		}
<%
	end
%>	}
}