using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.entidades
{
   public class ComprasProdutos
    {
        public int id { get; set; }
        public int qtde { get; set; }
        public Decimal valor { get; set; }

        public int idCompra { get; set; }
        public int idProduto { get; set; }


        public virtual Compras compras { get; set; }
        public virtual Produto produto { get; set; }

       

    }
}
