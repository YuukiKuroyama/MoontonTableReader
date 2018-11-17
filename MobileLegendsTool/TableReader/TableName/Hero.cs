using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MobileLegendsTool.TableReader.TableName
{
    public class Hero_Element
    {
        public bool GetError()
        {
            return this.m_bError;
        }
        public void SetError(bool bError)
        {
            this.m_bError = bError;
        }

        public bool m_bError = true;
        public static Hero_Element nullObj = new Hero_Element();
        public int m_ID;
        public string m_mName;
        public int m_SkinId;
        public int m_PreLoadID;
        public string m_Prefabs;
        public float m_Size;
        public int m_GuideType;
        public string m_Head;
        public string m_HeadExt;
        public string m_SmallMap;
        public string m_LargeMap;
        public int[][] m_SkillList;
        public int[] m_Sort;
        public string m_SortIcon;
        public int[] m_Specialty;
        public int[] m_AbilityShow;
        public int m_occupation;
        public int[] m_RecommendEquip;
        public int m_SurvivalHeroSort;
        public int[][] m_SurvivalRecommendEquipType;
        public int m_RecommendRune;
        public int[] m_RecommendSkill;
        public int[] m_RecommendLevel;
        public string[] m_HeroVoice;
        public string[] m_DeathVoice;
        public int m_TypeRadius;
        public int m_BaseSpeed;
        public int m_MoveSpeed;
        public int m_BasePhyAtt;
        public int m_BaseMagAtt;
        public int m_AttSpeed;
        public float m_AttSpeed_show;
        public int m_PhyBaseShield;
        public int m_MagBaseShield;
        public int m_BaseHp;
        public int m_FightHpRec;
        public int m_BaseMp;
        public int m_FightMpRec;
        public int m_XpMax;
        public int[] m_CostType;
        public int m_Crit;
        public int m_HeivySpellAtt;
        public int m_LevelPhyAtt;
        public int m_LevelMagAtt;
        public int m_LevelAttSpeed;
        public int m_PhyLevelShield;
        public int m_MagLevelShield;
        public int m_LevelHp;
        public int m_HPLevelRec;
        public int m_LevelMp;
        public int m_MPLevelRec;
        public int[] m_HeroGroup;
        public int[] m_HeroTask;
        public string m_mTale;
    }
    public class Hero : TableStream<int, Hero, Hero_Element>
    {
        public override void LoadDataBinary(byte[] datas)
        {
            this.tablename = this.strTabName;
            if (string.IsNullOrEmpty(this.tablename))
            {
                this.tablename = Hero.m_defaultTablename;
            }
            if (!base.ParseHeader(datas, this.tablename))
            {
                return;
            }
            if (this.useHeaderMask)
            {
                throw new Exception("Hero.csv, the mask flag of the table binary data is not match with the parse code, please regenarate the code  use UTF8csv tool!!!");
            }
            if (this.m_bUsePKeyHash)
            {
                base.ParsePKHashDictionary();
            }
            if (this.dynamicParse)
            {
                base.ParseDictionary();
                return;
            }
            for (int i = 0; i < this.dataRowNum; i++)
            {
                this.ParseElement(true);
            }
            if (this.m_Data.Count == 0)
            {
                return;
            }
            this.reader.Clear();
        }
        protected override Hero_Element ParseElement(bool bAddToMData = true)
        {
            Hero_Element cdata_Hero_Element = new Hero_Element();
            cdata_Hero_Element.SetError(false);
            try
            {
                if (this.dynamicParse)
                {
                    cdata_Hero_Element.m_ID = this.m_curentParseKey;
                }
                else
                {
                    cdata_Hero_Element.m_ID = this.reader.ReadInt32();
                }
                cdata_Hero_Element.m_mName = this.tablename + "_mName_" + cdata_Hero_Element.m_ID;
                cdata_Hero_Element.m_SkinId = this.reader.ReadInt32();
                cdata_Hero_Element.m_PreLoadID = this.reader.ReadInt32();
                cdata_Hero_Element.m_Prefabs = this.reader.ReadString();
                cdata_Hero_Element.m_Size = this.reader.ReadSingle();
                cdata_Hero_Element.m_GuideType = this.reader.ReadInt32();
                cdata_Hero_Element.m_Head = this.reader.ReadString();
                cdata_Hero_Element.m_HeadExt = this.reader.ReadString();
                cdata_Hero_Element.m_SmallMap = this.reader.ReadString();
                cdata_Hero_Element.m_LargeMap = this.reader.ReadString();
                cdata_Hero_Element.m_SkillList = this.reader.ReadIntArray2();
                cdata_Hero_Element.m_Sort = this.reader.ReadIntArray();
                cdata_Hero_Element.m_SortIcon = this.reader.ReadString();
                cdata_Hero_Element.m_Specialty = this.reader.ReadIntArray();
                cdata_Hero_Element.m_AbilityShow = this.reader.ReadIntArray();
                cdata_Hero_Element.m_occupation = this.reader.ReadInt32();
                cdata_Hero_Element.m_RecommendEquip = this.reader.ReadIntArray();
                cdata_Hero_Element.m_SurvivalHeroSort = this.reader.ReadInt32();
                cdata_Hero_Element.m_SurvivalRecommendEquipType = this.reader.ReadIntArray2();
                cdata_Hero_Element.m_RecommendRune = this.reader.ReadInt32();
                cdata_Hero_Element.m_RecommendSkill = this.reader.ReadIntArray();
                cdata_Hero_Element.m_RecommendLevel = this.reader.ReadIntArray();
                cdata_Hero_Element.m_HeroVoice = this.reader.ReadStringArray();
                cdata_Hero_Element.m_DeathVoice = this.reader.ReadStringArray();
                cdata_Hero_Element.m_TypeRadius = this.reader.ReadInt32();
                cdata_Hero_Element.m_BaseSpeed = this.reader.ReadInt32();
                cdata_Hero_Element.m_MoveSpeed = this.reader.ReadInt32();
                cdata_Hero_Element.m_BasePhyAtt = this.reader.ReadInt32();
                cdata_Hero_Element.m_BaseMagAtt = this.reader.ReadInt32();
                cdata_Hero_Element.m_AttSpeed = this.reader.ReadInt32();
                cdata_Hero_Element.m_AttSpeed_show = this.reader.ReadSingle();
                cdata_Hero_Element.m_PhyBaseShield = this.reader.ReadInt32();
                cdata_Hero_Element.m_MagBaseShield = this.reader.ReadInt32();
                cdata_Hero_Element.m_BaseHp = this.reader.ReadInt32();
                cdata_Hero_Element.m_FightHpRec = this.reader.ReadInt32();
                cdata_Hero_Element.m_BaseMp = this.reader.ReadInt32();
                cdata_Hero_Element.m_FightMpRec = this.reader.ReadInt32();
                cdata_Hero_Element.m_XpMax = this.reader.ReadInt32();
                cdata_Hero_Element.m_CostType = this.reader.ReadIntArray();
                cdata_Hero_Element.m_Crit = this.reader.ReadInt32();
                cdata_Hero_Element.m_HeivySpellAtt = this.reader.ReadInt32();
                cdata_Hero_Element.m_LevelPhyAtt = this.reader.ReadInt32();
                cdata_Hero_Element.m_LevelMagAtt = this.reader.ReadInt32();
                cdata_Hero_Element.m_LevelAttSpeed = this.reader.ReadInt32();
                cdata_Hero_Element.m_PhyLevelShield = this.reader.ReadInt32();
                cdata_Hero_Element.m_MagLevelShield = this.reader.ReadInt32();
                cdata_Hero_Element.m_LevelHp = this.reader.ReadInt32();
                cdata_Hero_Element.m_HPLevelRec = this.reader.ReadInt32();
                cdata_Hero_Element.m_LevelMp = this.reader.ReadInt32();
                cdata_Hero_Element.m_MPLevelRec = this.reader.ReadInt32();
                cdata_Hero_Element.m_HeroGroup = this.reader.ReadIntArray();
                cdata_Hero_Element.m_HeroTask = this.reader.ReadIntArray();
                cdata_Hero_Element.m_mTale = this.tablename + "_mTale_" + cdata_Hero_Element.m_ID;
                if (bAddToMData)
                {
                    this._addItem(cdata_Hero_Element.m_ID, cdata_Hero_Element);
                }
            }
            catch (Exception ex)
            {
            }
            return cdata_Hero_Element;
        }
        public int GetCount()
        {
            if (this.dynamicParse)
            {
                return this.m_keyDic.Count;
            }
            return this.m_Data.Count;
        }
        public bool HasKey(int key)
        {
            if (this.dynamicParse)
            {
                return this.m_keyDic.ContainsKey(key);
            }
            return this.m_Data.ContainsKey(key);
        }
        public void Destory()
        {
            this.strTabName = "";
            base.ClearLoadedDatas();
            this.m_keyDic.Clear();
            if (this.m_DataCache != null)
            {
                this.m_DataCache.Clear();
            }
            this.datas = null;
        }
        public static Hero GetInstance()
        {
            if (Hero.m_Instance == null)
            {
                Hero.m_Instance = new Hero();
            }
            return Hero.m_Instance;
        }
        public Dictionary<int, Hero_Element> GetAll()
        {
            if (this.dynamicParse && !this.m_Data_IsFullLoaded)
            {
                base.ParseAllElement(true);
                this.m_Data_IsFullLoaded = true;
            }
            return this.m_Data;
        }
        private bool _addItem(int ID, Hero_Element element)
        {
            if (this.m_Data.ContainsKey(ID))
            {
                return false;
            }
            this.m_Data[ID] = element;
            return true;
        }
        public Hero_Element GetValue_ByID(int ID)
        {
            Hero_Element result;
            if (this.m_Data.TryGetValue(ID, out result))
            {
                return result;
            }
            if (this.dynamicParse && (result = base.TryDynamicParseObject(ID, true)) != null)
            {
                return result;
            }
            return Hero_Element.nullObj;
        }
        public static readonly string m_defaultTablename = "Hero";
        private static Hero m_Instance;
    }
}
