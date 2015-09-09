using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Compression
{
    class Desarrollo
    {
        public List<Valor> llenarvector(string linea)
        {
            int i, j, tam, aux;
            List<Valor> valores = new List<Valor>();//Lista de valores
            //Calcular los valores
            for (i = 0; i < linea.Length; i++)
            {
                //El primero se mete porque no hay datos aun
                if (valores.Count == 0)
                {
                    Valor valor = new Valor();
                    valor.nombre = linea[i];
                    valor.frecuencia = 0;
                    valor.probabilidad = 0;
                    valor.codigohuffman = "";
                    valor.codigoshannonfano = "";
                    valor.codigo = Convert.ToString(Convert.ToInt32(((int)linea[i]).ToString("X"), 16), 2);
                    valores.Add(valor);
                }
                else
                {
                    //Se valida por medio de una bandera si el dato ya esta
                    tam = valores.Count;
                    aux = 0;
                    for (j = 0; j < tam; j++)
                    {
                        if (valores[j].nombre.Equals(linea[i]) == true)
                        {
                            aux = 1;
                            break;
                        }
                    }
                    //Si no esta el dato entra
                    if (aux == 0)
                    {
                        Valor valor = new Valor();
                        valor.nombre = linea[i];
                        valor.frecuencia = 0;
                        valor.probabilidad = 0;
                        valor.codigohuffman = "";
                        valor.codigoshannonfano = "";
                        valor.codigo = Convert.ToString(Convert.ToInt32(((int)linea[i]).ToString("X"), 16), 2);
                        valores.Add(valor);
                    }
                }
            }

            tam = valores.Count;
            int total = linea.Length;

            //Se calculan las frecuencias
            for (i = 0; i < linea.Length; i++)
            {
                for (j = 0; j < tam; j++)
                {
                    //Si el valor ya esta, se suma la frecuencia
                    if (valores[j].nombre.Equals(linea[i]) == true)
                    {
                        valores[j].frecuencia = valores[j].frecuencia + 1;
                    }
                }
            }

            //Se calculan las probabilidades
            for (i = 0; i < tam; i++)
            {
                valores[i].probabilidad = (double)valores[i].frecuencia / (double)total;
            }

            //Se calcula el codigo normal
            for (i = 0; i < linea.Length; i++)
            {
                //aa
                
                
            }
   
            return valores;
        }

        public double calcularEntropia(List<Valor> valores)
        {
            double hx = 0;
            int i, tam = valores.Count;
            //Se calcula la entropia
            for (i = 0; i < tam; i++)
            {
                //Entropia igual a la sumatoria de su probabilidad por logaritmo de su probabilidad
                hx = hx + (valores[i].probabilidad * (Math.Log(valores[i].probabilidad, 2)));
            }
            //Esa sumatoria tiene un - al inicio
            hx = Math.Abs(hx);
            //MessageBox.Show("Entropia: " + hx.ToString());
            return hx;
        }

        public double calcularMediaHuffman(List<Valor> valores)
        {
            double media = 0;
            int i, tam = valores.Count;
            //Se calcula la media de la longitud del codigo
            for (i = 0; i < tam; i++)
            {
                //La media igual a la sumatoria de su probabilidad por la longitud
                media = media + (valores[i].probabilidad * valores[i].codigohuffman.Length);
            }
            return media;
        }
        
        public double calcularMediaShannonFano(List<Valor> valores)
        {
            double media = 0;
            int i, tam = valores.Count;
            //Se calcula la media de la longitud del codigo
            for (i = 0; i < tam; i++)
            {
                //La media igual a la sumatoria de su probabilidad por la longitud
                media = media + (valores[i].probabilidad * valores[i].codigoshannonfano.Length);
            }
            return media;
        }

    }
}
