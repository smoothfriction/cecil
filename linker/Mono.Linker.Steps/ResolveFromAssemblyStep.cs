//
// ResolveFromAssemblyStep.cs
//
// Author:
//   Jb Evain (jbevain@gmail.com)
//
// (C) 2006 Jb Evain
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

using Mono.Cecil;

namespace Mono.Linker.Steps {

	public class ResolveFromAssemblyStep : ResolveStep {

		string _assembly;

		public ResolveFromAssemblyStep (string assembly)
		{
			_assembly = assembly;
		}

		public override void Process (LinkContext context)
		{
			AssemblyDefinition asm = context.Resolve (_assembly);
			Annotations.SetAction (asm, AssemblyAction.Copy);

			ProcessReferences (asm, context);
			foreach (TypeDefinition type in asm.MainModule.Types) {
				Annotations.Mark (type);

				foreach (MethodDefinition meth in type.Methods)
					MarkMethod (meth);
				foreach (MethodDefinition ctor in type.Constructors)
					MarkMethod (ctor);
			}
		}

		static void ProcessReferences (AssemblyDefinition assembly, LinkContext context)
		{
			foreach (AssemblyNameReference name in assembly.MainModule.AssemblyReferences)
				context.Resolve (name);
		}

		static void MarkMethod (MethodDefinition method)
		{
			Annotations.Mark (method);
			Annotations.SetAction (method, MethodAction.ForceParse);
		}
	}
}