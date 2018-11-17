using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileLegendsTool.TableReader
{
    public class EncryptTableReader : TableReader
    {
        static EncryptTableReader()
        {
            if (EncryptTableReader.key.Length < 256)
            {
                Console.WriteLine("Error table: secret key length is not enough!");
            }
            EncryptTableReader.intkey = new int[EncryptTableReader.key.Length];
            for (int i = 0; i < EncryptTableReader.key.Length; i++)
            {
                EncryptTableReader.intkey[i] = (int)EncryptTableReader.key[i];
            }
        }
        public override int ReadInt32()
        {
            int num = (int)this.m_buffer[this.p++] | (int)this.m_buffer[this.p++] << 8 | (int)this.m_buffer[this.p++] << 16 | (int)this.m_buffer[this.p++] << 24;
            int[] array = EncryptTableReader.intkey;
            byte keyPos = this.keyPos;
            this.keyPos += 1;
            return num ^ array[(int)keyPos];
        }
        public override uint ReadUInt32()
        {
            uint num = (uint)((int)this.m_buffer[this.p++] | (int)this.m_buffer[this.p++] << 8 | (int)this.m_buffer[this.p++] << 16 | (int)this.m_buffer[this.p++] << 24);
            uint[] array = EncryptTableReader.key;
            byte keyPos = this.keyPos;
            this.keyPos += 1;
            return num ^ array[(int)keyPos];
        }
        public unsafe override float ReadSingle()
        {
            uint num = (uint)((int)this.m_buffer[this.p++] | (int)this.m_buffer[this.p++] << 8 | (int)this.m_buffer[this.p++] << 16 | (int)this.m_buffer[this.p++] << 24);
            uint[] array = EncryptTableReader.key;
            byte keyPos = this.keyPos;
            this.keyPos += 1;
            uint num2 = num ^ array[(int)keyPos];
            return *(float*)(&num2);
        }
        public unsafe override double ReadDouble()
        {
            uint num = (uint)((int)this.m_buffer[this.p++] | (int)this.m_buffer[this.p++] << 8 | (int)this.m_buffer[this.p++] << 16 | (int)this.m_buffer[this.p++] << 24);
            uint[] array = EncryptTableReader.key;
            byte keyPos = this.keyPos;
            this.keyPos += 1;
            uint num2 = num ^ array[(int)keyPos];
            uint num3 = (uint)((int)this.m_buffer[this.p++] | (int)this.m_buffer[this.p++] << 8 | (int)this.m_buffer[this.p++] << 16 | (int)this.m_buffer[this.p++] << 24);
            uint[] array2 = EncryptTableReader.key;
            byte keyPos2 = this.keyPos;
            this.keyPos += 1;
            uint num4 = num3 ^ array2[(int)keyPos2];
            ulong num5 = (ulong)num4 << 32 | (ulong)num2;
            return *(double*)(&num5);
        }
        public EncryptTableReader()
        {
        }
        private static uint[] key = new uint[]
	{
		1361273563u,
		2102303378u,
		1917946223u,
		369148386u,
		63253552u,
		759199846u,
		950622838u,
		1682881435u,
		758998789u,
		262728207u,
		1840019903u,
		82601967u,
		741477343u,
		530480596u,
		796042895u,
		1945434605u,
		934709322u,
		1019586916u,
		1319805199u,
		459383425u,
		808312465u,
		1142164890u,
		1290557720u,
		1221919235u,
		1422868052u,
		2028728399u,
		1891896456u,
		805140959u,
		244729985u,
		1814018417u,
		589925179u,
		637453574u,
		1221464171u,
		2041276634u,
		655937399u,
		568373625u,
		905111051u,
		1378328348u,
		1845959586u,
		1201014061u,
		904666964u,
		1102547544u,
		965898429u,
		272564635u,
		703292425u,
		421552230u,
		423840577u,
		506378063u,
		201377792u,
		1624998161u,
		1979228109u,
		360036406u,
		920903561u,
		156683302u,
		33381579u,
		723819989u,
		880839207u,
		2024153236u,
		1860694634u,
		1642363574u,
		2001572442u,
		1719778137u,
		1984405496u,
		1705468375u,
		1505544890u,
		737472359u,
		1264187185u,
		468912708u,
		1974671818u,
		374490665u,
		1521594028u,
		428331259u,
		818209124u,
		1842290685u,
		627638963u,
		448276059u,
		221261329u,
		1133874418u,
		1188537656u,
		699048063u,
		1147889192u,
		2015226867u,
		1091929972u,
		749850058u,
		1959929622u,
		1017630689u,
		800531725u,
		1663479443u,
		535731744u,
		2065948687u,
		1451670087u,
		436198343u,
		1551140177u,
		1471468921u,
		1826903680u,
		476335705u,
		284338420u,
		1271091391u,
		1792409319u,
		255016366u,
		200290901u,
		1437449806u,
		1465324054u,
		1649813376u,
		477108969u,
		2111484889u,
		1415590081u,
		171053503u,
		344237327u,
		1163234537u,
		2070771911u,
		1364843411u,
		1488421492u,
		1942229594u,
		190693487u,
		1565374099u,
		168637960u,
		512936575u,
		2026048342u,
		1029209185u,
		453133939u,
		2140579441u,
		823987036u,
		1719655452u,
		174199764u,
		84144222u,
		1110490852u,
		1315879395u,
		1365181716u,
		663637721u,
		1180169625u,
		50207826u,
		789637091u,
		25303119u,
		775759799u,
		1930529428u,
		526805375u,
		1297184025u,
		559156571u,
		394555523u,
		848992729u,
		287595150u,
		1784914748u,
		1654006206u,
		1612814748u,
		1458574293u,
		1759694954u,
		1978968372u,
		1297269157u,
		1742759458u,
		1513328500u,
		1115942672u,
		2053393322u,
		1128771598u,
		1222330388u,
		150083075u,
		647812715u,
		1440020935u,
		874053577u,
		694063188u,
		1584679514u,
		118406056u,
		1759380579u,
		2097165451u,
		314241808u,
		1783176761u,
		1727412310u,
		1981898933u,
		329414846u,
		879602841u,
		1953162792u,
		337153235u,
		1363151065u,
		283288884u,
		1663364332u,
		1484674914u,
		87186119u,
		1842699085u,
		497325064u,
		24116689u,
		1583815154u,
		1817953564u,
		441825818u,
		671118528u,
		1226441854u,
		1061763569u,
		438310894u,
		839955287u,
		1858544958u,
		1140066685u,
		203117118u,
		692390089u,
		967769179u,
		1827037377u,
		588876378u,
		511839494u,
		1071927732u,
		1501625864u,
		2138125521u,
		128139834u,
		1371388174u,
		2064479516u,
		1481643308u,
		1273152468u,
		158944304u,
		1842858583u,
		674116854u,
		1382274794u,
		2049813391u,
		160566819u,
		1859255828u,
		1955341075u,
		1728959624u,
		1881470539u,
		490946070u,
		892289425u,
		1298120524u,
		2079826849u,
		1508289073u,
		1949885961u,
		711249029u,
		225786446u,
		1991257059u,
		201275012u,
		1655698314u,
		2036166923u,
		1002993574u,
		89998597u,
		124344580u,
		1967989396u,
		810558060u,
		852394972u,
		1940369341u,
		336758245u,
		312344508u,
		1775957726u,
		88993940u,
		707838926u,
		180172458u,
		334152429u,
		1911126692u,
		505967692u,
		1479149861u,
		2056142644u,
		428817656u,
		2124814319u,
		848616677u,
		766494167u,
		171339063u,
		700193102u,
		1656329567u,
		981929135u,
		1377281284u,
		170136125u,
		1465065421u,
		518993202u
	};
        private static int[] intkey = null;
    }
}
