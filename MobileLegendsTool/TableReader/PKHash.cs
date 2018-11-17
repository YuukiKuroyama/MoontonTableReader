using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileLegendsTool.TableReader
{
    public class PKHash
    {
        public PKHash(Dictionary<string, uint> l)
        {
            this.L0 = l;
        }
        public uint ConvertToIntId(string sStrId)
        {
            uint result = 0u;
            if (this.L0.TryGetValue(sStrId, out result))
            {
                return result;
            }
            return (uint)sStrId.GetHashCode();
        }
        public Dictionary<string, uint> L0;
    }

}
