using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileLegendsTool.TableReader
{
    public class DataCache<K, V>
    {
        public DataCache(int capacity)
        {
            this.m_capcity = capacity;
            this.m_objects = new Dictionary<K, LinkedListNode<DataCache<K, V>.CachedItem<K, V>>>(capacity);
            this.m_lru = new LinkedList<DataCache<K, V>.CachedItem<K, V>>();
        }
        public V Get(K key)
        {
            LinkedListNode<DataCache<K, V>.CachedItem<K, V>> linkedListNode;
            if (this.m_objects.TryGetValue(key, out linkedListNode))
            {
                V v = linkedListNode.Value.v;
                this.m_lru.Remove(linkedListNode);
                this.m_lru.AddLast(linkedListNode);
                return v;
            }
            return default(V);
        }
        public ICollection<V> Get(K[] keys)
        {
            ICollection<V> collection = new List<V>(keys.Length);
            foreach (K key in keys)
            {
                LinkedListNode<DataCache<K, V>.CachedItem<K, V>> linkedListNode;
                if (this.m_objects.TryGetValue(key, out linkedListNode))
                {
                    V v = linkedListNode.Value.v;
                    this.m_lru.Remove(linkedListNode);
                    this.m_lru.AddLast(linkedListNode);
                    collection.Add(v);
                }
            }
            return collection;
        }
        public void Add(K key, V val)
        {
            if (this.m_objects.Count >= this.m_capcity)
            {
                this.RemoveLastUsed();
            }
            LinkedListNode<DataCache<K, V>.CachedItem<K, V>> linkedListNode;
            if (this.m_objects.TryGetValue(key, out linkedListNode))
            {
                this.m_lru.Remove(linkedListNode);
            }
            DataCache<K, V>.CachedItem<K, V> value = new DataCache<K, V>.CachedItem<K, V>(key, val);
            linkedListNode = new LinkedListNode<DataCache<K, V>.CachedItem<K, V>>(value);
            this.m_lru.AddLast(linkedListNode);
            this.m_objects[key] = linkedListNode;
        }
        public void Clear()
        {
            this.m_objects.Clear();
            this.m_lru.Clear();
        }
        protected virtual V RemoveLastUsed()
        {
            LinkedListNode<DataCache<K, V>.CachedItem<K, V>> first = this.m_lru.First;
            this.m_lru.RemoveFirst();
            this.m_objects.Remove(first.Value.k);
            return first.Value.v;
        }
        private int m_capcity;
        private readonly IDictionary<K, LinkedListNode<DataCache<K, V>.CachedItem<K, V>>> m_objects;
        private readonly LinkedList<DataCache<K, V>.CachedItem<K, V>> m_lru;
        private class CachedItem<K0, V0>
        {
            public CachedItem(K0 key, V0 value)
            {
                this.k = key;
                this.v = value;
            }
            public K0 k;
            public V0 v;
        }
    }
}
