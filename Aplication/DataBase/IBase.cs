using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public interface IBase
    {
        int key { get; }
        void Salvar();
        void Excluir();

        void CriarTabelas();

        List<IBase> Todos();
        List<IBase> Busca();
    }
}
