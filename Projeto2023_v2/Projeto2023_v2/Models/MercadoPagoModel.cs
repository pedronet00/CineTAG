using System;

namespace Projeto2023_v2.Models
{
    public class MercadoPagoModel
    {
        public String email { get; set; }

        public String nome { get; set; }
        public String cidade { get; set; }
        public String estado { get; set; }
        public String telefone { get; set; }
        public int idPagamento { get; set; }
        public String logradouro { get; set; }
        public String numero { get; set; }
        public String cep { get; set; }
        public String nomePlano { get; set; }
        public decimal valor { get; set; }

    }
}
