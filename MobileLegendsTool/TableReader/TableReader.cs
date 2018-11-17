using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace MobileLegendsTool.TableReader
{
    public class TableReader
    {
        public Dictionary<string, RowInfo> ReadStringKeyDic()
        {
            int num = this.ReadInt32();
            Dictionary<string, RowInfo> dictionary = new Dictionary<string, RowInfo>(num);
            for (int i = 0; i < num; i++)
            {
                string key = this.ReadString();
                RowInfo value;
                value.startPosition = this.ReadInt32();
                value.size = this.ReadInt16();
                dictionary[key] = value;
            }
            return dictionary;
        }
        public Dictionary<int, RowInfo> ReadIntKeyDic()
        {
            int num = this.ReadInt32();
            Dictionary<int, RowInfo> dictionary = new Dictionary<int, RowInfo>(num);
            for (int i = 0; i < num; i++)
            {
                int key = this.ReadInt32();
                RowInfo value;
                value.startPosition = this.ReadInt32();
                value.size = this.ReadInt16();
                dictionary[key] = value;
            }
            return dictionary;
        }
        public Dictionary<uint, RowInfo> ReadUIntKeyDic()
        {
            int num = this.ReadInt32();
            Dictionary<uint, RowInfo> dictionary = new Dictionary<uint, RowInfo>(num);
            for (int i = 0; i < num; i++)
            {
                uint key = this.ReadUInt32();
                RowInfo value;
                value.startPosition = this.ReadInt32();
                value.size = this.ReadInt16();
                dictionary[key] = value;
            }
            return dictionary;
        }
        public Dictionary<string, uint> ReadStringKeyUIntValDic()
        {
            int num = this.ReadInt32();
            Dictionary<string, uint> dictionary = new Dictionary<string, uint>(num);
            for (int i = 0; i < num; i++)
            {
                string key = this.ReadString();
                uint value = this.ReadUInt32();
                dictionary[key] = value;
            }
            return dictionary;
        }
        public int[] ReadIntArray()
        {
            byte b = this.ReadByte();
            int[] array = new int[(int)b];
            for (int i = 0; i < (int)b; i++)
            {
                array[i] = this.ReadInt32();
            }
            return array;
        }
        public float[] ReadFloatArray()
        {
            byte b = this.ReadByte();
            float[] array = new float[(int)b];
            for (int i = 0; i < (int)b; i++)
            {
                array[i] = this.ReadSingle();
            }
            return array;
        }
        public int[][] ReadIntArray2()
        {
            byte b = this.ReadByte();
            int[][] array = new int[(int)b][];
            for (int i = 0; i < (int)b; i++)
            {
                byte b2 = this.ReadByte();
                int[] array2 = array[i] = new int[(int)b2];
                for (int j = 0; j < (int)b2; j++)
                {
                    array2[j] = this.ReadInt32();
                }
            }
            return array;
        }
        public string[] ReadStringArray()
        {
            byte b = this.ReadByte();
            string[] array = new string[(int)b];
            for (int i = 0; i < (int)b; i++)
            {
                array[i] = this.ReadString();
            }
            return array;
        }
        public string[][] ReadStringArray2()
        {
            byte b = this.ReadByte();
            string[][] array = new string[(int)b][];
            for (int i = 0; i < (int)b; i++)
            {
                byte b2 = this.ReadByte();
                string[] array2 = array[i] = new string[(int)b2];
                for (int j = 0; j < (int)b2; j++)
                {
                    array2[j] = this.ReadString();
                }
            }
            return array;
        }
        public virtual int ReadInt32()
        {
            return (int)this.m_buffer[this.p++] | (int)this.m_buffer[this.p++] << 8 | (int)this.m_buffer[this.p++] << 16 | (int)this.m_buffer[this.p++] << 24;
        }
        public virtual short ReadInt16()
        {
            return (short)((int)this.m_buffer[this.p++] | (int)this.m_buffer[this.p++] << 8);
        }
        public virtual uint ReadUInt32()
        {
            return (uint)((int)this.m_buffer[this.p++] | (int)this.m_buffer[this.p++] << 8 | (int)this.m_buffer[this.p++] << 16 | (int)this.m_buffer[this.p++] << 24);
        }
        public unsafe virtual float ReadSingle()
        {
            uint num = (uint)((int)this.m_buffer[this.p++] | (int)this.m_buffer[this.p++] << 8 | (int)this.m_buffer[this.p++] << 16 | (int)this.m_buffer[this.p++] << 24);
            return *(float*)(&num);
        }
        public unsafe virtual double ReadDouble()
        {
            uint num = (uint)((int)this.m_buffer[this.p++] | (int)this.m_buffer[this.p++] << 8 | (int)this.m_buffer[this.p++] << 16 | (int)this.m_buffer[this.p++] << 24);
            uint num2 = (uint)((int)this.m_buffer[this.p++] | (int)this.m_buffer[this.p++] << 8 | (int)this.m_buffer[this.p++] << 16 | (int)this.m_buffer[this.p++] << 24);
            ulong num3 = (ulong)num2 << 32 | (ulong)num;
            return *(double*)(&num3);
        }
        public byte ReadByte()
        {
            return this.m_buffer[this.p++];
        }
        public bool ReadBoolean()
        {
            return this.m_buffer[this.p++] != 0;
        }
        public string ReadString()
        {
            int num = 0;
            int num2 = 0;
            while (num2 != 35)
            {
                byte b = this.ReadByte();
                num |= (int)(b & 127) << num2;
                num2 += 7;
                if ((b & 128) == 0)
                {
                    break;
                }
            }
            if (num < 0)
            {
                throw new IOException("table string header format error!" + num);
            }
            if (num == 0)
            {
                return string.Empty;
            }
            int length = 0;
            try
            {
                length = TableReader.m_decoder.GetChars(this.m_buffer, this.p, num, TableReader.m_charBuffer, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            this.p += num;
            return new string(TableReader.m_charBuffer, 0, length);
        }
        public void ResetPos()
        {
            this.keyPos = 0;
            this.p = TableReader.signature.Length + 1;
        }
        public void SetPos(int pos)
        {
            this.p = pos;
        }
        public void Clear()
        {
            this.ResetPos();
            this.m_buffer = null;
        }
        public static bool CheckSignature(byte[] data, string tablename)
        {
            for (int i = 0; i < TableReader.signature.Length; i++)
            {
                if (TableReader.signature[i] != (char)data[i])
                {
                    Console.WriteLine("Error: table binary data error, invalid signature! table : " + tablename);
                    return false;
                }
            }
            return true;
        }
        public static bool CheckHasEncrypt(byte[] data)
        {
            return data[TableReader.signature.Length] != 0;
        }
        protected static readonly string signature = "$mulonggame_table_1";
        public byte[] m_buffer;
        protected int p;
        protected byte keyPos;
        private static readonly int MAX_STRING_LEN = 20480;
        private static char[] m_charBuffer = new char[TableReader.MAX_STRING_LEN];
        private static System.Text.Decoder m_decoder = new UTF8Encoding().GetDecoder();
    }

}
