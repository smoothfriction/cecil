//
// Gendarme.Rules.Naming.DoNotPrefixValuesWithEnumNameRule
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

namespace Gendarme.Rules.Naming {

	public class DoNotPrefixValuesWithEnumNameRule : ITypeRule {

		public MessageCollection CheckType (TypeDefinition type, Runner runner)
		{
			if (!type.IsEnum)
				return runner.RuleSuccess;

			MessageCollection results = null;

			foreach (FieldDefinition field in type.Fields) {
				// this excludes special "value__"
				if (!field.IsStatic)
					continue;

				if (field.Name.StartsWith (type.Name, StringComparison.OrdinalIgnoreCase)) {
					if (results == null)
						results = new MessageCollection ();
					Location loc = new Location (field);
					Message msg = new Message (string.Format ("Enum values should not be prefixed with the enum's name.", type.FullName), loc, MessageType.Warning);
					results.Add (msg);
				}
			}

			return results;
		}
	}
}