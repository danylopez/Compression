using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShannonFano_namespace
{
    class CrearShannonFano
    {
        static double totalprobabilidad;
        static List<NodoShannonFano> s = new List<NodoShannonFano>();
        //List
        public List<Compression.Valor> crearShannonFano(List<Compression.Valor> valores)
        {
            int i,j;
            NodoShannonFano temp = new NodoShannonFano();
            for (i = 0; i < valores.Count; i++)
            {
                NodoShannonFano valor = new NodoShannonFano();
                valor.sym = valores[i].nombre;
                valor.pro = (float)valores[i].probabilidad;
                totalprobabilidad = totalprobabilidad + valores[i].probabilidad;
                s.Add(valor);
            }
            NodoShannonFano aux = new NodoShannonFano();
            aux.pro = 0;
            s.Add(aux);
            //Console.Write("El valor de i es: "+i.ToString());
            s[i].pro = 1 - (float)totalprobabilidad;
            for(j = 1; j <= valores.Count - 1; j++)
            {
                for (i = 0; i < valores.Count - 1; i++)
                {
                    if ((s[i].pro) > (s[i + 1].pro))
                    {
                        temp.pro = s[i].pro;
                        temp.sym = s[i].sym;
                        s[i].pro = s[i + 1].pro;
                        s[i].sym = s[i + 1].sym;
                        s[i + 1].pro = temp.pro;
                        s[i + 1].sym = temp.sym;
                    }
                }
            }
            for (i = 0; i < valores.Count + 1; i++)
            {
                s[i].top = -1;
                s[i].arr = new int[20] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
            }
            ShannonFano shannon = new ShannonFano();
            shannon.desarrollo(0, valores.Count - 1, s, valores);
            return valores;
        }
    }
}
