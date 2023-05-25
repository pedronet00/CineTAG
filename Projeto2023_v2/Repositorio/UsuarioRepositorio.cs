using Repositorio.contexto;
using Repositorio.entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class UsuarioRepositorio : BaseRepositorio<Usuario>
    {
        public UsuarioRepositorio(EmpresaContexto contexto)
            : base(contexto) { }
    }
}
