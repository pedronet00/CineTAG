
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio.contexto;
using Repositorio.entidades;

namespace Repositorio
{
   public class CategoriaRepositorio:BaseRepositorio<Categoria>
    {
        public CategoriaRepositorio(EmpresaContexto contexto)
            :base(contexto) { }
    }
}
