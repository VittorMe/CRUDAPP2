using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDAPP2.Entity
{
    class DadosDgvProduto
    {
        public int codProduto { get; set; }
        public string nomeProduto { get; set; }
        public string  nomeGrupo { get; set; }
        public string precoCusto { get; set; }
        public string precoVenda { get; set; }
        public bool ativo { get; set; }

    }
}
