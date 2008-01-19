//
// Gendarme.Rules.Design.OverrideEqualsMethodRule
//
// Authors:
//	Andreas Noever <andreas.noever@gmail.com>
//
//  (C) 2008 Andreas Noever
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

using System;
using Mono.Cecil;
using Gendarme.Framework;
using Gendarme.Framework.Rocks;

namespace Gendarme.Rules.Design {

	public class OverrideEqualsMethodRule : ITypeRule {

		public MessageCollection CheckType (TypeDefinition type, Runner runner)
		{
			if (type.IsEnum || type.IsInterface)
				return runner.RuleSuccess;

			if (type.HasMethod (MethodSignatures.op_Equality)) {
				if (!type.HasMethod (MethodSignatures.Equals)) {
					Location loc = new Location (type);
					Message msg = new Message ("This type implements the equality (==) operator. It should also override the Object.Equals method.", loc, MessageType.Warning);
					return new MessageCollection (msg);
				}
			}
			return runner.RuleSuccess;
		}
	}
}