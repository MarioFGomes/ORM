using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class OpcoesBase:Attribute
    {
        public bool UsarNoBancoDedados { get; set; }
        public bool UsarParaBuscar { get; set; }
        public bool ChavePrimaria { get; set; }
        public bool IsAutoincremente { get; set; }
    }
}
