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
 * <%=Time.now%>
 *
 *****************************************************************************/

namespace Mono.Cecil.Implem {

    using System;
    using System.Collections;
    using System.Collections.Specialized;

    using Mono.Cecil;
    using Mono.Cecil.Cil;

    internal class <%=$cur_coll.name%> : <%=$cur_coll.intf%>, ILazyLoadableCollection {

        private IDictionary m_items;
        private <%=$cur_coll.container_impl%> m_container;
        private LazyLoader m_loader;

        private bool m_loaded;

        public <%=$cur_coll.type%> this [string name] {
            get {
                if (m_loader != null)
                    m_loader.<%=$cur_coll.pathtoloader%>.Visit (this);
                return m_items [name] as <%=$cur_coll.type%>;
            }
            set { m_items [name] = value; }
        }

        public <%=$cur_coll.container%> Container {
            get { return m_container; }
        }

        public int Count {
            get {
                if (m_loader != null)
                    m_loader.<%=$cur_coll.pathtoloader%>.Visit (this);
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
        
        public <%=$cur_coll.name%> (<%=$cur_coll.container_impl%> container)
        {
            m_container = container;
            m_items = new HybridDictionary ();
        }        

        public <%=$cur_coll.name%> (<%=$cur_coll.container_impl%> container, LazyLoader loader) : this (container)
        {
            m_loader = loader;
        }

        public void Clear ()
        {
            m_items.Clear ();
        }

        public bool Contains (<%=$cur_coll.type%> value)
        {
            return m_items.Contains (value);
        }

        public void Remove (<%=$cur_coll.type%> value)
        {
            m_items.Remove (value);
        }

        public void CopyTo (Array ary, int index)
        {
            if (m_loader != null)
                m_loader.<%=$cur_coll.pathtoloader%>.Visit (this);
            m_items.Values.CopyTo (ary, index);
        }

        public IEnumerator GetEnumerator ()
        {
            if (m_loader != null)
                m_loader.<%=$cur_coll.pathtoloader%>.Visit (this);
            return m_items.Values.GetEnumerator ();
        }

        public void Accept (<%=$cur_coll.visitor%> visitor)
        {
            visitor.<%=$cur_coll.visitThis%> (this);
            <%=$cur_coll.type%> [] items = new <%=$cur_coll.type%> [m_items.Count];
            m_items.Values.CopyTo (items, 0);
            for (int i = 0; i < items.Length; i++)
                items [i].Accept (visitor);
        }
    }
}
