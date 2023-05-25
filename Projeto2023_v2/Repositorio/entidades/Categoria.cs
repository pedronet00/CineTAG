using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.entidades
{
    public class Categoria
    {
        public Categoria() {
            this.produtos = new HashSet<Produto>();
        }
        public int id { get; set; }
        public String descricao { get; set; }

        public virtual ICollection<Produto> produtos { get; set; }
    }
}
