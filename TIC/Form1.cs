using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compression
{
    public partial class Form1 : Form
    {
        static string linea = ""; //Valor que se lee
        static List<Valor> valores = new List<Valor>(); //Crea lista de valores

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {    
            Desarrollo obj = new Desarrollo(); //Crea objeto de practica
            double hx, n, miu;
            int i, j, aux;
            bool x = true; //Auxiliar para impresion
            string codex, codey, auxlinea;

            /*
             *
             * LECTURA ARCHIVO
             * 
             */
            //Lectura Archivo si no se a elegido ningun archivo
            aux = linea.Length;//Auxiliar si ya se leyo un archivo
            if (aux == 0)
            {
                //Mientras no se abra ningun archivo
                while (true)
                {
                    //Crea una ventana para abrir archivo
                    OpenFileDialog openFileDialog1 = new OpenFileDialog();
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        System.IO.StreamReader archivo = new System.IO.StreamReader(openFileDialog1.FileName);
                        //Lectura de la linea
                        linea = archivo.ReadLine();
                        while ((auxlinea = archivo.ReadLine()) != null)
                        {
                            linea = linea + auxlinea;
                        }
                        //Si lee algo sale del ciclo infinito
                        if (linea.Length != 0)
                        {
                            break;
                        }

                        archivo.Close();
                    }
                }
            }

            //Llama al metodo leer
            valores = obj.llenarvector(linea);
            
            /*
            * 
            * METODO HUFFMAN
            * 
            */
            //Se crea un objeto tipo Huffman
            Huffman_namespace.CrearHuffman objt = new Huffman_namespace.CrearHuffman();
            //Lista con caracter
            List<String> listaHuffmanTodo = new List<string>();
            //Lista sin caracter
            List<String> listaHuffman = new List<string>();
            //Se llama metodo Huffman
            listaHuffmanTodo = objt.crearHuffman(linea);
            //Se agrega a la lista de vectores
            for (i = 0; i < listaHuffmanTodo.Count; i++)
            {
                listaHuffman.Add(listaHuffmanTodo[i].Remove(0, 1));
                valores[i].codigohuffman = listaHuffman[i];
            }

            /*
             * 
             * MOSTRAR DATOS LEIDOS
             * 
             */
            //Muestra los datos en el Form
            richTextBox1.Text = "";
            for (i = 0; i < linea.Length; i++)
            {
                if (x == true)
                {
                    AnexarTexto(this.richTextBox1, Color.Green, linea[i].ToString());
                    x = false;
                }
                else
                {
                    AnexarTexto(this.richTextBox1, Color.Blue, linea[i].ToString());
                    x = true;
                }
            }

            /*
            * 
            * MOSTRAR CODIGO NORMAL A 8BITS
            * 
            */
            x = true;
            richTextBox4.Text = "";
            for (i = 0; i < linea.Length; i++)
            {
                if (x == true)
                {
                    for (j = 0; j < valores.Count; j++)
                    {
                        if (String.Equals(valores[j].nombre.ToString(), linea[i].ToString(), StringComparison.Ordinal))
                        {
                            AnexarTexto(this.richTextBox4, Color.Green, valores[j].codigo);
                            break;
                        }
                    }
                    x = false;
                }
                else
                {
                    for (j = 0; j < valores.Count; j++)
                    {
                        if (String.Equals(valores[j].nombre.ToString(), linea[i].ToString(), StringComparison.Ordinal))
                        {
                            AnexarTexto(this.richTextBox4, Color.Blue, valores[j].codigo);
                            break;
                        }
                    }
                    x = true;
                }
            }

            /*
            * 
            * METODO SHANNON FANO
            * 
            */
            //Se crea un objeto tipo ShannonFano
            ShannonFano_namespace.CrearShannonFano objto = new ShannonFano_namespace.CrearShannonFano();
            valores = objto.crearShannonFano(valores);
            
            /*
            * 
            * MOSTRAR DATOS CODIFICADOS HUFFMAN
            * 
            */
            x = true;
            richTextBox2.Text = "";
            for (i = 0; i < linea.Length; i++)
            {
                if (x == true)
                {
                    for (j = 0; j < valores.Count; j++)
                    {
                        if (String.Equals(valores[j].nombre.ToString(), linea[i].ToString(), StringComparison.Ordinal))
                        {
                            AnexarTexto(this.richTextBox2, Color.Green, valores[j].codigohuffman);
                            break;
                        }
                    }
                    x = false;
                }
                else
                {
                    for (j = 0; j < valores.Count; j++)
                    {
                        if (String.Equals(valores[j].nombre.ToString(), linea[i].ToString(), StringComparison.Ordinal))
                        {
                            AnexarTexto(this.richTextBox2, Color.Blue, valores[j].codigohuffman);
                            break;
                        }
                    }
                    x = true;
                }
            }
            //Guardamos el codigo
            codex = richTextBox2.Text+"x";

            /*
            * 
            * MOSTRAR TRADUCCION HUFFMAN
            * 
            */
            x = true;
            richTextBox3.Text = "";
            while (codex != "x")
            {
                if (x == true)
                {
                    for (j = 0; j < valores.Count; j++)
                    {
                        if (codex.StartsWith(valores[j].codigohuffman))
                        {
                            AnexarTexto(this.richTextBox3, Color.Green, valores[j].nombre.ToString());
                            codex = codex.Substring(valores[j].codigohuffman.ToString().Length);
                            break;
                        }
                    }
                    x = false;
                }
                else
                {
                    for (j = 0; j < valores.Count; j++)
                    {
                        if (codex.StartsWith(valores[j].codigohuffman))
                        {
                            AnexarTexto(this.richTextBox3, Color.Blue, valores[j].nombre.ToString());
                            codex = codex.Substring(valores[j].codigohuffman.ToString().Length);
                            break;
                        }
                    }
                    x = true;
                }
            }

            

            /*
            * 
            * MOSTRAR DATOS CODIFICADOS SHANNONFANO
            * 
            */
            x = true;
            richTextBox5.Text = "";
            for (i = 0; i < linea.Length; i++)
            {
                if (x == true)
                {
                    for (j = 0; j < valores.Count; j++)
                    {
                        if (String.Equals(valores[j].nombre.ToString(), linea[i].ToString(), StringComparison.Ordinal))
                        {
                            AnexarTexto(this.richTextBox5, Color.Green, valores[j].codigoshannonfano);
                            break;
                        }
                    }
                    x = false;
                }
                else
                {
                    for (j = 0; j < valores.Count; j++)
                    {
                        if (String.Equals(valores[j].nombre.ToString(), linea[i].ToString(), StringComparison.Ordinal))
                        {
                            AnexarTexto(this.richTextBox5, Color.Blue, valores[j].codigoshannonfano);
                            break;
                        }
                    }
                    x = true;
                }
            }

            //Guardamos codigo
            codey = richTextBox5.Text + "x";

            /*
            * 
            * MOSTRAR TRADUCCION SHANNONFANO
            * 
            */
            x = true;
            richTextBox6.Text = "";
            while (codey != "x")
            {
                if (x == true)
                {
                    for (j = 0; j < valores.Count; j++)
                    {
                        if (codey.StartsWith(valores[j].codigoshannonfano))
                        {
                            AnexarTexto(this.richTextBox6, Color.Green, valores[j].nombre.ToString());
                            codey = codey.Substring(valores[j].codigoshannonfano.ToString().Length);
                            break;
                        }
                    }
                    x = false;
                }
                else
                {
                    for (j = 0; j < valores.Count; j++)
                    {
                        if (codey.StartsWith(valores[j].codigoshannonfano))
                        {
                            AnexarTexto(this.richTextBox6, Color.Blue, valores[j].nombre.ToString());
                            codey = codey.Substring(valores[j].codigoshannonfano.ToString().Length);
                            break;
                        }
                    }
                    x = true;
                }
            }


            //Guardamos el codigo
            codex = richTextBox2.Text + "x";

            /*
            * 
            * CALCULA LA EFICIENCIA HUFFMAN
            * 
            */
            //Calcula la Entropia
            hx = obj.calcularEntropia(valores);
            //Calcula la Media
            n = obj.calcularMediaHuffman(valores);
            miu = (hx / n) * 100;
            label5.Text = miu+"%".ToString();

            /*
            * 
            * CALCULA LA EFICIENCIA SHANNONFANO
            * 
            */
            //Calcula la Entropia
            hx = obj.calcularEntropia(valores);
            //Calcula la Media
            n = obj.calcularMediaShannonFano(valores);
            miu = (hx / n) * 100;
            label9.Text = miu + "%".ToString();

            //Llamar el metodo
            dataGridView1.DataSource = obtenerTabla();

        }

        public DataTable obtenerTabla()
        {
            int i;
            // Creando la tabla
            DataTable tabla = new DataTable();
            tabla.Columns.Add("Nombre", typeof(char));
            tabla.Columns.Add("Frecuencia", typeof(int));
            tabla.Columns.Add("Probabilidad", typeof(double));
            tabla.Columns.Add("Código", typeof(string));
            tabla.Columns.Add("Código Huffman", typeof(string));
            tabla.Columns.Add("Código Shannon-Fano", typeof(string));
            for (i = 0; i < valores.Count; i++)
            {
                tabla.Rows.Add(valores[i].nombre, valores[i].frecuencia, valores[i].probabilidad, valores[i].codigo, valores[i].codigohuffman, valores[i].codigoshannonfano);
            }
            //table.Rows.Add(0, 0, 0, 0, 0, 0, 0, 0);
            /*foreach (Valor temp in valores)
            {
                
            }*/
            return tabla;
        }

        //Metodo Para dar color a RichBox
        void AnexarTexto(RichTextBox box, Color color, string text)
        {
            int start = box.TextLength;
            box.AppendText(text);
            int end = box.TextLength;
            // Textbox may transform chars, so (end-start) != text.Length
            box.Select(start, end - start);
            {
                box.SelectionColor = color;
                // could set box.SelectionBackColor, box.SelectionFont too.
            }
            box.SelectionLength = 0; // clear
        }

        //Submenu para elegir archivo
        private void elegirArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Crea una ventana para abrir archivo
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader archivo = new System.IO.StreamReader(openFileDialog1.FileName);
                //Lectura de la linea
                linea = archivo.ReadLine();
                archivo.Close();
                MessageBox.Show("Archivo cargado correctamente.", "Mensaje");
            }
        }

    }
}
