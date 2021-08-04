using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase;

namespace Business
{
    public class Endereco:Base
    {
        [OpcoesBase(ChavePrimaria =true, IsAutoincremente=true, UsarNoBancoDedados = true)]
        public int IDEndereco { get; set; }
        [OpcoesBase(UsarNoBancoDedados = true)]
        public string Morada { get; set; }

        [OpcoesBase(UsarNoBancoDedados = true)]
        public string Localidade { get; set; }

        [OpcoesBase(UsarNoBancoDedados = true)]
        public int IDUsuario { get; set; }




        public new List<Endereco> Todos()
        {
            var End = new List<Endereco>();
            foreach (var Ibase in base.Todos())
            {
                End.Add((Endereco)Ibase);
            }
            return End;
        }

        public new List<Endereco> Busca()
        {

            var End = new List<Endereco>();
            foreach (var Ibase in base.Busca())
            {
                End.Add((Endereco)Ibase);
            }
            return End;


        }

    }
}
