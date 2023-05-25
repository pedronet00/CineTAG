using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.entidades
{
    public class Status
    {
        public Status() {
            this.compras = new HashSet<Compras>();
        }
        public int id { get; set; }
        public String descricao { get; set; }

        public virtual ICollection<Compras> compras { get; set; }
    }
}
