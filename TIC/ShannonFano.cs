using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShannonFano_namespace
{
    class ShannonFano
    {
        public void desarrollo(int l, int h, List<NodoShannonFano> s, List<Compression.Valor> valores)
        {
            float pack1 = 0, pack2 = 0, diff1 = 0, diff2 = 0;
            int i, k=0, j;
            String[] aux;
            aux = new string[valores.Count];
            if ((l + 1) == h || l == h || l > h)
            {
                if (l == h || l > h)
                {
                    return;
                }
                s[h].arr[++(s[h].top)] = 0;
                s[l].arr[++(s[l].top)] = 1;
                return;
            }
            else
            {
                for (i = l; i <= h - 1; i++)
                {
                    pack1 = pack1 + s[i].pro;
                }
                pack2 = pack2 + s[h].pro;
                diff1 = pack1 - pack2;
                if (diff1 < 0)
                {
                    diff1 = diff1 * -1;
                }
                j = 2;
                while (j != h - l + 1)
                {
                    k = h - j;
                    pack1 = pack2 = 0;
                    for (i = l; i <= k; i++)
                    {
                        pack1 = pack1 + s[i].pro;
                    }
                    for (i = h; i > k; i--)
                    {
                        pack2 = pack2 + s[i].pro;
                    }
                    diff2 = pack1 - pack2;
                    if (diff2 < 0)
                    {
                        diff2 = diff2 * -1;
                    }
                    if (diff2 >= diff1)
                    {
                        break;
                    }
                    diff1 = diff2;
                    j++;
                }
                k++;
                for (i = l; i <= k; i++)
                {
                    s[i].arr[++(s[i].top)] = 1;
                }
                for (i = k + 1; i <= h; i++)
                {
                    s[i].arr[++(s[i].top)] = 0;
                }

                ShannonFano shannon = new ShannonFano();
                shannon.desarrollo(l, k, s, valores);
                shannon.desarrollo(k + 1, h, s, valores);

                for (i = 0; i < valores.Count ; i++)
                {
                    for (j = 0; j <= s[i].arr.Length-1; j++)
                    {
                        aux[i] = aux[i] + s[i].arr[j].ToString();
                    }
                    valores[i].codigoshannonfano = aux[i].Trim(new Char[] { '2' });
                }
                
            }
        }

        public ShannonFano()
        {
            // TODO: Complete member initialization
        }
    }
}
