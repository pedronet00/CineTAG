using Repositorio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.entidades
{
   public class Usuario
    {
        public int id { get; set; }
        public String nome { get; set; }
        public String email { get; set; }
        public String senha { get; set; }
        //0: adm e 1: normal

        public PerfilEnum PerfilEnum { get; set; }
    }
}
