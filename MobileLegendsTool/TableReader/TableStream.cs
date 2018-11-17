using System;
using System.Collections.Generic;

namespace MobileLegendsTool.TableReader
{
    public class TableStream<K, T, E> : MobileLegendsTool.TableReader.TableStreamBase<K, T, E, E> where T : new()
    {
        public TableStream<K, T, E>.DataEnumerator GetEnumerator()
        {
            return new TableStream<K, T, E>.DataEnumerator(this);
        }
        public struct DataEnumerator
        {
            public DataEnumerator(TableStream<K, T, E> tbl)
            {
                this._table = tbl;
                this._dataVisitor = this._table.m_keyDic.GetEnumerator();
            }

            public KeyValuePair<K, E> Current
            {
                get
                {
                    KeyValuePair<K, RowInfo> keyValuePair = this._dataVisitor.Current;
                    E value;
                    if (this._table.m_Data != null && this._table.m_Data.ContainsKey(keyValuePair.Key))
                    {
                        value = this._table.m_Data[keyValuePair.Key];
                    }
                    else
                    {
                        value = this._table.TryGetFromCacheThenDynamicParse(keyValuePair.Key);
                    }
                    return new KeyValuePair<K, E>(keyValuePair.Key, value);
                }
            }

            public bool MoveNext()
            {
                return this._dataVisitor.MoveNext();
            }

            public void Reset()
            {
                this._dataVisitor = this._table.m_keyDic.GetEnumerator();
            }

            private TableStream<K, T, E> _table;

            private Dictionary<K, RowInfo>.Enumerator _dataVisitor;
        }
    }

}
