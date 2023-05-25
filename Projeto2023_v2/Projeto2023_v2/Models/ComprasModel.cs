using AutoMapper;
using MercadoPago.Client.Common;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;
using Repositorio;
using Repositorio.contexto;
using Repositorio.entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto2023_v2.Models
{
    public class ComprasModel
    {

        public int id { get; set; }
        public DateTime dataCadastro { get; set; }
        public Decimal valor { get; set; }

        public int idStatus { get; set; }

        public String idPreferencia { get; set;}

        public String url { get; set; }

        public ComprasModel selecionar(int id)
        {
            ComprasModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (EmpresaContexto contexto = new EmpresaContexto())
            {
                ComprasRepositorio repositorio =
                    new ComprasRepositorio(contexto);
                //select * from categoria c where c.id = id
                Compras cat = repositorio.Recuperar(c => c.id == id);
                model = mapper.Map<ComprasModel>(cat);
            }
            return model;
        }


        public ComprasModel salvar(ComprasModel model)
        {

            //Categoria cat = new Categoria();
            //cat.id = model.id;
            //cat.descricao = model.descricao;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Compras cat = mapper.Map<Compras>(model);

            using (EmpresaContexto contexto = new EmpresaContexto())
            {
                ComprasRepositorio repositorio =
                new ComprasRepositorio(contexto);

                if (model.id == 0)
                    repositorio.Inserir(cat);
                else
                    repositorio.Alterar(cat);

                contexto.SaveChanges();
            }
            model.id = cat.id;
            return model;


        }

        public async Task<RetornoMercadoPago> gerarPagamentoMercadoPago(MercadoPagoModel model)
        {

            RetornoMercadoPago ret = new RetornoMercadoPago();
            try
            {

                string cidade = model.cidade;
                string estado = model.estado;



                // Adicione as credenciais
                MercadoPagoConfig.AccessToken = "APP_USR-3317200715988437-052308-95b364a81d2bc6e9730be2efd7a381fd-638354613";


                String[] split = model.nome.Split(' ');
                // ...
                var payer = new PreferencePayerRequest
                {
                    Name = split[0],
                    Surname = split[split.Length - 1],
                    Email = model.email,
                    Phone = new PhoneRequest
                    {
                        AreaCode = "",
                        Number = model.telefone,
                    },

                    Identification = new IdentificationRequest
                    {
                        Type = "DNI",
                        Number = model.idPagamento.ToString(),
                    },

                    Address = new AddressRequest
                    {
                        StreetName = model.logradouro,
                        StreetNumber = model.numero,
                        ZipCode = model.cep,
                    },
                };
                // ...


                // ...
                var item = new PreferenceItemRequest
                {
                    Id = model.idPagamento.ToString(),
                    Title = "Venda Cinema",
                    Description = "Compra de ingressos CineTAG",
                    CategoryId = "Cinema",
                    Quantity = 1,
                    CurrencyId = "BRL",
                    UnitPrice = model.valor,
                };
                // ...


                var request = new PreferenceRequest
                {
                    // ...
                    BackUrls = new PreferenceBackUrlsRequest
                    {
                        Success = "ENDPOINT_Retorno_sucesso",
                        Failure = "ENDPOINT_Retorno_falha",
                        Pending = "ENDPOINT_Retorno_pendencias",
                    },
                    AutoReturn = "approved",
                    Payer = payer,
                    Items = new List<PreferenceItemRequest>()
                };
                request.Items.Add(item);

                // Cria a preferência usando o client
                var client = new PreferenceClient();
                Preference preference = await client.CreateAsync(request);
                ret.url = preference.InitPoint;
                ret.idPreferencia = preference.Id;
                ret.status = "SUCESSO";
                // preference.
                return ret;

            }
            catch (Exception ex)
            {
                ret.status = "ERRO";
                ret.erro = ex.Message;
                return ret;
            }
        }
    }
}


