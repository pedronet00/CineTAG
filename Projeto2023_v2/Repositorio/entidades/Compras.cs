using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.entidades
{
   public class Compras
    {
        public Compras()
        {
            this.comprasprodutos = new HashSet<ComprasProdutos>();
        }

        public int id { get; set; }
        public DateTime dataCadastro { get; set; }
        public Decimal valor { get; set; }
      
        public int idStatus { get; set; }

       
        public virtual Status status { get; set; }

        public String idPreferencia { get; set; }   

        public String url { get; set; }

        public virtual ICollection<ComprasProdutos> comprasprodutos { get; set; }

    }
}
