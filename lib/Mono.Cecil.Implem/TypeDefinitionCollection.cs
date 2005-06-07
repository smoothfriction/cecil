/*
 * Copyright (c) 2004, 2005 DotNetGuru and the individuals listed
 * on the ChangeLog entries.
 *
 * Authors :
 *   Jb Evain   (jbevain@gmail.com)
 *
 * This is a free software distributed under a MIT/X11 license
 * See LICENSE.MIT file for more details
 *
 * Generated by /CodeGen/cecil-gen.rb do not edit
 * Sun Jun 05 22:09:40 Paris, Madrid 2005
 *
 *****************************************************************************/

namespace Mono.Cecil.Implem {

    using System;
    using System.Collections;

    using Mono.Cecil;
    using Mono.Cecil.Cil;

    internal class TypeDefinitionCollection : ITypeDefinitionCollection, ILazyLoadableCollection {

        private IDictionary m_items;
        private ModuleDefinition m_container;
        private ReflectionController m_controller;

        private bool m_loaded;

        public ITypeDefinition this [string name] {
            get {
                Load ();
                return m_items [name] as ITypeDefinition;
            }
            set { m_items [name] = value; }
        }

        public IModuleDefinition Container {
            get { return m_container; }
        }

        public int Count {
            get {
                Load ();
                return m_items.Count;
            }
        }

        public bool IsSynchronized {
            get { return false; }
        }

        public object SyncRoot {
            get { return this; }
        }

        public bool Loaded {
            get { return m_loaded; }
            set { m_loaded = value; }
        }
        
        public TypeDefinitionCollection (ModuleDefinition container)
        {
            m_container = container;
            m_items = new Hashtable ();
        }        

        public TypeDefinitionCollection (ModuleDefinition container, ReflectionController controller) : this (container)
        {
            m_controller = controller;
        }

        public void Clear ()
        {
            m_items.Clear ();
        }

        public bool Contains (ITypeDefinition value)
        {
            return m_items.Contains (value);
        }

        public void Remove (ITypeDefinition value)
        {
            m_items.Remove (value);
        }

        public void CopyTo (Array ary, int index)
        {
            Load ();
            m_items.Values.CopyTo (ary, index);
        }

        public IEnumerator GetEnumerator ()
        {
            Load ();
            return m_items.Values.GetEnumerator ();
        }
        
        public void Load ()
        {
            if (m_controller != null && !m_loaded) {
                m_controller.Reader.Visit (this);
                m_loaded = true;
            }
        }

        public void Accept (IReflectionVisitor visitor)
        {
            visitor.Visit (this);
            ITypeDefinition [] items = new ITypeDefinition [m_items.Count];
            m_items.Values.CopyTo (items, 0);
            for (int i = 0; i < items.Length; i++)
                items [i].Accept (visitor);
        }
    }
}
