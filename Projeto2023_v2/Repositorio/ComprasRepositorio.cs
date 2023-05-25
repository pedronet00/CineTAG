using Repositorio.contexto;
using Repositorio.entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
   public class ComprasRepositorio : BaseRepositorio<Compras>
    {
        public ComprasRepositorio(EmpresaContexto contexto) : base(contexto)
        {
        }
    }
}
