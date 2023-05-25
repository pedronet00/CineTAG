using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.entidades
{
   public class Produto
    {
        public Produto()
        {
            this.comprasprodutos = new HashSet<ComprasProdutos>();

        }
        public int id { get; set; }
        public String descricao { get; set; }

        public String sinopse { get; set; }
        public DateTime dataCadastro { get; set; }
        public Decimal valor { get; set; }
        public bool ehDestaque { get; set; }
        public int idCategoria { get; set; }

        public String imagem { get; set; }
        public virtual Categoria categoria { get; set; }
        public virtual ICollection<ComprasProdutos> comprasprodutos { get; set; }

    }
}
