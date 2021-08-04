using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase;

namespace Business
{
    public class Usuario : Base
    {
        [OpcoesBase(UsarParaBuscar = true, ChavePrimaria = true, IsAutoincremente = true, UsarNoBancoDedados=true)]
        public int IDUsuario { get; set; }
        [OpcoesBase(UsarNoBancoDedados=true)]
        public string Nome { get; set; }
        [OpcoesBase(UsarNoBancoDedados = true)]
        public string Telefone { get; set; }
        [OpcoesBase(UsarNoBancoDedados = true)]
        public string BI { get; set; }
        public string Endereco { get {
                var dados = Enderecos;
                return dados.Count>0 ?string.Join(",",dados.Select(i => i.Morada)):null;
            } }


        public List<Endereco> Enderecos
        {

            get
            {
                List<Endereco> endereco = new List<Endereco>();

                foreach (IBase iBase in new Endereco() { IDUsuario = this.IDUsuario }.Busca())
                {
                    endereco.Add((Endereco)iBase);
                }

                return endereco;

            }
        }


        public override string ToString()
        {
            return Nome;
        }

        public new List<Usuario> Todos()
        {
            var User = new List<Usuario>();
            foreach (var Ibase in base.Todos())
            {
                User.Add((Usuario)Ibase);
            }
            return User;
        }

        public new List<Usuario> Busca()
        {

            var User = new List<Usuario>();
            foreach (var Ibase in base.Busca())
            {
                User.Add((Usuario)Ibase);
            }
            return User;

            
        }
    }
}
