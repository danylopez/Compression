using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compression
{
    class Valor
    {
        public char nombre { get; set; }
        public int frecuencia { get; set; }
        public double probabilidad { get; set; }
        public string codigohuffman { get; set; }
        public string codigoshannonfano { get; set; }
        public string codigo { get; set; }
    }
}
