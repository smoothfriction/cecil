/*
 * Copyright (c) 2004 DotNetGuru and the individuals listed
 * on the ChangeLog entries.
 *
 * Authors :
 *   Jb Evain   (jb.evain@dotnetguru.org)
 *
 * This is a free software distributed under a MIT/X11 license
 * See LICENSE.MIT file for more details
 *
 * Generated by /CodeGen/cecil-gen.rb do not edit
 * Sat Feb 12 06:05:00 Paris, Madrid 2005
 *
 *****************************************************************************/

namespace Mono.Cecil.Implem {

    using System;
    using System.Collections;
    using System.Collections.Specialized;

    using Mono.Cecil;
    using Mono.Cecil.Cil;

    internal class TypeReferenceCollection : ITypeReferenceCollection {

        private IDictionary m_items;
        private IModuleDefinition m_container;

        public ITypeReference this [string name] {
            get { return m_items [name] as ITypeReference; }
            set { m_items [name] = value; }
        }

        public IModuleDefinition Container {
            get { return m_container; }
        }

        public int Count {
            get { return m_items.Count; }
        }

        public bool IsSynchronized {
            get { return false; }
        }

        public object SyncRoot {
            get { return this; }
        }

        public TypeReferenceCollection (IModuleDefinition container)
        {
            m_container = container;
            m_items = new ListDictionary ();
        }

        public void Clear ()
        {
            m_items.Clear ();
        }

        public bool Contains (ITypeReference value)
        {
            return m_items.Contains (value);
        }

        public void Remove (ITypeReference value)
        {
            m_items.Remove (value);
        }

        public void CopyTo (Array ary, int index)
        {
            m_items.Values.CopyTo (ary, index);
        }

        public IEnumerator GetEnumerator ()
        {
            return m_items.Values.GetEnumerator ();
        }

        public void Accept (IReflectionVisitor visitor)
        {
            visitor.Visit (this);
            ITypeReference [] items = new ITypeReference [m_items.Count];
            m_items.Values.CopyTo (items, 0);
            for (int i = 0; i < items.Length; i++)
                items [i].Accept (visitor);
        }
    }
}
