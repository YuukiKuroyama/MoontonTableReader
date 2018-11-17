using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileLegendsTool.TableReader
{
    public class TableStreamBase<K, T, E, E_DATA> where T : new()
    {
        protected bool ParseHeader(byte[] datas, string tablename)
        {
            if (!TableReader.CheckSignature(datas, tablename))
            {
                return false;
            }
            this.hasEncrypt = TableReader.CheckHasEncrypt(datas);
            if (this.hasEncrypt)
            {
                this.reader = TableStreamBase<K, T, E, E_DATA>.encryptReader;
            }
            else
            {
                this.reader = TableStreamBase<K, T, E, E_DATA>.noEncryptReader;
            }
            this.reader.m_buffer = datas;
            this.reader.ResetPos();
            int num = (int)this.reader.ReadByte();
            this.dataRowNum = this.reader.ReadInt32();
            this.reader.ReadInt32();
            this.useHeaderMask = ((num & 1) != 0);
            this.dynamicParse = ((num & 2) != 0);
            this.m_bUsePKeyHash = ((num & 4) != 0);
            if (this.dynamicParse)
            {
                this.dicStartPos = this.reader.ReadInt32();
                this.datas = datas;
            }
            if (this.m_bUsePKeyHash)
            {
                this.pkHashStartPos = this.reader.ReadInt32();
            }
            return true;
        }
        protected void ParseDictionary()
        {
            this.reader.SetPos(this.dicStartPos);
            this.reader.ReadUInt32();
            object obj;
            if (typeof(K) == typeof(string))
            {
                obj = this.reader.ReadStringKeyDic();
            }
            else if (typeof(K) == typeof(uint))
            {
                obj = this.reader.ReadUIntKeyDic();
            }
            else
            {
                obj = this.reader.ReadIntKeyDic();
            }
            this.m_keyDic = (obj as Dictionary<K, RowInfo>);
            if (TableStreamBase<K, T, E, E_DATA>.m_dynamicParseClose)
            {
                this.ParseAllElement(false);
                this.datas = null;
                return;
            }
            this.ParseOneElement();
        }
        protected void ParsePKHashDictionary()
        {
            this.reader.SetPos(this.pkHashStartPos);
            this.reader.ReadUInt32();
            Dictionary<string, uint> l = this.reader.ReadStringKeyUIntValDic();
            this.InitPKHash(l);
        }
        public void ClearLoadedDatas()
        {
            this.m_Data.Clear();
            this.m_Data_IsFullLoaded = false;
        }
        private void ParseOneElement()
        {
            Dictionary<K, RowInfo>.Enumerator enumerator = this.m_keyDic.GetEnumerator();
            if (enumerator.MoveNext())
            {
                KeyValuePair<K, RowInfo> keyValuePair = enumerator.Current;
                this.TryDynamicParseObject(keyValuePair.Key, true);
            }
        }
        protected void ParseAllElement(bool bClearFirst = true)
        {
            if (bClearFirst)
            {
                this.ClearLoadedDatas();
            }
            foreach (KeyValuePair<K, RowInfo> keyValuePair in this.m_keyDic)
            {
                this.TryDynamicParseObject(keyValuePair.Key, true);
            }
            this.m_Data_IsFullLoaded = true;
        }
        private bool TryInitDynamicParse(K key)
        {
            RowInfo rowInfo;
            if (this.m_keyDic.TryGetValue(key, out rowInfo))
            {
                this.reader.m_buffer = this.datas;
                this.reader.SetPos(rowInfo.startPosition);
                return true;
            }
            return false;
        }
        protected virtual E ParseElement(bool bAddToMData = true)
        {
            return default(E);
        }
        protected E TryDynamicParseObject(K key, bool bAddToMData = true)
        {
            if (this.TryInitDynamicParse(key))
            {
                this.m_curentParseKey = key;
                return this.ParseElement(bAddToMData);
            }
            return default(E);
        }
        protected E TryGetFromCacheThenDynamicParse(K KeyName)
        {
            E e = default(E);
            if (this.m_DataCache != null)
            {
                e = this.m_DataCache.Get(KeyName);
            }
            if (e == null)
            {
                e = this.TryDynamicParseObject(KeyName, false);
                if (e != null && this.m_DataCache != null)
                {
                    this.m_DataCache.Add(KeyName, e);
                }
            }
            return e;
        }
        public void LoadDocument(object data)
        {
            if (data is string)
            {
                this.LoadData(data as string);
                return;
            }
            if (data is byte[])
            {
                this.LoadDataBinary(data as byte[]);
            }
        }
        public void LoadData(string tabName, string szText)
        {
            this.strTabName = tabName;
            this.LoadData(szText);
        }
        public void LoadDocument(string tabName, object data)
        {
            this.strTabName = tabName;
            this.LoadDocument(data);
        }
        public void LoadDataBinary(string tabName, byte[] datas)
        {
            this.strTabName = tabName;
            this.LoadDataBinary(datas);
        }
        public virtual void LoadData(string szText)
        {
        }
        public virtual void LoadDataBinary(byte[] datas)
        {
        }
        public void InitDataCache(bool bEnable, int capcity = 0)
        {
            this.m_bUseDataCache = bEnable;
            if (bEnable)
            {
                this.m_DataCache = new DataCache<K, E>(capcity);
            }
        }
        protected void InitPKHash(Dictionary<string, uint> L0)
        {
            this.m_PKHash = new PKHash(L0);
        }
        public uint ConvertPKey(string sKey)
        {
            if (sKey == null)
            {
                return 0u;
            }
            return this.m_PKHash.ConvertToIntId(sKey);
        }
        public bool IsPKeyEqual(string sKey, uint iKey)
        {
            uint num = this.ConvertPKey(sKey);
            return num == iKey;
        }
        public TableStreamBase()
        {
        }
        static TableStreamBase()
        {
        }
        public static readonly bool m_dynamicParseClose = false;
        protected static readonly string[] emptyStrArray = new string[0];
        protected static readonly int[] emptyIntArray = new int[0];
        protected static readonly uint[] emptyUIntArray = new uint[0];
        protected static readonly float[] emptyFloatArray = new float[0];
        protected static readonly double[] emptyDoubleArray = new double[0];
        protected static readonly string[][] emptyStrArray2 = new string[0][];
        protected static readonly int[][] emptyIntArray2 = new int[0][];
        protected static Dictionary<K, RowInfo> emptyKeyDic = new Dictionary<K, RowInfo>();
        public static TableReader noEncryptReader = new TableReader();
        public static EncryptTableReader encryptReader = new EncryptTableReader();
        protected string strTabName = "";
        protected bool dynamicParse;
        protected bool useHeaderMask;
        protected int dataRowNum;
        protected int dicStartPos;
        protected bool hasEncrypt;
        protected TableReader reader;
        protected byte[] datas;
        protected string tablename;
        protected Dictionary<K, RowInfo> m_keyDic = TableStreamBase<K, T, E, E_DATA>.emptyKeyDic;
        protected K m_curentParseKey;
        protected bool m_Data_IsFullLoaded;
        protected Dictionary<K, E_DATA> m_Data = new Dictionary<K, E_DATA>();
        public bool m_bUseDataCache;
        protected bool bLoadFirstObj;
        protected DataCache<K, E> m_DataCache;
        protected PKHash m_PKHash;
        public bool m_bUsePKeyHash;
        protected int pkHashStartPos;
    }
}
